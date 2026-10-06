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

        private void AbrirFormulario<T>() where T : Form, new()
        {
            var formulario = this.MdiChildren.OfType<T>().FirstOrDefault();

            if (formulario != null)
            {
                formulario.BringToFront();
                formulario.Activate();
            }
            else
            {
                formulario = new T();
                formulario.MdiParent = this;
                formulario.Show();
            }
        }

        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<CursoLista>();
        }

        private void especialidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<EspecialidadLista>();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void cursosPorMateriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<MateriaCursos>();
        }
    }
}

