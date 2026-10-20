using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;

namespace WindowsForms
{
    // 1. Declaramos los modos posibles para esta pantalla
    public enum FormMode
    {
        Add,
        Update
    }
    public partial class EspecialidadDetalle : Form
    {
        private EspecialidadDTO _especialidad = new();
        private FormMode _mode;

        public EspecialidadDetalle(FormMode mode, EspecialidadDTO especialidad)
        {
            InitializeComponent();
            
            _mode = mode;
            _especialidad = especialidad;

            ConfigurarPantalla();
        }
        // Este constructor vacío lo necesita Visual Studio para el diseñador visual.
        public EspecialidadDetalle()
        {
            InitializeComponent();
        }
        private void ConfigurarPantalla()
        {
            if (_mode == FormMode.Add)
            {
                // Si es agregar, ocultamos el ID porque la BD lo genera solo
                idTextBox.Visible = false;
                idLabel.Visible = false; 
            }
            else if (_mode == FormMode.Update)
            {
                // Si es editar, mostramos el ID y rellenamos los text boxes.
                idTextBox.Visible = true;
                idTextBox.Text = _especialidad.ID.ToString();
                descripcionTextBox.Text = _especialidad.Descripcion;
            }
        }
        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            // Validamos que no esté vacío
            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                MessageBox.Show("La descripción es requerida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Pasamos lo que escribió el usuario a nuestro DTO
            _especialidad.Descripcion = descripcionTextBox.Text;
            // Dependiendo del modo, le decimos al servicio qué hacer
            try
            {
                aceptarButton.Enabled = false;
                if (_mode == FormMode.Add)
                {
                    await EspecialidadApiClient.AddAsync(_especialidad);
                }
                else if (_mode == FormMode.Update)
                {
                    var ok = await EspecialidadApiClient.UpdateAsync(_especialidad);
                    if (!ok)
                    {
                        throw new Exception("No se pudo actualizar la especialidad.");
                    }
                }
                // Cerramos avisando que todo salió bien
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                aceptarButton.Enabled = true;
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
