using API.Clients;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            ConfigurarMenuSegunRol();
            this.Text = $"Gestión Académica - Usuario: {BaseApiClient.UsuarioActual} ({BaseApiClient.Rol})";
        }

        private void ConfigurarMenuSegunRol()
        {
            if (!BaseApiClient.EsAdmin)
            {
                cursosToolStripMenuItem.Visible = false;
                planesToolStripMenuItem.Visible = false;
                especialidadesToolStripMenuItem.Visible = false;
                comisionesToolStripMenuItem.Visible = false;
                usuariosToolStripMenuItem.Visible = false;

                // Los reportes son visibles para Administradores y Docentes, pero no para Alumnos
                if (BaseApiClient.EsAlumno)
                {
                    reporteCursosToolStripMenuItem.Visible = false;
                    reporteRendimientoToolStripMenuItem.Visible = false;
                }
            }
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
            BaseApiClient.Token = string.Empty;
            BaseApiClient.Rol = string.Empty;
            BaseApiClient.UsuarioActual = string.Empty;
            BaseApiClient.IDPersonaActual = 0;
            Application.Restart();
        }

        private void planesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<PlanLista>();
        }

        private void comisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<ComisionLista>();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<UsuarioLista>();
        }

        private void inscripcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<InscripcionLista>();
        }

        private void reporteCursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<ReporteCursosForm>();
        }

        private void reporteRendimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<ReporteRendimientoForm>();
        }
    }
}
