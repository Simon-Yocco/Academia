namespace WindowsForms
{
    partial class ReporteRendimientoForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            btnRecargar = new Button();
            dgvReporte = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).BeginInit();
            SuspendLayout();
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 23);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(63, 20);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Materia:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(89, 20);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(250, 27);
            txtBuscar.TabIndex = 1;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(345, 18);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(85, 30);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnRecargar
            // 
            btnRecargar.Location = new Point(436, 18);
            btnRecargar.Name = "btnRecargar";
            btnRecargar.Size = new Size(95, 30);
            btnRecargar.TabIndex = 3;
            btnRecargar.Text = "Ver Todos";
            btnRecargar.UseVisualStyleBackColor = true;
            btnRecargar.Click += btnRecargar_Click;
            // 
            // dgvReporte
            // 
            dgvReporte.AllowUserToAddRows = false;
            dgvReporte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporte.Location = new Point(20, 65);
            dgvReporte.Name = "dgvReporte";
            dgvReporte.ReadOnly = true;
            dgvReporte.RowHeadersWidth = 51;
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.Size = new Size(945, 370);
            dgvReporte.TabIndex = 4;
            // 
            // ReporteRendimientoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 453);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnRecargar);
            Controls.Add(dgvReporte);
            Name = "ReporteRendimientoForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reporte de Rendimiento por Curso";
            Load += ReporteRendimientoForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReporte).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnRecargar;
        private DataGridView dgvReporte;
    }
}
