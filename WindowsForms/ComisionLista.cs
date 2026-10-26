using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class ComisionLista : Form
    {
        public ComisionLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
            ConfigurarAnchors();
        }


        private void ConfigurarAnchors()
        {
            comisionDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }
        private void ConfigurarColumnas()
        {
            comisionDataGridView.AutoGenerateColumns = false;
            comisionDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            comisionDataGridView.Columns.Clear(); // Reiniciamos las columnas del DataGridView

            comisionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                DataPropertyName = "ID",
                Visible = false // Ocultamos la columna ID por motivos estéticos
            });

            comisionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Comisión",
                DataPropertyName = "Descripcion",
            });

            comisionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AnioEspecialidad",
                HeaderText = "Año Carrera",
                DataPropertyName = "AnioEspecialidad",
            });

            comisionDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanDescripcion",
                HeaderText = "Plan",
                DataPropertyName = "PlanDescripcion",
            });
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (comisionDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná una comisión de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filaSeleccionada = comisionDataGridView.SelectedRows[0];
            var comisionSeleccionada = (DTOs.ComisionDTO)filaSeleccionada.DataBoundItem;

            var formDetalle = new ComisionDetalle(FormMode.Update, comisionSeleccionada);
            if (formDetalle.ShowDialog(this) == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (comisionDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná una comisión de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filaSeleccionada = comisionDataGridView.SelectedRows[0];
            var comisionSeleccionada = (DTOs.ComisionDTO)filaSeleccionada.DataBoundItem;

            var respuesta = MessageBox.Show($"¿Seguro que querés eliminar la comisión '{comisionSeleccionada.Descripcion}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                var ok = await ComisionApiClient.DeleteAsync(comisionSeleccionada.ID);
                if (!ok)
                {
                    MessageBox.Show("No se pudo eliminar la comisión (puede tener cursos asociados).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                await CargarGrilla();
            }
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            var formDetalle = new ComisionDetalle(FormMode.Add, new DTOs.ComisionDTO());
            if (formDetalle.ShowDialog(this) == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void ComisionLista_Load(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async Task CargarGrilla()
        {
            IEnumerable<ComisionDTO> comisiones;

            if (string.IsNullOrWhiteSpace(buscarTextBox.Text))
            {
                comisiones = await ComisionApiClient.GetAllAsync();
            }
            else
            {
                comisiones = await ComisionApiClient.GetByCriteriaAsync(buscarTextBox.Text.Trim());
            }

            comisionDataGridView.DataSource = comisiones.ToList();
        }
    }
}
