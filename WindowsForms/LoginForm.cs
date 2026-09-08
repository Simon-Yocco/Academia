using API.Clients;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    loginButton.Enabled = false;
                    loginButton.Text = "Iniciando sesión...";

                    string usuario = usernameTextBox.Text;
                    string clave = passwordTextBox.Text;

                    var usuarioLogueado = await UsuarioApiClient.LoginAsync(usuario, clave);

                    if (usuarioLogueado != null)
                    {
                        MessageBox.Show($"¡Bienvenido {usuarioLogueado.Nombre}!", "Éxito");
                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.", "Error");
                        passwordTextBox.Clear();
                        passwordTextBox.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo conectar con el servidor: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Si llegó hasta acá (falló el login o la conexión), volvemos a la normalidad
                loginButton.Enabled = true;
                loginButton.Text = "Iniciar";
            }
        }
        private bool ValidateInput()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                MessageBox.Show("El usuario es requerido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("La contraseña es requerida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            return isValid;
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
