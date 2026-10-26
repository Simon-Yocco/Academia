using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class InscripcionLista : Form
    {
        public InscripcionLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
            ConfigurarAnchors();
            this.Load += InscripcionLista_Load;
        }


        private void ConfigurarAnchors()
        {
            inscripcionDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }
        private void ConfigureButtonPermissions()
        {
            // El admin y el alumno pueden inscribir; el docente y admin pueden calificar/actualizar; solo el admin elimina
            bool canAdd = BaseApiClient.EsAdmin || BaseApiClient.EsAlumno;
            bool canUpdate = BaseApiClient.EsAdmin || BaseApiClient.EsDocente;
            bool canDelete = BaseApiClient.EsAdmin;

            agregarButton.Visible = canAdd;
            actualizarButton.Visible = canUpdate;
            eliminarButton.Visible = canDelete;

            // Si es docente o alumno, personalizamos el texto del botón y el título
            if (BaseApiClient.EsDocente)
            {
                actualizarButton.Text = "Cargar Nota";
                this.Text = "Alumnos de mis Cursos - Calificaciones";
            }
            else if (BaseApiClient.EsAlumno)
            {
                agregarButton.Text = "Inscribirme";
                this.Text = "Mis Inscripciones a Cursado";
            }
        }

        private void ConfigurarColumnas()
        {
            inscripcionDataGridView.AutoGenerateColumns = false;
            inscripcionDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            inscripcionDataGridView.AllowUserToAddRows = false;
            inscripcionDataGridView.AllowUserToDeleteRows = false;
            inscripcionDataGridView.ReadOnly = true;
            inscripcionDataGridView.MultiSelect = false;
            inscripcionDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            inscripcionDataGridView.Columns.Clear();

            inscripcionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                DataPropertyName = "ID",
                Visible = false
            });

            inscripcionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AlumnoLegajo",
                HeaderText = "Legajo",
                DataPropertyName = "AlumnoLegajo",
                FillWeight = 40
            });

            inscripcionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AlumnoNombreCompleto",
                HeaderText = "Alumno",
                DataPropertyName = "AlumnoNombreCompleto",
                FillWeight = 80
            });

            inscripcionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MateriaDescripcion",
                HeaderText = "Materia",
                DataPropertyName = "MateriaDescripcion",
                FillWeight = 70
            });

            inscripcionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ComisionDescripcion",
                HeaderText = "Comisión",
                DataPropertyName = "ComisionDescripcion",
                FillWeight = 45
            });

            inscripcionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AnioCalendario",
                HeaderText = "Año",
                DataPropertyName = "AnioCalendario",
                FillWeight = 35
            });

            inscripcionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Condicion",
                HeaderText = "Condición",
                DataPropertyName = "Condicion",
                FillWeight = 50
            });

            inscripcionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nota",
                HeaderText = "Nota",
                DataPropertyName = "Nota",
                FillWeight = 30
            });
        }

        private async void InscripcionLista_Load(object? sender, EventArgs e)
        {
            ConfigureButtonPermissions();
            await CargarGrilla();
        }

        private async Task CargarGrilla()
        {
            try
            {
                IEnumerable<AlumnoInscripcionDTO> inscripciones;

                if (BaseApiClient.EsDocente)
                {
                    // 1. Obtener cursos donde dicta clases el docente logueado
                    var todosLosCursos = await CursoApiClient.GetAllAsync();
                    var misCursos = todosLosCursos
                        .Where(c => c.IDDocente == BaseApiClient.IDPersonaActual)
                        .Select(c => c.ID)
                        .ToHashSet();

                    // 2. Filtrar solo los alumnos inscriptos en esos cursos
                    var todas = await InscripcionApiClient.GetAllAsync();
                    var misAlumnos = todas.Where(i => misCursos.Contains(i.IDCurso));

                    if (!string.IsNullOrWhiteSpace(buscarTextBox.Text))
                    {
                        var filtro = buscarTextBox.Text.Trim();
                        misAlumnos = misAlumnos.Where(i =>
                            i.AlumnoNombreCompleto.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                            i.MateriaDescripcion.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                            i.AlumnoLegajo.ToString().Contains(filtro));
                    }

                    inscripciones = misAlumnos;
                }
                else if (BaseApiClient.EsAlumno)
                {
                    var misInscripciones = await InscripcionApiClient.GetByAlumnoAsync(BaseApiClient.IDPersonaActual);
                    if (!string.IsNullOrWhiteSpace(buscarTextBox.Text))
                    {
                        var filtro = buscarTextBox.Text.Trim();
                        misInscripciones = misInscripciones.Where(i =>
                            i.MateriaDescripcion.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                            i.ComisionDescripcion.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                            i.Condicion.Contains(filtro, StringComparison.OrdinalIgnoreCase));
                    }
                    inscripciones = misInscripciones;
                }
                else
                {
                    // Administrador: búsqueda normal
                    if (string.IsNullOrWhiteSpace(buscarTextBox.Text))
                    {
                        inscripciones = await InscripcionApiClient.GetAllAsync();
                    }
                    else
                    {
                        inscripciones = await InscripcionApiClient.GetByCriteriaAsync(buscarTextBox.Text.Trim());
                    }
                }

                inscripcionDataGridView.DataSource = inscripciones.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar inscripciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            var formDetalle = new InscripcionDetalle(FormMode.Add, new AlumnoInscripcionDTO());
            if (formDetalle.ShowDialog(this) == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (inscripcionDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná una inscripción de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filaSeleccionada = inscripcionDataGridView.SelectedRows[0];
            var inscripcionSeleccionada = (AlumnoInscripcionDTO)filaSeleccionada.DataBoundItem;

            var formDetalle = new InscripcionDetalle(FormMode.Update, inscripcionSeleccionada);
            if (formDetalle.ShowDialog(this) == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (inscripcionDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná una inscripción de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filaSeleccionada = inscripcionDataGridView.SelectedRows[0];
            var inscripcionSeleccionada = (AlumnoInscripcionDTO)filaSeleccionada.DataBoundItem;

            var confirmacion = MessageBox.Show(
                $"¿Seguro que deseás dar de baja la inscripción del alumno {inscripcionSeleccionada.AlumnoNombreCompleto} en la materia {inscripcionSeleccionada.MateriaDescripcion}?",
                "Confirmar baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    await InscripcionApiClient.DeleteAsync(inscripcionSeleccionada.ID);
                    await CargarGrilla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
