using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsForms
{
    public partial class PlanLista : Form
    {
        public PlanLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
            ConfigurarAnchors();
            Init();
        }

        private async void Init()
        {
            await CargarGrilla();
        }

        private void ConfigurarAnchors()
        {
            planDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizaButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }
        private void ConfigurarColumnas()
        {
            planDataGridView.AutoGenerateColumns = false;
            planDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            planDataGridView.Columns.Clear();
            planDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                DataPropertyName = "ID",
                FillWeight = 15
            });
            planDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Plan",
                DataPropertyName = "Descripcion",
                FillWeight = 45
            });
            planDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EspecialidadDescripcion",
                HeaderText = "Especialidad",
                DataPropertyName = "EspecialidadDescripcion",
                FillWeight = 30
            });
            planDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadMaterias",
                HeaderText = "Materias",
                DataPropertyName = "CantidadMaterias",
                FillWeight = 15
            });
        }

        private async Task CargarGrilla()
        {
            try
            {
                DeshabilitarControles();
                var planes = await PlanApiClient.GetAllAsync();
                planDataGridView.DataSource = planes.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los planes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            var formDetalle = new PlanDetalle(FormMode.Add, new PlanDTO());
            if (formDetalle.ShowDialog(this) == DialogResult.OK)
            {
                await CargarGrilla(); // Refresca para mostrar el nuevo plan
            }
        }

        private async void actualizaButton_Click(object sender, EventArgs e)
        {
            if (planDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un plan de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DeshabilitarControles();
                var filaSeleccionada = planDataGridView.SelectedRows[0];
                var planSeleccionado = (PlanDTO)filaSeleccionada.DataBoundItem;

                var planCompleto = await PlanApiClient.GetByIdAsync(planSeleccionado.ID);
                if (planCompleto == null)
                {
                    MessageBox.Show("No se pudo obtener el plan seleccionado desde el servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var formDetalle = new PlanDetalle(FormMode.Update, planCompleto);
                if (formDetalle.ShowDialog(this) == DialogResult.OK)
                {
                    await CargarGrilla(); // Refresca con los cambios
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el plan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (planDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un plan de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var filaSeleccionada = planDataGridView.SelectedRows[0];
            var planSeleccionado = (PlanDTO)filaSeleccionada.DataBoundItem;
            var confirm = MessageBox.Show($"¿Seguro que desea eliminar el plan '{planSeleccionado.Descripcion}' junto con todas sus materias?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    var ok = await PlanApiClient.DeleteAsync(planSeleccionado.ID);
                    if (!ok)
                    {
                        MessageBox.Show("No se pudo eliminar el plan (puede tener comisiones, cursos o personas asociadas).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    await CargarGrilla();
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
            actualizaButton.Enabled = false;
            eliminarButton.Enabled = false;
            planDataGridView.Enabled = false;
        }

        private void HabilitarControles()
        {
            agregarButton.Enabled = true;
            planDataGridView.Enabled = true;
            bool hasItems = planDataGridView.Rows.Count > 0;
            actualizaButton.Enabled = hasItems;
            eliminarButton.Enabled = hasItems;
        }
    }
}
