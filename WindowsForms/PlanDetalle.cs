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
        private PlanDTO _plan;
        private FormMode _mode;
        private BindingList<MateriaDTO> _materias;
        public PlanDetalle()
        {
            InitializeComponent();
            _materias = new BindingList<MateriaDTO>();
        }
        public PlanDetalle(FormMode mode, PlanDTO plan) : this()
        {
            Init(mode, plan);
        }
        private async void Init(FormMode mode, PlanDTO plan)
        {
            _mode = mode;
            _plan = plan ?? new PlanDTO();
            aceptarButton.Enabled = false;
            // Enlazamos la grilla a la lista en memoria
            if (_plan.Materias != null && _plan.Materias.Any())
            {
                _materias = new BindingList<MateriaDTO>(_plan.Materias.ToList());
            }
            materiaDataGridView.DataSource = _materias;
            await CargarEspecialidades();
            ConfigurarPantalla();
            aceptarButton.Enabled = true;
        }
        private async Task CargarEspecialidades()
        {
            try
            {
                var especialidades = await EspecialidadApiClient.GetAllAsync();
                especialidadComboBox.DataSource = especialidades.ToList();
                especialidadComboBox.DisplayMember = "Descripcion";
                especialidadComboBox.ValueMember = "ID";
                especialidadComboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar especialidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            var form = new MateriaDetalle(FormMode.Add, new MateriaDTO());
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _materias.Add(form.Materia);
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
            var form = new MateriaDetalle(FormMode.Update, materiaSeleccionada);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                materiaDataGridView.Refresh();
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
            _materias.Remove(materiaSeleccionada);
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
            if (_materias.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos una materia al plan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _plan.Descripcion = descripcionTextBox.Text.Trim();
            _plan.IDEspecialidad = (int)especialidadComboBox.SelectedValue;
            _plan.Materias = _materias.ToList();
            try
            {
                aceptarButton.Enabled = false;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el plan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                aceptarButton.Enabled = true;
            }
        }
        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}