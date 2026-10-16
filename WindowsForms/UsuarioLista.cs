using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class UsuarioLista : Form
    {
        public UsuarioLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            usuarioDataGridView.AutoGenerateColumns = false;
            usuarioDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            usuarioDataGridView.Columns.Clear();

            usuarioDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                DataPropertyName = "ID",
                Visible = false
            });

            usuarioDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LEGAJO",
                HeaderText = "LEGAJO",
                DataPropertyName = "Legajo",
            });

            usuarioDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "APELLIDO",
                HeaderText = "APELLIDO",
                DataPropertyName = "Apellido",
            });

            usuarioDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NOMBRE",
                HeaderText = "NOMBRE",
                DataPropertyName = "Nombre",
            });

            usuarioDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Rol",
                HeaderText = "Tipo / Rol",
                DataPropertyName = "Rol"
            });

            usuarioDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreUsuario",
                HeaderText = "Usuario",
                DataPropertyName = "NombreUsuario"
            });

            usuarioDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email"
            });

            usuarioDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Habilitado",
                HeaderText = "Habilitado",
                DataPropertyName = "Habilitado"
            });
        }

        private async void UsuarioLista_Load(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async Task CargarGrilla()
        {
            try
            {
                var usuarios = await UsuarioApiClient.GetAllAsync();
                // Filtro local por texto (busca por nombre, apellido, usuario, rol o legajo)
                if (!string.IsNullOrWhiteSpace(buscarTextBox.Text))
                {
                    var filtro = buscarTextBox.Text.Trim().ToLower();
                    usuarios = usuarios.Where(u =>
                        u.Nombre.ToLower().Contains(filtro) ||
                        u.Apellido.ToLower().Contains(filtro) ||
                        u.NombreUsuario.ToLower().Contains(filtro) ||
                        u.Rol.ToLower().Contains(filtro) ||
                        u.Legajo.ToString().Contains(filtro)
                    );
                }
                usuarioDataGridView.DataSource = usuarios.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            var form = new UsuarioDetalle(FormMode.Add, new UsuarioDTO());
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void modificarButton_Click(object sender, EventArgs e)
        {
            // Si no selecciona ningún usuario
            if (usuarioDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná un usuario de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fila = usuarioDataGridView.SelectedRows[0];
            var usuarioSeleccionado = (UsuarioDTO)fila.DataBoundItem;

            var form = new UsuarioDetalle(FormMode.Update, usuarioSeleccionado);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {

            // misma validacion para ver que este seleccionada una fila
            if (usuarioDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná un usuario de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fila = usuarioDataGridView.SelectedRows[0];
            var usuarioSeleccionado = (UsuarioDTO)fila.DataBoundItem;
            var rta = MessageBox.Show($"¿Seguro que querés eliminar al usuario '{usuarioSeleccionado.NombreUsuario}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rta == DialogResult.Yes)
            {
                try
                {
                    var ok = await UsuarioApiClient.DeleteAsync(usuarioSeleccionado.ID);
                    if (ok)
                    {
                        await CargarGrilla();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
