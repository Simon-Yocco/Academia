using API.Clients;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class ComisionDetalle : Form
    {
        private ComisionDTO _comision = new();
        private FormMode _mode;

        public ComisionDetalle(FormMode mode, ComisionDTO comision) : this()
        {
            Init(mode, comision);
        }

        public ComisionDetalle()
        {
            InitializeComponent();
        }

        private async void Init(FormMode mode, ComisionDTO comision)
        {
            _mode = mode;
            _comision = comision;

            aceptarButton.Enabled = false; // Bloqueamos para evitar errores mientras carga

            await LoadCombos();
            ConfigurarPantalla();

            aceptarButton.Enabled = true;
        }

        private async Task LoadCombos()
        {
            try
            {
                var planes = await PlanApiClient.GetAllAsync();
                planComboBox.DataSource = planes.ToList();
                planComboBox.DisplayMember = "Descripcion";
                planComboBox.ValueMember = "ID";
                planComboBox.SelectedIndex = -1; // Por defecto vacío
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los desplegables: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarPantalla()
        {
            if (_mode == FormMode.Add)
            {
                idTextBox.Visible = false;
                idLabel.Visible = false;
                anioEspecialidadNumericUpDown.Value = 1;
            }
            else if (_mode == FormMode.Update)
            {
                idTextBox.Visible = true;
                idTextBox.Text = _comision.ID.ToString();
                descripcionTextBox.Text = _comision.Descripcion;

                if (_comision.AnioEspecialidad >= anioEspecialidadNumericUpDown.Minimum &&
                    _comision.AnioEspecialidad <= anioEspecialidadNumericUpDown.Maximum)
                {
                    anioEspecialidadNumericUpDown.Value = _comision.AnioEspecialidad;
                }
            }

            if (_comision.IDPlan > 0) planComboBox.SelectedValue = _comision.IDPlan;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                MessageBox.Show("La descripción es requerida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (planComboBox.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un Plan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (anioEspecialidadNumericUpDown.Value < 1)
            {
                MessageBox.Show("El año de especialidad debe ser mayor o igual a 1.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _comision.Descripcion = descripcionTextBox.Text.Trim();
            _comision.AnioEspecialidad = (int)anioEspecialidadNumericUpDown.Value;
            _comision.IDPlan = (int)planComboBox.SelectedValue;

            try
            {
                aceptarButton.Enabled = false;
                if (_mode == FormMode.Add)
                {
                    await ComisionApiClient.AddAsync(_comision);
                }
                else if (_mode == FormMode.Update)
                {
                    var ok = await ComisionApiClient.UpdateAsync(_comision);
                    if (!ok) throw new Exception("No se pudo actualizar la comisión.");
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                aceptarButton.Enabled = true;
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

