using API.Clients;
using DTOs;
using System.Data;

namespace WindowsForms
{
    public partial class MateriaCursos : Form
    {
        public MateriaCursos()
        {
            InitializeComponent();

            // Inicialización de componentes de la interfaz
            Init();

            // Suscripción programática al evento de cambio de selección
            materiaComboBox.SelectedIndexChanged += materiaComboBox_SelectedIndexChanged;
        }

        private async void Init()
        {
            try
            {
                var materias = await MateriaApiClient.GetAllAsync();

                // Se suspende temporalmente el evento SelectedIndexChanged durante la carga de datos
                materiaComboBox.SelectedIndexChanged -= materiaComboBox_SelectedIndexChanged;
                
                materiaComboBox.DataSource = materias.ToList();
                materiaComboBox.DisplayMember = "Descripcion";
                materiaComboBox.ValueMember = "ID";
                materiaComboBox.SelectedIndex = -1;
                
                // Se restaura la suscripción al evento
                materiaComboBox.SelectedIndexChanged += materiaComboBox_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las materias: " + ex.Message);
            }
        }

        private async void materiaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (materiaComboBox.SelectedItem is MateriaDTO materiaSeleccionada)
            {
                try
                {
                    var todosLosCursos = await CursoApiClient.GetAllAsync();
                    var cursosDeEstaMateria = todosLosCursos.Where(c => c.IDmateria == materiaSeleccionada.ID).ToList();

                    cursosDataGridView.DataSource = null;
                    cursosDataGridView.DataSource = cursosDeEstaMateria;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los cursos: " + ex.Message);
                }
            }
        }

        private void agregarCursobutton_Click(object sender, EventArgs e)
        {
            if (materiaComboBox.SelectedItem is MateriaDTO materiaSeleccionada)
            {
                var cursoNuevo = new CursoDTO
                {
                    IDmateria = materiaSeleccionada.ID
                };

                var formDetalle = new CursoDetalle(FormMode.Add, cursoNuevo);

                if (formDetalle.ShowDialog() == DialogResult.OK)
                {
                    // Disparamos el evento de selección para refrescar la grilla de cursos
                    materiaComboBox_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Primero tenés que seleccionar una materia arriba.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
