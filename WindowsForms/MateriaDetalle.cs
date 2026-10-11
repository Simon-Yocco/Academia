using DTOs;
using System;
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
            _mode = mode;
            _materia = materia ?? new MateriaDTO();
            ConfigurarPantalla();
        }
        private void ConfigurarPantalla()
        {
            if (_mode == FormMode.Update)
            {
                descripcionTextBox.Text = _materia.Descripcion;
                hsSemanalesNumericUpDown.Value = _materia.HSSemanales;
                hsTotalesNumericUpDown.Value = _materia.HSTotales;
            }
        }
        private void aceptarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                MessageBox.Show("La descripción de la materia es requerida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (hsSemanalesNumericUpDown.Value <= 0)
            {
                MessageBox.Show("Las horas semanales deben ser mayores a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (hsTotalesNumericUpDown.Value <= 0)
            {
                MessageBox.Show("Las horas totales deben ser mayores a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _materia.Descripcion = descripcionTextBox.Text.Trim();
            _materia.HSSemanales = (int)hsSemanalesNumericUpDown.Value;
            _materia.HSTotales = (int)hsTotalesNumericUpDown.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
