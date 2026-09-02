using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application.Services;
using Data;

namespace WindowsForms
{
    public partial class EspecialidadLista : Form
    {
        private readonly IEspecialidadService _especialidadService;
        public EspecialidadLista()
        {
            InitializeComponent();

            // Preparamos el servicio
            var repository = new EspecialidadRepository();
            _especialidadService = new EspecialidadService(repository);
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            await CargarGrilla();
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            // 1. Verificamos que haya seleccionado algo
            if (especialidadesDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná una especialidad de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // 2. Extraemos el DTO
            var filaSeleccionada = especialidadesDataGridView.SelectedRows[0];
            var especialidadSeleccionada = (DTOs.EspecialidadDTO)filaSeleccionada.DataBoundItem;
            // 3. Le preguntamos si está seguro
            var respuesta = MessageBox.Show($"¿Seguro que querés eliminar la especialidad '{especialidadSeleccionada.Descripcion}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                // 4. Le avisamos al servicio que lo borre de la base de datos usando el ID
                await _especialidadService.DeleteAsync(especialidadSeleccionada.ID);

                // 5. Refrescar la grilla
                await CargarGrilla();
            }
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            // Verificamos que el usuario haya seleccionado una fila
            if (especialidadesDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccioná una especialidad de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Extraemos el DTO de la fila seleccionada
            var filaSeleccionada = especialidadesDataGridView.SelectedRows[0];
            var especialidadSeleccionada = (DTOs.EspecialidadDTO)filaSeleccionada.DataBoundItem;
            // Le pasamos FormMode.Update y el DTO con datos
            var formDetalle = new EspecialidadDetalle(FormMode.Update, especialidadSeleccionada);
            if (formDetalle.ShowDialog() == DialogResult.OK)
            {
                await CargarGrilla();
            }
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            // Le pasamos FormMode.Add y un DTO vacío
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
            // 1. Traemos TODAS las especialidades de la base de datos
            var especialidades = await _especialidadService.GetAllAsync();
            // 2. Nos fijamos si el usuario escribió algo en el buscador
            string textoBuscado = buscarTextBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(textoBuscado))
            {
                // 3. Filtramos la lista: nos quedamos solo con las que contengan el texto
                especialidades = especialidades
                    .Where(e => e.Descripcion.ToLower().Contains(textoBuscado))
                    .ToList();
            }
            // 4. Se las pasamos a la grilla para que las dibuje (ya sean todas o las filtradas)
            especialidadesDataGridView.DataSource = especialidades;
        }
    }
}
