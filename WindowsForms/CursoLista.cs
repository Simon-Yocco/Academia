using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class CursoLista : Form
    {
        public CursoLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            cursosDataGridView.AutoGenerateColumns = false;
            cursosDataGridView.Columns.Clear(); // Reiniciamos las columnas del DataGridView

            cursosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                DataPropertyName = "ID",
                Visible = false // Ocultamos la columna ID por motivos estéticos
            });

            cursosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Curso",
                DataPropertyName = "Descripcion",
            });

            cursosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MateriaDescripcion",
                HeaderText = "Materia",
                DataPropertyName = "MateriaDescripcion",
            });

            cursosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ComisionDescripcion",
                HeaderText = "Comisión",
                DataPropertyName = "ComisionDescripcion",
            });

            cursosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AnioCalendario",
                HeaderText = "Año",
                DataPropertyName = "AnioCalendario",
            });

            cursosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cupo",
                HeaderText = "Cupo",
                DataPropertyName = "Cupo",
            });
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (cursosDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná un curso de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filaSeleccionada = cursosDataGridView.SelectedRows[0];
            var cursoSeleccionado = (DTOs.CursoDTO)filaSeleccionada.DataBoundItem;

            var formDetalle = new CursoDetalle(FormMode.Update, cursoSeleccionado);
            if (formDetalle.ShowDialog() == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (cursosDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná un curso de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filaSeleccionada = cursosDataGridView.SelectedRows[0];
            var cursoSeleccionado = (DTOs.CursoDTO)filaSeleccionada.DataBoundItem;

            var respuesta = MessageBox.Show($"¿Seguro que querés eliminar el curso '{cursoSeleccionado.Descripcion}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                await CursoApiClient.DeleteAsync(cursoSeleccionado.ID);
                await CargarGrilla();
            }
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            var formDetalle = new CursoDetalle(FormMode.Add, new DTOs.CursoDTO());
            if (formDetalle.ShowDialog() == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void CursoLista_Load(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async Task CargarGrilla()
        {
            IEnumerable<CursoDTO> cursos;

            if (string.IsNullOrWhiteSpace(buscarTextBox.Text))
            {
                cursos = await CursoApiClient.GetAllAsync();
            }
            else
            {
                cursos = await CursoApiClient.GetByCriteriaAsync(buscarTextBox.Text);
            }

            cursosDataGridView.DataSource = cursos;
        }
    }
}
