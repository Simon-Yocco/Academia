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
            Init();
        }

        private async void Init()
        {
            await CargarGrilla();
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
                var planes = await PlanApiClient.GetAllAsync();
                // Proyectamos una columna extra para mostrar la especialidad y cuántas materias tiene cada plan
                var listaVisual = planes.Select(p => new
                {
                    ID = p.ID,
                    Descripcion = p.Descripcion,
                    EspecialidadDescripcion = !string.IsNullOrEmpty(p.EspecialidadDescripcion) ? p.EspecialidadDescripcion : p.IDEspecialidad.ToString(),
                    CantidadMaterias = p.Materias != null ? p.Materias.Count : 0,
                    PlanOriginal = p
                }).ToList();
                planDataGridView.DataSource = listaVisual;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los planes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Tomamos el objeto del plan seleccionado
            dynamic fila = planDataGridView.SelectedRows[0].DataBoundItem;
            PlanDTO planSeleccionado = (PlanDTO)fila.PlanOriginal;
            var formDetalle = new PlanDetalle(FormMode.Update, planSeleccionado);
            if (formDetalle.ShowDialog(this) == DialogResult.OK)
            {
                await CargarGrilla(); // Refresca con los cambios
            }
        }
        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (planDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un plan de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dynamic fila = planDataGridView.SelectedRows[0].DataBoundItem;
            PlanDTO planSeleccionado = (PlanDTO)fila.PlanOriginal;
            var confirm = MessageBox.Show($"¿Seguro que desea eliminar el plan '{planSeleccionado.Descripcion}' junto con todas sus materias?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await PlanApiClient.DeleteAsync(planSeleccionado.ID);
                    await CargarGrilla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
