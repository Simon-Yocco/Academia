using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public class ReporteRendimientoForm : Form
    {
        private TextBox txtBuscar = new();
        private Button btnBuscar = new();
        private Button btnRecargar = new();
        private DataGridView dgvReporte = new();
        private Label lblBuscar = new();

        private List<CursoRendimientoDTO> listaCursos = new();

        public ReporteRendimientoForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Reporte de Rendimiento por Curso";
            this.Size = new System.Drawing.Size(1000, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Etiqueta
            lblBuscar.Text = "Materia:";
            lblBuscar.Location = new System.Drawing.Point(20, 23);
            lblBuscar.AutoSize = true;

            // Caja de texto de búsqueda
            txtBuscar.Location = new System.Drawing.Point(80, 20);
            txtBuscar.Size = new System.Drawing.Size(250, 27);

            // Botón Buscar
            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new System.Drawing.Point(340, 19);
            btnBuscar.Size = new System.Drawing.Size(85, 30);
            btnBuscar.Click += (s, e) => FiltrarCursos();

            // Botón Todos / Recargar
            btnRecargar.Text = "Ver Todos";
            btnRecargar.Location = new System.Drawing.Point(435, 19);
            btnRecargar.Size = new System.Drawing.Size(95, 30);
            btnRecargar.Click += async (s, e) =>
            {
                txtBuscar.Clear();
                await CargarDatos();
            };

            // Grilla de datos
            dgvReporte.Location = new System.Drawing.Point(20, 65);
            dgvReporte.Size = new System.Drawing.Size(945, 370);
            dgvReporte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReporte.ReadOnly = true;
            dgvReporte.AllowUserToAddRows = false;
            dgvReporte.AutoGenerateColumns = false;
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            ConfigurarColumnas();

            // Controles al formulario
            this.Controls.Add(lblBuscar);
            this.Controls.Add(txtBuscar);
            this.Controls.Add(btnBuscar);
            this.Controls.Add(btnRecargar);
            this.Controls.Add(dgvReporte);

            this.Load += async (s, e) => await CargarDatos();
        }

        private void ConfigurarColumnas()
        {
            dgvReporte.Columns.Clear();
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Materia", DataPropertyName = "Materia", FillWeight = 25 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Comisión", DataPropertyName = "Comision", FillWeight = 15 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Año", DataPropertyName = "AnioCalendario", FillWeight = 10 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Inscriptos", DataPropertyName = "TotalInscriptos", FillWeight = 12 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "% Aprobados", DataPropertyName = "PorcentajeAprobados", FillWeight = 13 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "% Regulares", DataPropertyName = "PorcentajeRegulares", FillWeight = 13 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "% Libres", DataPropertyName = "PorcentajeLibres", FillWeight = 12 });
        }

        private async Task CargarDatos()
        {
            try
            {
                listaCursos = await ReporteApiClient.GetCursosRendimientoAsync();
                FiltrarCursos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarCursos()
        {
            string texto = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(texto))
            {
                dgvReporte.DataSource = listaCursos;
            }
            else
            {
                dgvReporte.DataSource = listaCursos
                    .Where(c => c.Materia.Contains(texto, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }
    }
}
