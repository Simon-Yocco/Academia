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
            label1.Location = new Point(31, 23);
            label1.Name = "label1";
            label1.Size = new Size(132, 15);
            label1.TabIndex = 0;
            label1.Text = "Seleccione una materia:";
            // 
            // materiaComboBox
            // 
            materiaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            materiaComboBox.FormattingEnabled = true;
            materiaComboBox.Location = new Point(169, 15);
            materiaComboBox.Name = "materiaComboBox";
            materiaComboBox.Size = new Size(266, 23);
            materiaComboBox.TabIndex = 1;
            // 
            // cursosDataGridView
            // 
            cursosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            cursosDataGridView.Location = new Point(31, 60);
            cursosDataGridView.Name = "cursosDataGridView";
            cursosDataGridView.ReadOnly = true;
            cursosDataGridView.Size = new Size(730, 289);
            cursosDataGridView.TabIndex = 2;
            // 
            // agregarCursobutton
            // 
            agregarCursobutton.Location = new Point(642, 365);
            agregarCursobutton.Name = "agregarCursobutton";
            agregarCursobutton.Size = new Size(119, 23);
            agregarCursobutton.TabIndex = 3;
            agregarCursobutton.Text = "Agregar curso";
            agregarCursobutton.UseVisualStyleBackColor = true;
            agregarCursobutton.Click += agregarCursobutton_Click;
            // 
            // MateriaCursos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(792, 400);
            Controls.Add(agregarCursobutton);
            Controls.Add(cursosDataGridView);
            Controls.Add(materiaComboBox);
            Controls.Add(label1);
            Name = "MateriaCursos";
            Text = "Form1";
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