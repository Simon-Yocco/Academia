namespace WindowsForms
{
    partial class MateriaCursos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            materiaComboBox = new ComboBox();
            cursosDataGridView = new DataGridView();
            agregarCursobutton = new Button();
            ((System.ComponentModel.ISupportInitialize)cursosDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(58, 49);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(267, 32);
            label1.TabIndex = 0;
            label1.Text = "Seleccione una materia:";
            // 
            // materiaComboBox
            // 
            materiaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            materiaComboBox.FormattingEnabled = true;
            materiaComboBox.Location = new Point(324, 49);
            materiaComboBox.Margin = new Padding(6, 6, 6, 6);
            materiaComboBox.Name = "materiaComboBox";
            materiaComboBox.Size = new Size(491, 40);
            materiaComboBox.TabIndex = 1;
            // 
            // cursosDataGridView
            // 
            cursosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            cursosDataGridView.Location = new Point(58, 128);
            cursosDataGridView.Margin = new Padding(6, 6, 6, 6);
            cursosDataGridView.Name = "cursosDataGridView";
            cursosDataGridView.ReadOnly = true;
            cursosDataGridView.RowHeadersWidth = 82;
            cursosDataGridView.Size = new Size(1356, 617);
            cursosDataGridView.TabIndex = 2;
            // 
            // agregarCursobutton
            // 
            agregarCursobutton.Location = new Point(1192, 779);
            agregarCursobutton.Margin = new Padding(6, 6, 6, 6);
            agregarCursobutton.Name = "agregarCursobutton";
            agregarCursobutton.Size = new Size(221, 49);
            agregarCursobutton.TabIndex = 3;
            agregarCursobutton.Text = "Agregar curso";
            agregarCursobutton.UseVisualStyleBackColor = true;
            agregarCursobutton.Click += agregarCursobutton_Click;
            // 
            // MateriaCursos
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1471, 853);
            Controls.Add(agregarCursobutton);
            Controls.Add(cursosDataGridView);
            Controls.Add(materiaComboBox);
            Controls.Add(label1);
            Margin = new Padding(6, 6, 6, 6);
            Name = "MateriaCursos";
            Text = "Cursos por Materia";
            ((System.ComponentModel.ISupportInitialize)cursosDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox materiaComboBox;
        private DataGridView cursosDataGridView;
        private Button agregarCursobutton;
    }
}