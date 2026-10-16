using API.Clients;
using DTOs;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class UsuarioDetalle : Form
    {
        private UsuarioDTO _usuario;
        private FormMode _mode;

        public UsuarioDTO Usuario => _usuario;

        public UsuarioDetalle()
        {
            InitializeComponent();
        }

        public UsuarioDetalle(FormMode mode, UsuarioDTO usuario) : this()
        {
            Init(mode, usuario);
        }

        private async void Init(FormMode mode, UsuarioDTO usuario)
        {
            _mode = mode;
            _usuario = usuario ?? new UsuarioDTO();

            aceptarButton.Enabled = false;

            CargarTiposPersona();
            await CargarPlanes();
            await ConfigurarPantallaAsync();

            aceptarButton.Enabled = true;
        }

        private void CargarTiposPersona()
        {
            tipoPersonaComboBox.Items.Clear();
            tipoPersonaComboBox.Items.Add("Alumno");
            tipoPersonaComboBox.Items.Add("Docente");
            tipoPersonaComboBox.Items.Add("Administrador");
            tipoPersonaComboBox.SelectedIndex = 0; // "Alumno" por defecto
        }

        private async Task CargarPlanes()
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
                MessageBox.Show($"Error al cargar planes: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task ConfigurarPantallaAsync()
        {
            legajoNumericUpDown.Enabled = false; // El legajo se asigna automáticamente de forma autoincremental

            if (_mode == FormMode.Add)
            {
                this.Text = "Nuevo Usuario (Alumno / Profesor)";
                fechaNacimientoDateTimePicker.Value = DateTime.Today.AddYears(-20);
                habilitadoCheckBox.Checked = true;

                try
                {
                    var proximoLegajo = await UsuarioApiClient.GetProximoLegajoAsync();
                    legajoNumericUpDown.Value = proximoLegajo;
                }
                catch
                {
                    legajoNumericUpDown.Value = 1001;
                }
            }
            else if (_mode == FormMode.Update)
            {
                this.Text = "Modificar Usuario";

                // Datos de la Persona
                nombreTextBox.Text = _usuario.Persona.Nombre;
                apellidoTextBox.Text = _usuario.Persona.Apellido;
                legajoNumericUpDown.Value = _usuario.Persona.Legajo > 0 ? _usuario.Persona.Legajo : 1;
                emailTextBox.Text = _usuario.Persona.Email;
                telefonoTextBox.Text = _usuario.Persona.Telefono;
                direccionTextBox.Text = _usuario.Persona.Direccion;

                if (_usuario.Persona.FechaNacimiento != default && _usuario.Persona.FechaNacimiento <= DateTime.Today)
                {
                    fechaNacimientoDateTimePicker.Value = _usuario.Persona.FechaNacimiento;
                }

                if (!string.IsNullOrEmpty(_usuario.Persona.TipoPersona))
                {
                    tipoPersonaComboBox.SelectedItem = _usuario.Persona.TipoPersona;
                }

                if (_usuario.Persona.IDPlan.HasValue && _usuario.Persona.IDPlan > 0)
                {
                    planComboBox.SelectedValue = _usuario.Persona.IDPlan.Value;
                }

                habilitadoCheckBox.Checked = _usuario.Habilitado;
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            // Validaciones de interfaz
            if (string.IsNullOrWhiteSpace(nombreTextBox.Text) || string.IsNullOrWhiteSpace(apellidoTextBox.Text))
            {
                MessageBox.Show("El nombre y apellido son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var email = emailTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("El correo electrónico es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("El formato del correo electrónico no es válido (ejemplo: usuario@dominio.com).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(direccionTextBox.Text))
            {
                MessageBox.Show("La dirección es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(telefonoTextBox.Text))
            {
                MessageBox.Show("El teléfono es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (fechaNacimientoDateTimePicker.Value > DateTime.Today)
            {
                MessageBox.Show("La fecha de nacimiento no puede ser posterior a la fecha actual.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Asignar datos personales a PersonaDTO
            _usuario.Persona.Nombre = nombreTextBox.Text.Trim();
            _usuario.Persona.Apellido = apellidoTextBox.Text.Trim();
            _usuario.Persona.Legajo = (int)legajoNumericUpDown.Value;
            _usuario.Persona.Email = email;
            _usuario.Persona.Telefono = telefonoTextBox.Text.Trim();
            _usuario.Persona.Direccion = direccionTextBox.Text.Trim();
            _usuario.Persona.FechaNacimiento = fechaNacimientoDateTimePicker.Value;
            _usuario.Persona.TipoPersona = tipoPersonaComboBox.SelectedItem?.ToString() ?? "Alumno";

            if (planComboBox.SelectedValue != null && (int)planComboBox.SelectedValue > 0)
            {
                _usuario.Persona.IDPlan = (int)planComboBox.SelectedValue;
            }
            else
            {
                _usuario.Persona.IDPlan = null;
            }

            // 2. Asignación automática de Usuario y Contraseña
            if (_mode == FormMode.Add)
            {
                // Usuario = nombre en minúsculas
                _usuario.NombreUsuario = nombreTextBox.Text.Trim().ToLower();
                // Contraseña = su número de legajo
                _usuario.Clave = legajoNumericUpDown.Value.ToString();
            }
            else if (_mode == FormMode.Update)
            {
                // En modificación, si no tenía usuario o clave, los aseguramos
                if (string.IsNullOrWhiteSpace(_usuario.NombreUsuario))
                {
                    _usuario.NombreUsuario = nombreTextBox.Text.Trim().ToLower();
                }
                if (string.IsNullOrWhiteSpace(_usuario.Clave))
                {
                    _usuario.Clave = legajoNumericUpDown.Value.ToString();
                }
            }

            _usuario.Habilitado = habilitadoCheckBox.Checked;

            // 3. Persistir a través de la API
            try
            {
                aceptarButton.Enabled = false;

                if (_mode == FormMode.Add)
                {
                    await UsuarioApiClient.AddAsync(_usuario);
                }
                else if (_mode == FormMode.Update)
                {
                    var ok = await UsuarioApiClient.UpdateAsync(_usuario);
                    if (!ok) throw new Exception("No se pudo actualizar el usuario.");
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                aceptarButton.Enabled = true;
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
