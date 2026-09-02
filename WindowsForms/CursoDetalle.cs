using Application.Services;
using Data;
using DTOs;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsForms
{
    public partial class CursoDetalle : Form
    {
        private CursoDTO _curso;
        private FormMode _mode;
        private ICursoService _cursoService;

        public CursoDetalle(FormMode mode, CursoDTO curso)
        {
            InitializeComponent();
            // Preparamos nuestro servicio
            _cursoService = new CursoService(new CursoRepository());

            _mode = mode;
            _curso = curso;

            ConfigurarPantalla();
        }
        // Este constructor vacío lo necesita Visual Studio para el diseñador visual.
        public CursoDetalle()
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
                idTextBox.Text = _curso.ID.ToString();
                descripcionTextBox.Text = _curso.Descripcion;
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
            _curso.AnioCalendario = int.Parse(anioCalendarioTextBox.Text);
            _curso.Cupo = int.Parse(cupoTextBox.Text);
            _curso.Descripcion = descripcionTextBox.Text;
            // Dependiendo del modo, le decimos al servicio qué hacer
            if (_mode == FormMode.Add)
            {
                await _cursoService.AddAsync(_curso);
            }
            else if (_mode == FormMode.Update)
            {
                await _cursoService.UpdateAsync(_curso);
            }
            // Cerramos avisando que todo salió bien
            this.DialogResult = DialogResult.OK;
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
