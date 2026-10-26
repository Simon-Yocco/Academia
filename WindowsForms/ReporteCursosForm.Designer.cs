namespace WindowsForms
{
    partial class ReporteCursosForm
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
            lblMateria = new Label();
            cmbMaterias = new ComboBox();
            btnRecargar = new Button();
            dgvCursos = new DataGridView();
            chartBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvCursos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartBox).BeginInit();
            SuspendLayout();
            // 
            // lblMateria
            // 
            lblMateria.AutoSize = true;
            lblMateria.Location = new Point(20, 22);
            lblMateria.Name = "lblMateria";
            lblMateria.Size = new Size(63, 20);
            lblMateria.TabIndex = 0;
            lblMateria.Text = "Materia:";
            // 
            // cmbMaterias
            // 
            cmbMaterias.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaterias.FormattingEnabled = true;
            cmbMaterias.Location = new Point(90, 18);
            cmbMaterias.Name = "cmbMaterias";
            cmbMaterias.Size = new Size(290, 28);
            cmbMaterias.TabIndex = 1;
            cmbMaterias.SelectedIndexChanged += cmbMaterias_SelectedIndexChanged;
            // 
            // btnRecargar
            // 
            btnRecargar.Location = new Point(395, 17);
            btnRecargar.Name = "btnRecargar";
            btnRecargar.Size = new Size(95, 30);
            btnRecargar.TabIndex = 2;
            btnRecargar.Text = "Recargar";
            btnRecargar.UseVisualStyleBackColor = true;
            btnRecargar.Click += btnRecargar_Click;
            // 
            // dgvCursos
            // 
            dgvCursos.AllowUserToAddRows = false;
            dgvCursos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvCursos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCursos.Location = new Point(20, 60);
            dgvCursos.Name = "dgvCursos";
            dgvCursos.ReadOnly = true;
            dgvCursos.RowHeadersWidth = 51;
            dgvCursos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCursos.Size = new Size(960, 210);
            dgvCursos.TabIndex = 3;
            // 
            // chartBox
            // 
            chartBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartBox.BackColor = Color.White;
            chartBox.BorderStyle = BorderStyle.FixedSingle;
            chartBox.Location = new Point(20, 285);
            chartBox.Name = "chartBox";
            chartBox.Size = new Size(960, 345);
            chartBox.TabIndex = 4;
            chartBox.TabStop = false;
            chartBox.Paint += chartBox_Paint;
            // 
            // ReporteCursosForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(chartBox);
            Controls.Add(dgvCursos);
            Controls.Add(btnRecargar);
            Controls.Add(cmbMaterias);
            Controls.Add(lblMateria);
            Name = "ReporteCursosForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reporte de Cursos y Ocupación";
            Load += ReporteCursosForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCursos).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMateria;
        private ComboBox cmbMaterias;
        private Button btnRecargar;
        private DataGridView dgvCursos;
        private PictureBox chartBox;
    }
}
