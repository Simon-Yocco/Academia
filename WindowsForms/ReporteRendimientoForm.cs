using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class ReporteRendimientoForm : Form
    {
        private List<CursoRendimientoDTO> listaCursos = new();

        public ReporteRendimientoForm()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private async void ReporteRendimientoForm_Load(object? sender, EventArgs e)
        {
            await CargarDatos();
        }

        private void ConfigurarColumnas()
        {
            dgvReporte.Columns.Clear();
            dgvReporte.AutoGenerateColumns = false;
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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

            dgvReporte.DataSource = null;
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

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            FiltrarCursos();
        }

        private async void btnRecargar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            await CargarDatos();
        }
    }
}
