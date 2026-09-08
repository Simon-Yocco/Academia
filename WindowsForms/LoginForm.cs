using API.Clients;

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
            // 1. Agarramos lo que el usuario escribió en las cajitas de texto
            string usuario = usernameTextBox.Text;
            string clave = passwordTextBox.Text;
            // 2. Le pedimos a nuestro servicio que intente hacer el login
            var usuarioLogueado = await UsuarioApiClient.LoginAsync(usuario, clave);
            // 3. Vemos qué pasó
            if (usuarioLogueado != null)
            {
                MessageBox.Show($"¡Bienvenido {usuarioLogueado.Nombre}!", "Éxito");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
