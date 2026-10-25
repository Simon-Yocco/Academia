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
    public class ReporteCursosForm : Form
    {
        private DataGridView grid = new();
        private PictureBox chartBox = new();
        private Panel topPanel = new();
        private Label titleLabel = new();
        private Button btnRecargar = new();
        private SplitContainer splitContainer = new();
        private List<CursoOcupacionDTO> datos = new();
        public ReporteCursosForm()
        {
            InitializeComponents();
            this.Load += async (s, e) => await CargarDatos();
        }
        private void InitializeComponents()
        {
            this.Text = "Reporte de Cursos y Ocupación";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            // Panel Superior
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 60;
            topPanel.BackColor = Color.FromArgb(245, 247, 250);
            topPanel.Padding = new Padding(15, 12, 15, 12);
            titleLabel.Text = "Reporte de Ocupación y Cupos por Curso";
            titleLabel.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.AutoSize = true;
            btnRecargar.Text = "Recargar";
            btnRecargar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRecargar.Location = new Point(880, 15);
            btnRecargar.Size = new Size(85, 30);
            btnRecargar.Click += async (s, e) => await CargarDatos();
            topPanel.Controls.Add(titleLabel);
            topPanel.Controls.Add(btnRecargar);
            // Split Container: Arriba Grilla, Abajo Gráfico
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Horizontal;
            splitContainer.SplitterDistance = 280;
            // Grilla
            grid.Dock = DockStyle.Fill;
            grid.AutoGenerateColumns = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ConfigurarColumnas();
            // PictureBox para gráfico (Doble buffer nativo para no parpadear)
            chartBox.Dock = DockStyle.Fill;
            chartBox.BackColor = Color.White;
            chartBox.Paint += ChartBox_Paint;
            splitContainer.Panel1.Controls.Add(grid);
            splitContainer.Panel2.Controls.Add(chartBox);
            this.Controls.Add(splitContainer);
            this.Controls.Add(topPanel);
        }
        private void ConfigurarColumnas()
        {
            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Materia", DataPropertyName = "Materia", FillWeight = 25 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Comisión", DataPropertyName = "Comision", FillWeight = 15 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Año", DataPropertyName = "AnioCalendario", FillWeight = 10 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Docente", DataPropertyName = "Docente", FillWeight = 20 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cupo Máx.", DataPropertyName = "CupoTotal", FillWeight = 10 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Inscriptos", DataPropertyName = "Inscriptos", FillWeight = 10 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "% Ocupación", DataPropertyName = "PorcentajeOcupacion", FillWeight = 10 });
        }
        private async Task CargarDatos()
        {
            try
            {
                btnRecargar.Enabled = false;
                datos = await ReporteApiClient.GetCursosOcupacionAsync();
                grid.DataSource = datos;
                chartBox.Invalidate(); // Redibujar gráfico
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
        // DIBUJO DEL GRÁFICO COMPARATIVO (Barras Lado a Lado)
        private void ChartBox_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (datos == null || !datos.Any())
            {
                using var font = new Font("Segoe UI", 11);
                g.DrawString("No hay cursos para graficar.", font, Brushes.Gray, 30, 30);
                return;
            }
            int w = chartBox.ClientSize.Width;
            int h = chartBox.ClientSize.Height;
            int padLeft = 60, padRight = 30, padTop = 40, padBottom = 80;
            int chartW = w - padLeft - padRight;
            int chartH = h - padTop - padBottom;
            if (chartW <= 0 || chartH <= 0) return;
            // Leyenda
            using var fontLegend = new Font("Segoe UI", 9, FontStyle.Bold);
            using var brushCupo = new SolidBrush(Color.FromArgb(54, 162, 235));     // Azul
            using var brushInscriptos = new SolidBrush(Color.FromArgb(75, 192, 192)); // Verde / Turquesa
            g.FillRectangle(brushCupo, padLeft, 10, 14, 14);
            g.DrawString("Cupo Máximo", fontLegend, Brushes.Black, padLeft + 20, 9);
            g.FillRectangle(brushInscriptos, padLeft + 140, 10, 14, 14);
            g.DrawString("Alumnos Inscriptos", fontLegend, Brushes.Black, padLeft + 160, 9);
            // Escala máxima
            int maxVal = Math.Max(datos.Max(d => Math.Max(d.CupoTotal, d.Inscriptos)), 10);
            maxVal = (int)(Math.Ceiling(maxVal / 10.0) * 10);
            // Ejes y líneas guía
            using var penEje = new Pen(Color.FromArgb(200, 200, 200), 1);
            using var fontAxis = new Font("Segoe UI", 8);
            for (int i = 0; i <= 5; i++)
            {
                int val = (maxVal * i) / 5;
                int y = padTop + chartH - (int)((float)val / maxVal * chartH);
                g.DrawLine(penEje, padLeft, y, padLeft + chartW, y);
                g.DrawString(val.ToString(), fontAxis, Brushes.Gray, 15, y - 6);
            }
            // Barras por Curso
            int n = datos.Count;
            float groupWidth = (float)chartW / n;
            float barWidth = Math.Min(groupWidth * 0.35f, 35f);
            for (int i = 0; i < n; i++)
            {
                var c = datos[i];
                float groupX = padLeft + i * groupWidth + (groupWidth - (barWidth * 2 + 4)) / 2;
                // Barra Cupo
                float hCupo = ((float)c.CupoTotal / maxVal) * chartH;
                float yCupo = padTop + chartH - hCupo;
                g.FillRectangle(brushCupo, groupX, yCupo, barWidth, hCupo);
                // Barra Inscriptos
                float hIns = ((float)c.Inscriptos / maxVal) * chartH;
                float yIns = padTop + chartH - hIns;
                g.FillRectangle(brushInscriptos, groupX + barWidth + 4, yIns, barWidth, hIns);
                // Valores numéricos sobre las barras
                g.DrawString(c.CupoTotal.ToString(), fontAxis, Brushes.DarkBlue, groupX, yCupo - 14);
                g.DrawString(c.Inscriptos.ToString(), fontAxis, Brushes.DarkGreen, groupX + barWidth + 4, yIns - 14);
                // Etiqueta curso (rotada o corta)
                string label = $"{c.Materia}\n({c.Comision})";
                var rectLabel = new RectangleF(padLeft + i * groupWidth, padTop + chartH + 5, groupWidth, padBottom - 10);
                var sf = new StringFormat { Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
                g.DrawString(label, fontAxis, Brushes.Black, rectLabel, sf);
            }
        }
    }
}