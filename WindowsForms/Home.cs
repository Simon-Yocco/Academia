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
            // 1. Creamos una "instancia" (una copia en memoria) de la pantalla
            var ventanaCursos = new CursoLista();

            // 2. Le decimos que se muestre en pantalla
            ventanaCursos.ShowDialog();
        }

        private void especialidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Creamos una "instancia" (una copia en memoria) de la pantalla
            var ventanaEspecialidades = new EspecialidadLista();

            // 2. Le decimos que se muestre en pantalla
            ventanaEspecialidades.ShowDialog();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
}
