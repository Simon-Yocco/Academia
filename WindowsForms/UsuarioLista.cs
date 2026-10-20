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
                Name = "Legajo",
                HeaderText = "Legajo",
                DataPropertyName = "Legajo",
            });

            usuarioDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Apellido",
                HeaderText = "Apellido",
                DataPropertyName = "Apellido",
            });

            usuarioDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
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
                DeshabilitarControles();
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
            finally
            {
                HabilitarControles();
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
            if (usuarioDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná un usuario de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DeshabilitarControles();
                var fila = usuarioDataGridView.SelectedRows[0];
                var usuarioSeleccionado = (UsuarioDTO)fila.DataBoundItem;

                var usuarioCompleto = await UsuarioApiClient.GetByIdAsync(usuarioSeleccionado.ID);
                if (usuarioCompleto == null)
                {
                    MessageBox.Show("No se pudo obtener el usuario seleccionado desde el servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var form = new UsuarioDetalle(FormMode.Update, usuarioCompleto);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    await CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el usuario para edición: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
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
                    DeshabilitarControles();
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
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private void DeshabilitarControles()
        {
            agregarButton.Enabled = false;
            modificarButton.Enabled = false;
            eliminarButton.Enabled = false;
            buscarButton.Enabled = false;
            buscarTextBox.Enabled = false;
            usuarioDataGridView.Enabled = false;
        }

        private void HabilitarControles()
        {
            agregarButton.Enabled = true;
            buscarButton.Enabled = true;
            buscarTextBox.Enabled = true;
            usuarioDataGridView.Enabled = true;
            bool hasItems = usuarioDataGridView.Rows.Count > 0;
            modificarButton.Enabled = hasItems;
            eliminarButton.Enabled = hasItems;
        }
    }
}
