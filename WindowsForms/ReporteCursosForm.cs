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
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Panel Superior
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 55;
            topPanel.BackColor = Color.FromArgb(245, 247, 250);
            topPanel.Padding = new Padding(15, 10, 15, 10);

            titleLabel.Text = "Reporte de Ocupación y Cupos por Curso";
            titleLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(15, 12);
            titleLabel.AutoSize = true;

            btnRecargar.Text = "Recargar";
            btnRecargar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRecargar.Location = new Point(980, 12);
            btnRecargar.Size = new Size(90, 32);
            btnRecargar.Click += async (s, e) => await CargarDatos();

            topPanel.Controls.Add(titleLabel);
            topPanel.Controls.Add(btnRecargar);

            // Split Container: Arriba Grilla, Abajo Gráfico
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Horizontal;
            splitContainer.SplitterDistance = 210;

            // Grilla
            grid.Dock = DockStyle.Fill;
            grid.AutoGenerateColumns = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;

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
                grid.DataSource = null;
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

        // DIBUJO DEL GRÁFICO COMPARATIVO
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

            int padLeft = 60, padRight = 30, padTop = 55, padBottom = 75;
            int chartW = w - padLeft - padRight;
            int chartH = h - padTop - padBottom;
            if (chartW <= 0 || chartH <= 0) return;

            // Leyenda dinámica adaptable a cualquier escalado DPI
            using var fontLegend = new Font("Segoe UI", 9, FontStyle.Bold);
            using var brushCupo = new SolidBrush(Color.FromArgb(54, 162, 235));     // Azul
            using var brushInscriptos = new SolidBrush(Color.FromArgb(75, 192, 192)); // Verde / Turquesa

            float legendX = padLeft;
            float legendY = 12;

            g.FillRectangle(brushCupo, legendX, legendY, 14, 14);
            g.DrawString("Cupo Máximo", fontLegend, Brushes.Black, legendX + 20, legendY);

            var sizeCupo = g.MeasureString("Cupo Máximo", fontLegend);
            float inscriptosX = legendX + sizeCupo.Width + 45;

            g.FillRectangle(brushInscriptos, inscriptosX, legendY, 14, 14);
            g.DrawString("Alumnos Inscriptos", fontLegend, Brushes.Black, inscriptosX + 20, legendY);

            // Escala máxima
            int maxVal = Math.Max(datos.Max(d => Math.Max(d.CupoTotal, d.Inscriptos)), 10);
            maxVal = (int)(Math.Ceiling(maxVal / 10.0) * 10);

            // Ejes y líneas guía
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

            // Barras por Curso
            int n = datos.Count;
            float groupWidth = (float)chartW / n;
            float barWidth = Math.Min(groupWidth * 0.35f, 32f);

            using var sfCenter = new StringFormat { Alignment = StringAlignment.Center };

            for (int i = 0; i < n; i++)
            {
                var c = datos[i];
                float groupX = padLeft + i * groupWidth + (groupWidth - (barWidth * 2 + 6)) / 2;

                // Barra Cupo
                float hCupo = ((float)c.CupoTotal / maxVal) * chartH;
                float yCupo = padTop + chartH - hCupo;
                g.FillRectangle(brushCupo, groupX, yCupo, barWidth, hCupo);

                // Barra Inscriptos
                float hIns = ((float)c.Inscriptos / maxVal) * chartH;
                float yIns = padTop + chartH - hIns;
                g.FillRectangle(brushInscriptos, groupX + barWidth + 6, yIns, barWidth, hIns);

                // Valores numéricos centrados sobre cada barra
                g.DrawString(c.CupoTotal.ToString(), fontAxis, Brushes.DarkBlue, groupX + barWidth / 2, yCupo - 15, sfCenter);
                g.DrawString(c.Inscriptos.ToString(), fontAxis, Brushes.DarkGreen, groupX + barWidth + 6 + barWidth / 2, yIns - 15, sfCenter);

                // Etiqueta del curso debajo del eje X
                string label = $"{c.Materia}\n({c.Comision})";
                var rectLabel = new RectangleF(padLeft + i * groupWidth, padTop + chartH + 8, groupWidth, padBottom - 12);
                var sfLabel = new StringFormat { Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
                g.DrawString(label, fontAxis, Brushes.Black, rectLabel, sfLabel);
            }
        }
    }
}
