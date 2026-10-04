using API.Clients;
using DTOs;
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
                    var authClient = new AuthApiClient();
                    var request = new LoginRequest
                    {
                        Username = usernameTextBox.Text,
                        Password = passwordTextBox.Text
                    };
                    // Ejecutamos la solicitud de autenticación al servidor
                    var response = await authClient.LoginAsync(request);
                    if (response != null && !string.IsNullOrEmpty(response.Token))
                    {
                        // Almacenamos el token JWT en el cliente base para las siguientes peticiones
                        BaseApiClient.Token = response.Token;
                        MessageBox.Show($"¡Bienvenido {response.Username}!", "Éxito");
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


