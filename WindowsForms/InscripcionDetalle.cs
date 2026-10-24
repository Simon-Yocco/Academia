using API.Clients;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class InscripcionDetalle : Form
    {
        private AlumnoInscripcionDTO _inscripcion = new();
        private FormMode _mode;

        public InscripcionDetalle(FormMode mode, AlumnoInscripcionDTO inscripcion) : this()
        {
            Init(mode, inscripcion);
        }

        public InscripcionDetalle()
        {
            InitializeComponent();
        }

        private async void Init(FormMode mode, AlumnoInscripcionDTO inscripcion)
        {
            _mode = mode;
            _inscripcion = inscripcion;

            aceptarButton.Enabled = false;

            await LoadCombos();
            ConfigurarPantalla();

            aceptarButton.Enabled = true;
        }

        private void InscripcionDetalle_Load(object? sender, EventArgs e)
        {
        }

        private async Task LoadCombos()
        {
            try
            {
                // 1. Cargar alumnos
                var usuarios = await UsuarioApiClient.GetAllAsync();
                var alumnos = usuarios
                    .Where(u => u.Rol == "Alumno" || u.Persona?.TipoPersona == "Alumno")
                    .Select(u => new
                    {
                        ID = u.IDPersona,
                        Descripcion = $"{u.Apellido}, {u.Nombre} (Legajo: {u.Legajo})"
                    })
                    .OrderBy(a => a.Descripcion)
                    .ToList();

                alumnoComboBox.DataSource = alumnos;
                alumnoComboBox.DisplayMember = "Descripcion";
                alumnoComboBox.ValueMember = "ID";
                alumnoComboBox.SelectedIndex = -1;

                // 2. Cargar cursos
                var cursos = await CursoApiClient.GetAllAsync();
                var listaCursos = cursos
                    .Select(c => new
                    {
                        ID = c.ID,
                        Descripcion = $"{c.MateriaDescripcion} - {c.ComisionDescripcion} ({c.AnioCalendario}) - Cupo: {c.Cupo}"
                    })
                    .OrderBy(c => c.Descripcion)
                    .ToList();

                cursoComboBox.DataSource = listaCursos;
                cursoComboBox.DisplayMember = "Descripcion";
                cursoComboBox.ValueMember = "ID";
                cursoComboBox.SelectedIndex = -1;

                // 3. Cargar opciones de condicion
                condicionComboBox.Items.Clear();
                condicionComboBox.Items.AddRange(new object[] { "Inscripto", "Regular", "Aprobado", "Libre" });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos en los desplegables: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarPantalla()
        {
            if (_mode == FormMode.Add)
            {
                // Ocultar Id
                idTextBox.Visible = false;
                idLabel.Visible = false;

                // Ocultar Condicion y Nota al dar de alta
                condicionComboBox.Visible = false;
                condicionLabel.Visible = false;
                notaNumericUpDown.Visible = false;
                notaLabel.Visible = false;

                alumnoComboBox.Enabled = true;
            }
            else if (_mode == FormMode.Update)
            {
                idTextBox.Visible = true;
                idLabel.Visible = true;
                idTextBox.Text = _inscripcion.ID.ToString();

                // Mostrar Condicion y Nota para poder calificar o actualizar estado
                condicionComboBox.Visible = true;
                condicionLabel.Visible = true;
                notaNumericUpDown.Visible = true;
                notaLabel.Visible = true;

                // En modificacion no se cambia el alumno
                alumnoComboBox.SelectedValue = _inscripcion.IDAlumno;
                alumnoComboBox.Enabled = false;

                cursoComboBox.SelectedValue = _inscripcion.IDCurso;

                if (BaseApiClient.EsDocente)
                {
                    cursoComboBox.Enabled = false;
                    this.Text = "Cargar Calificación";
                    aceptarButton.Text = "Guardar Nota";
                }

                condicionComboBox.SelectedItem = string.IsNullOrEmpty(_inscripcion.Condicion)
                    ? "Inscripto"
                    : _inscripcion.Condicion;

                if (_inscripcion.Nota.HasValue &&
                    _inscripcion.Nota.Value >= notaNumericUpDown.Minimum &&
                    _inscripcion.Nota.Value <= notaNumericUpDown.Maximum)
                {
                    notaNumericUpDown.Value = _inscripcion.Nota.Value;
                }
                else
                {
                    notaNumericUpDown.Value = notaNumericUpDown.Minimum;
                }
            }
        }

        private async void aceptarButton_Click(object? sender, EventArgs e)
        {
            if (alumnoComboBox.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un alumno.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cursoComboBox.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un curso.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _inscripcion.IDAlumno = (int)alumnoComboBox.SelectedValue;
            _inscripcion.IDCurso = (int)cursoComboBox.SelectedValue;

            if (_mode == FormMode.Add)
            {
                _inscripcion.Condicion = "Inscripto";
                _inscripcion.Nota = null;
            }
            else
            {
                _inscripcion.Condicion = condicionComboBox.SelectedItem?.ToString() ?? "Inscripto";
                _inscripcion.Nota = (int)notaNumericUpDown.Value;
            }

            try
            {
                aceptarButton.Enabled = false;

                if (_mode == FormMode.Add)
                {
                    await InscripcionApiClient.AddAsync(_inscripcion);
                }
                else if (_mode == FormMode.Update)
                {
                    var ok = await InscripcionApiClient.UpdateAsync(_inscripcion);
                    if (!ok) throw new Exception("No se pudo actualizar la inscripcion.");
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                // Muestra directamente las validaciones de negocio (cupo lleno, ya inscripto, etc.)
                MessageBox.Show(ex.Message, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                aceptarButton.Enabled = true;
            }
        }

        private void cancelarButton_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
