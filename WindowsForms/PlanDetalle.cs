using API.Clients;
using DTOs;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class PlanDetalle : Form
    {
        private PlanDTO _plan = new();
        private FormMode _mode;
        private List<MateriaDTO> materiasLocales;

        public PlanDetalle()
        {
            InitializeComponent();
            ConfigurarColumnasGrilla();
            materiasLocales = new List<MateriaDTO>();
        }

        private void ConfigurarColumnasGrilla()
        {
            materiaDataGridView.AutoGenerateColumns = false;
            materiaDataGridView.AllowUserToAddRows = false;
            materiaDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            materiaDataGridView.Columns.Clear();

            materiaDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Materia",
                DataPropertyName = "Descripcion",
                FillWeight = 50
            });
            materiaDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HSSemanales",
                HeaderText = "Hs Semanales",
                DataPropertyName = "HSSemanales",
                FillWeight = 25
            });
            materiaDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HSTotales",
                HeaderText = "Hs Totales",
                DataPropertyName = "HSTotales",
                FillWeight = 25
            });
        }

        public PlanDetalle(FormMode mode, PlanDTO plan) : this()
        {
            Init(mode, plan);
        }

        private async void Init(FormMode mode, PlanDTO plan)
        {
            try
            {
                DeshabilitarControles();
                _mode = mode;
                _plan = plan ?? new PlanDTO();

                // Clonar materias para modelo local en memoria
                materiasLocales = (_plan.Materias ?? new List<MateriaDTO>()).Select(m => new MateriaDTO
                {
                    ID = m.ID,
                    Descripcion = m.Descripcion,
                    HSSemanales = m.HSSemanales,
                    HSTotales = m.HSTotales,
                    IDPlan = m.IDPlan
                }).ToList();

                RefreshMateriasGrid();
                await CargarEspecialidades();
                ConfigurarPantalla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void RefreshMateriasGrid()
        {
            materiaDataGridView.DataSource = null;
            materiaDataGridView.DataSource = materiasLocales;

            // Habilitar/deshabilitar botones según si hay materias
            bool hasItems = materiasLocales != null && materiasLocales.Count > 0;
            modificarButton.Enabled = hasItems;
            eliminarButton.Enabled = hasItems;

            if (hasItems && materiaDataGridView.Rows.Count > 0)
            {
                materiaDataGridView.Rows[0].Selected = true;
            }
        }

        private async Task CargarEspecialidades()
        {
            var especialidades = await EspecialidadApiClient.GetAllAsync();
            especialidadComboBox.DataSource = especialidades.ToList();
            especialidadComboBox.DisplayMember = "Descripcion";
            especialidadComboBox.ValueMember = "ID";
            especialidadComboBox.SelectedIndex = -1;
        }

        private void ConfigurarPantalla()
        {
            if (_mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
            }
            else if (_mode == FormMode.Update)
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
                idTextBox.Text = _plan.ID.ToString();
                descripcionTextBox.Text = _plan.Descripcion;

                if (_plan.IDEspecialidad > 0)
                {
                    especialidadComboBox.SelectedValue = _plan.IDEspecialidad;
                }
            }
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {
            var nuevoItem = new MateriaDTO();
            var form = new MateriaDetalle(FormMode.Add, nuevoItem);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                materiasLocales.Add(form.Materia);
                RefreshMateriasGrid();
            }
        }

        private void modificarButton_Click(object sender, EventArgs e)
        {
            if (materiaDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una materia de la grilla para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var materiaSeleccionada = (MateriaDTO)materiaDataGridView.CurrentRow.DataBoundItem;

            // Clonar materia para edición
            var itemCopia = new MateriaDTO
            {
                ID = materiaSeleccionada.ID,
                Descripcion = materiaSeleccionada.Descripcion,
                HSSemanales = materiaSeleccionada.HSSemanales,
                HSTotales = materiaSeleccionada.HSTotales,
                IDPlan = materiaSeleccionada.IDPlan
            };

            var form = new MateriaDetalle(FormMode.Update, itemCopia);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                int index = materiasLocales.IndexOf(materiaSeleccionada);
                if (index >= 0)
                {
                    materiasLocales[index] = form.Materia;
                    RefreshMateriasGrid();
                }
            }
        }

        private void eliminarButton_Click(object sender, EventArgs e)
        {
            if (materiaDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una materia de la grilla para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var materiaSeleccionada = (MateriaDTO)materiaDataGridView.CurrentRow.DataBoundItem;

            var result = MessageBox.Show($"¿Está seguro que desea eliminar la materia '{materiaSeleccionada.Descripcion}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                materiasLocales.Remove(materiaSeleccionada);
                RefreshMateriasGrid();
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                MessageBox.Show("La descripción del plan es requerida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (especialidadComboBox.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una Especialidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (materiasLocales == null || materiasLocales.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos una materia al plan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DeshabilitarControles();

                _plan.Descripcion = descripcionTextBox.Text.Trim();
                _plan.IDEspecialidad = (int)especialidadComboBox.SelectedValue;
                _plan.Materias = materiasLocales.ToList();

                if (_mode == FormMode.Add)
                {
                    await PlanApiClient.AddAsync(_plan);
                }
                else if (_mode == FormMode.Update)
                {
                    var ok = await PlanApiClient.UpdateAsync(_plan);
                    if (!ok) throw new Exception("No se pudo actualizar el plan.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el plan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DeshabilitarControles()
        {
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            descripcionTextBox.Enabled = false;
            especialidadComboBox.Enabled = false;
            materiaDataGridView.Enabled = false;
            agregarButton.Enabled = false;
            modificarButton.Enabled = false;
            eliminarButton.Enabled = false;
        }

        private void HabilitarControles()
        {
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            descripcionTextBox.Enabled = true;
            especialidadComboBox.Enabled = true;
            materiaDataGridView.Enabled = true;
            agregarButton.Enabled = true;

            bool hasItems = materiasLocales != null && materiasLocales.Count > 0;
            modificarButton.Enabled = hasItems;
            eliminarButton.Enabled = hasItems;
        }
    }
}