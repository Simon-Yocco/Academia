using API.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class EspecialidadLista : Form
    {
        public EspecialidadLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            especialidadesDataGridView.AutoGenerateColumns = false;
            especialidadesDataGridView.Columns.Clear();

            especialidadesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                DataPropertyName = "ID",
                Width = 50,
                Visible = false // Ocultamos la columna ID
            });

            especialidadesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Especialidad",
                DataPropertyName = "Descripcion",
                Width = 250
            });
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (especialidadesDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná una especialidad de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var filaSeleccionada = especialidadesDataGridView.SelectedRows[0];
            var especialidadSeleccionada = (DTOs.EspecialidadDTO)filaSeleccionada.DataBoundItem;
            
            var respuesta = MessageBox.Show($"¿Seguro que querés eliminar la especialidad '{especialidadSeleccionada.Descripcion}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                await EspecialidadApiClient.DeleteAsync(especialidadSeleccionada.ID);
                await CargarGrilla();
            }
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (especialidadesDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná una especialidad de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var filaSeleccionada = especialidadesDataGridView.SelectedRows[0];
            var especialidadSeleccionada = (DTOs.EspecialidadDTO)filaSeleccionada.DataBoundItem;
            
            var formDetalle = new EspecialidadDetalle(FormMode.Update, especialidadSeleccionada);
            if (formDetalle.ShowDialog() == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            var formDetalle = new EspecialidadDetalle(FormMode.Add, new DTOs.EspecialidadDTO());

            if (formDetalle.ShowDialog() == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void EspecialidadLista_Load(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async Task CargarGrilla()
        {
            IEnumerable<DTOs.EspecialidadDTO> especialidades;
            string textoBuscado = buscarTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(textoBuscado))
            {
                especialidades = await EspecialidadApiClient.GetAllAsync();
            }
            else
            {
                especialidades = await EspecialidadApiClient.GetByCriteriaAsync(textoBuscado);
            }
            
            especialidadesDataGridView.DataSource = especialidades;
        }
    }
}
