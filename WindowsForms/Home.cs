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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Instanciamos el formulario correspondiente
            var ventanaCursos = new CursoLista();
            ventanaCursos.MdiParent = this;
            // 2. Le decimos que se muestre en pantalla
            ventanaCursos.Show();
        }

        private void especialidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Instanciamos el formulario correspondiente
            var ventanaEspecialidades = new EspecialidadLista();
            ventanaEspecialidades.MdiParent = this;
            // 2. Le decimos que se muestre en pantalla
            ventanaEspecialidades.Show();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void cursosPorMateriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Instanciamos el formulario correspondiente
            var ventanaMateriaCursos = new MateriaCursos();
            ventanaMateriaCursos.MdiParent = this;
            // 2. Le decimos que se muestre en pantalla
            ventanaMateriaCursos.Show();
        }
    }
}

