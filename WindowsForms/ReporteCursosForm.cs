using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class ReporteCursosForm : Form
    {
        private List<CursoOcupacionDTO> todosLosCursos = new();
        private List<CursoOcupacionDTO> cursosFiltrados = new();

        public ReporteCursosForm()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private async void ReporteCursosForm_Load(object? sender, EventArgs e)
        {
            await CargarDatos();
        }

        private void ConfigurarColumnas()
        {
            dgvCursos.Columns.Clear();
            dgvCursos.AutoGenerateColumns = false;
            dgvCursos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvCursos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Materia", DataPropertyName = "Materia", FillWeight = 25 });
            dgvCursos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Comisión", DataPropertyName = "Comision", FillWeight = 15 });
            dgvCursos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Año", DataPropertyName = "AnioCalendario", FillWeight = 10 });
            dgvCursos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Docente", DataPropertyName = "Docente", FillWeight = 20 });
            dgvCursos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cupo Máx.", DataPropertyName = "CupoTotal", FillWeight = 10 });
            dgvCursos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Inscriptos", DataPropertyName = "Inscriptos", FillWeight = 10 });
            dgvCursos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "% Ocupación", DataPropertyName = "PorcentajeOcupacion", FillWeight = 10 });
        }

        private async Task CargarDatos()
        {
            try
            {
                btnRecargar.Enabled = false;
                todosLosCursos = await ReporteApiClient.GetCursosOcupacionAsync();

                string materiaPrevia = cmbMaterias.SelectedItem?.ToString() ?? "";
                cmbMaterias.Items.Clear();

                var materias = todosLosCursos
                    .Select(c => c.Materia)
                    .Where(m => !string.IsNullOrWhiteSpace(m))
                    .Distinct()
                    .OrderBy(m => m)
                    .ToArray();

                cmbMaterias.Items.AddRange(materias);

                // Si ya había seleccionado una materia previamente, mantenerla; sino arrancar sin nada seleccionado
                if (!string.IsNullOrEmpty(materiaPrevia) && cmbMaterias.Items.Contains(materiaPrevia))
                {
                    cmbMaterias.SelectedItem = materiaPrevia;
                }
                else
                {
                    cmbMaterias.SelectedIndex = -1;
                }

                FiltrarPorMateria();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRecargar.Enabled = true;
            }
        }

        private void FiltrarPorMateria()
        {
            if (cmbMaterias.SelectedIndex < 0 || cmbMaterias.SelectedItem == null)
            {
                cursosFiltrados = new List<CursoOcupacionDTO>();
            }
            else
            {
                string materiaSeleccionada = cmbMaterias.SelectedItem.ToString() ?? "";
                cursosFiltrados = todosLosCursos
                    .Where(c => c.Materia.Equals(materiaSeleccionada, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            dgvCursos.DataSource = null;
            dgvCursos.DataSource = cursosFiltrados;
            chartBox.Invalidate();
        }

        private void cmbMaterias_SelectedIndexChanged(object? sender, EventArgs e)
        {
            FiltrarPorMateria();
        }

        private async void btnRecargar_Click(object? sender, EventArgs e)
        {
            await CargarDatos();
        }

        private void chartBox_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (cursosFiltrados == null || !cursosFiltrados.Any())
            {
                using var font = new Font("Segoe UI", 11);
                using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                string mensaje = cmbMaterias.SelectedIndex < 0
                    ? "Seleccione una materia para ver el reporte de sus comisiones."
                    : "No se encontraron comisiones para la materia seleccionada.";
                g.DrawString(mensaje, font, Brushes.Gray, chartBox.ClientRectangle, sf);
                return;
            }

            int w = chartBox.ClientSize.Width;
            int h = chartBox.ClientSize.Height;

            int padLeft = 60, padRight = 30, padTop = 50, padBottom = 65;
            int chartW = w - padLeft - padRight;
            int chartH = h - padTop - padBottom;
            if (chartW <= 0 || chartH <= 0) return;

            // Leyenda de colores
            using var fontLegend = new Font("Segoe UI", 9, FontStyle.Bold);
            using var brushCupo = new SolidBrush(Color.FromArgb(54, 162, 235));     // Azul
            using var brushInscriptos = new SolidBrush(Color.FromArgb(75, 192, 192)); // Verde / Turquesa

            float legendX = padLeft;
            float legendY = 12;

            g.FillRectangle(brushCupo, legendX, legendY, 14, 14);
            g.DrawString("Cupo Máximo", fontLegend, Brushes.Black, legendX + 20, legendY);

            var sizeCupo = g.MeasureString("Cupo Máximo", fontLegend);
            float inscriptosX = legendX + sizeCupo.Width + 40;

            g.FillRectangle(brushInscriptos, inscriptosX, legendY, 14, 14);
            g.DrawString("Alumnos Inscriptos", fontLegend, Brushes.Black, inscriptosX + 20, legendY);

            // Escala máxima del eje Y
            int maxVal = Math.Max(cursosFiltrados.Max(d => Math.Max(d.CupoTotal, d.Inscriptos)), 10);
            maxVal = (int)(Math.Ceiling(maxVal / 10.0) * 10);

            // Ejes y líneas guía horizontales
            using var penEje = new Pen(Color.FromArgb(220, 224, 230), 1);
            using var fontAxis = new Font("Segoe UI", 8);
            using var sfAxisY = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

            for (int i = 0; i <= 5; i++)
            {
                int val = (maxVal * i) / 5;
                int y = padTop + chartH - (int)((float)val / maxVal * chartH);
                g.DrawLine(penEje, padLeft, y, padLeft + chartW, y);
                g.DrawString(val.ToString(), fontAxis, Brushes.Gray, padLeft - 8, y, sfAxisY);
            }

            // Barras por Comisión
            int n = cursosFiltrados.Count;
            float groupWidth = (float)chartW / n;
            float barWidth = Math.Min(groupWidth * 0.35f, 40f);

            using var sfCenter = new StringFormat { Alignment = StringAlignment.Center };

            for (int i = 0; i < n; i++)
            {
                var c = cursosFiltrados[i];
                float groupX = padLeft + i * groupWidth + (groupWidth - (barWidth * 2 + 6)) / 2;

                // Barra Cupo
                float hCupo = ((float)c.CupoTotal / maxVal) * chartH;
                float yCupo = padTop + chartH - hCupo;
                g.FillRectangle(brushCupo, groupX, yCupo, barWidth, hCupo);

                // Barra Inscriptos
                float hIns = ((float)c.Inscriptos / maxVal) * chartH;
                float yIns = padTop + chartH - hIns;
                g.FillRectangle(brushInscriptos, groupX + barWidth + 6, yIns, barWidth, hIns);

                // Valores numéricos sobre cada barra
                g.DrawString(c.CupoTotal.ToString(), fontAxis, Brushes.DarkBlue, groupX + barWidth / 2, yCupo - 15, sfCenter);
                g.DrawString(c.Inscriptos.ToString(), fontAxis, Brushes.DarkGreen, groupX + barWidth + 6 + barWidth / 2, yIns - 15, sfCenter);

                // Etiqueta debajo del eje X mostrando la Comisión y el Año
                string label = $"Comisión {c.Comision}\n({c.AnioCalendario})";
                var rectLabel = new RectangleF(padLeft + i * groupWidth, padTop + chartH + 8, groupWidth, padBottom - 12);
                var sfLabel = new StringFormat { Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
                g.DrawString(label, fontAxis, Brushes.Black, rectLabel, sfLabel);
            }
        }
    }
}
