using API.Clients;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class MateriaDetalle : Form
    {
        private MateriaDTO _materia;
        private FormMode _mode;

        public MateriaDTO Materia => _materia;

        public MateriaDetalle()
        {
            InitializeComponent();
        }

        public MateriaDetalle(FormMode mode, MateriaDTO materia) : this()
        {
            Init(mode, materia);
        }

        private async void Init(FormMode mode, MateriaDTO materia)
        {
            _mode = mode;
            _materia = materia ?? new MateriaDTO();

            aceptarButton.Enabled = false;

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
                planComboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los planes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarPantalla()
        {
            if (_mode == FormMode.Add)
            {
                this.Text = "Nueva Materia";
                idTextBox.Visible = false;
                idLabel.Visible = false;
                hsSemanalesNumericUpDown.Value = 4;
                hsTotalesNumericUpDown.Value = 64;
            }
            else if (_mode == FormMode.Update)
            {
                this.Text = "Modificar Materia";
                idTextBox.Visible = true;
                idLabel.Visible = true;
                idTextBox.Text = _materia.ID.ToString();
                descripcionTextBox.Text = _materia.Descripcion;

                if (_materia.HSSemanales > hsSemanalesNumericUpDown.Maximum)
                {
                    hsSemanalesNumericUpDown.Maximum = _materia.HSSemanales;
                }
                hsSemanalesNumericUpDown.Value = Math.Max(hsSemanalesNumericUpDown.Minimum, _materia.HSSemanales);

                if (_materia.HSTotales > hsTotalesNumericUpDown.Maximum)
                {
                    hsTotalesNumericUpDown.Maximum = _materia.HSTotales;
                }
                hsTotalesNumericUpDown.Value = Math.Max(hsTotalesNumericUpDown.Minimum, _materia.HSTotales);
            }

            if (_materia.IDPlan > 0)
            {
                planComboBox.SelectedValue = _materia.IDPlan;
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                MessageBox.Show("La descripción de la materia es requerida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (hsSemanalesNumericUpDown.Value <= 0)
            {
                MessageBox.Show("Las horas semanales deben ser mayores a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (hsTotalesNumericUpDown.Value <= 0)
            {
                MessageBox.Show("Las horas totales deben ser mayores a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (planComboBox.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un Plan.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _materia.Descripcion = descripcionTextBox.Text.Trim();
            _materia.HSSemanales = (int)hsSemanalesNumericUpDown.Value;
            _materia.HSTotales = (int)hsTotalesNumericUpDown.Value;
            _materia.IDPlan = (int)planComboBox.SelectedValue;

            try
            {
                aceptarButton.Enabled = false;

                if (_mode == FormMode.Add)
                {
                    await MateriaApiClient.AddAsync(_materia);
                }
                else if (_mode == FormMode.Update)
                {
                    var ok = await MateriaApiClient.UpdateAsync(_materia);
                    if (!ok) throw new Exception("No se pudo actualizar la materia.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la materia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                aceptarButton.Enabled = true;
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
