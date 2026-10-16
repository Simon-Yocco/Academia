using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class MateriaLista : Form
    {
        public MateriaLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            materiaDataGridView.AutoGenerateColumns = false;
            materiaDataGridView.Columns.Clear();

            materiaDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                DataPropertyName = "ID",
                Visible = false
            });

            materiaDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Materia",
                DataPropertyName = "Descripcion",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            materiaDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HSSemanales",
                HeaderText = "Hs Semanales",
                DataPropertyName = "HSSemanales",
                Width = 110
            });

            materiaDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HSTotales",
                HeaderText = "Hs Totales",
                DataPropertyName = "HSTotales",
                Width = 110
            });

            materiaDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanDescripcion",
                HeaderText = "Plan",
                DataPropertyName = "PlanDescripcion",
                Width = 200
            });
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (materiaDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná una materia de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filaSeleccionada = materiaDataGridView.SelectedRows[0];
            var materiaSeleccionada = (MateriaDTO)filaSeleccionada.DataBoundItem;

            var formDetalle = new MateriaDetalle(FormMode.Update, materiaSeleccionada);
            if (formDetalle.ShowDialog(this) == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (materiaDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná una materia de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filaSeleccionada = materiaDataGridView.SelectedRows[0];
            var materiaSeleccionada = (MateriaDTO)filaSeleccionada.DataBoundItem;

            var respuesta = MessageBox.Show($"¿Seguro que querés eliminar la materia '{materiaSeleccionada.Descripcion}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    var ok = await MateriaApiClient.DeleteAsync(materiaSeleccionada.ID);
                    if (ok)
                    {
                        await CargarGrilla();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la materia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            var formDetalle = new MateriaDetalle(FormMode.Add, new MateriaDTO());
            if (formDetalle.ShowDialog(this) == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void MateriaLista_Load(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async Task CargarGrilla()
        {
            try
            {
                IEnumerable<MateriaDTO> materias;

                if (string.IsNullOrWhiteSpace(buscarTextBox.Text))
                {
                    materias = await MateriaApiClient.GetAllAsync();
                }
                else
                {
                    materias = await MateriaApiClient.GetByCriteriaAsync(buscarTextBox.Text.Trim());
                }

                materiaDataGridView.DataSource = materias.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar materias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
