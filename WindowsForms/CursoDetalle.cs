using API.Clients;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class CursoDetalle : Form
    {
        private CursoDTO _curso;
        private FormMode _mode;

        public CursoDetalle(FormMode mode, CursoDTO curso) : this()
        {
            Init(mode, curso);
        }

        public CursoDetalle()
        {
            InitializeComponent();
        }

        private async void Init(FormMode mode, CursoDTO curso)
        {
            _mode = mode;
            _curso = curso;

            aceptarButton.Enabled = false; // Bloqueamos para evitar errores mientras carga

            await LoadCombos();
            ConfigurarPantalla();

            aceptarButton.Enabled = true;
        }

        private async Task LoadCombos()
        {
            try
            {
                var materias = await MateriaApiClient.GetAllAsync();
                materiaComboBox.DataSource = materias.ToList();
                materiaComboBox.DisplayMember = "Descripcion";
                materiaComboBox.ValueMember = "ID";
                materiaComboBox.SelectedIndex = -1; // Por defecto vacío

                var comisiones = await ComisionApiClient.GetAllAsync();
                comisionComboBox.DataSource = comisiones.ToList();
                comisionComboBox.DisplayMember = "Descripcion";
                comisionComboBox.ValueMember = "ID";
                comisionComboBox.SelectedIndex = -1; // Por defecto vacío
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
            }
            else if (_mode == FormMode.Update)
            {
                idTextBox.Visible = true;
                idTextBox.Text = _curso.ID.ToString();
                anioCalendarioTextBox.Text = _curso.AnioCalendario.ToString();
                cupoTextBox.Text = _curso.Cupo.ToString();
                descripcionTextBox.Text = _curso.Descripcion;
                
            }

            if (_curso.IDmateria > 0) materiaComboBox.SelectedValue = _curso.IDmateria;
            if (_curso.IDcomision > 0) comisionComboBox.SelectedValue = _curso.IDcomision;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                MessageBox.Show("La descripción es requerida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (materiaComboBox.SelectedValue == null || comisionComboBox.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una Materia y una Comisión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _curso.AnioCalendario = int.Parse(anioCalendarioTextBox.Text);
            _curso.Cupo = int.Parse(cupoTextBox.Text);
            _curso.Descripcion = descripcionTextBox.Text;
            _curso.IDmateria = (int)materiaComboBox.SelectedValue;
            _curso.IDcomision = (int)comisionComboBox.SelectedValue;

            try
            {
                aceptarButton.Enabled = false;
                if (_mode == FormMode.Add)
                {
                    await CursoApiClient.AddAsync(_curso);
                }
                else if (_mode == FormMode.Update)
                {
                    await CursoApiClient.UpdateAsync(_curso);
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
