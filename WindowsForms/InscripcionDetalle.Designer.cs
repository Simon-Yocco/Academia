namespace WindowsForms
{
    partial class InscripcionDetalle
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
            idLabel = new Label();
            alumnoLabel = new Label();
            cursoLabel = new Label();
            idTextBox = new TextBox();
            condicionLabel = new Label();
            notaLabel = new Label();
            aceptarButton = new Button();
            cancelarButton = new Button();
            alumnoComboBox = new ComboBox();
            cursoComboBox = new ComboBox();
            condicionComboBox = new ComboBox();
            notaTextBox = new TextBox();
            SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(117, 55);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(20, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id:";
            // 
            // alumnoLabel
            // 
            alumnoLabel.AutoSize = true;
            alumnoLabel.Location = new Point(84, 94);
            alumnoLabel.Name = "alumnoLabel";
            alumnoLabel.Size = new Size(53, 15);
            alumnoLabel.TabIndex = 1;
            alumnoLabel.Text = "Alumno:";
            // 
            // cursoLabel
            // 
            cursoLabel.AutoSize = true;
            cursoLabel.Location = new Point(96, 132);
            cursoLabel.Name = "cursoLabel";
            cursoLabel.Size = new Size(41, 15);
            cursoLabel.TabIndex = 2;
            cursoLabel.Text = "Curso:";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(163, 47);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(100, 23);
            idTextBox.TabIndex = 3;
            // 
            // condicionLabel
            // 
            condicionLabel.AutoSize = true;
            condicionLabel.Location = new Point(72, 173);
            condicionLabel.Name = "condicionLabel";
            condicionLabel.Size = new Size(65, 15);
            condicionLabel.TabIndex = 4;
            condicionLabel.Text = "Condicion:";
            // 
            // notaLabel
            // 
            notaLabel.AutoSize = true;
            notaLabel.Location = new Point(101, 216);
            notaLabel.Name = "notaLabel";
            notaLabel.Size = new Size(36, 15);
            notaLabel.TabIndex = 5;
            notaLabel.Text = "Nota:";
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(312, 265);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(75, 23);
            aceptarButton.TabIndex = 6;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(419, 265);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(75, 23);
            cancelarButton.TabIndex = 7;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // alumnoComboBox
            // 
            alumnoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            alumnoComboBox.FormattingEnabled = true;
            alumnoComboBox.Location = new Point(163, 86);
            alumnoComboBox.Name = "alumnoComboBox";
            alumnoComboBox.Size = new Size(219, 23);
            alumnoComboBox.TabIndex = 8;
            // 
            // cursoComboBox
            // 
            cursoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cursoComboBox.FormattingEnabled = true;
            cursoComboBox.Location = new Point(163, 124);
            cursoComboBox.Name = "cursoComboBox";
            cursoComboBox.Size = new Size(219, 23);
            cursoComboBox.TabIndex = 9;
            // 
            // condicionComboBox
            // 
            condicionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            condicionComboBox.FormattingEnabled = true;
            condicionComboBox.Location = new Point(163, 165);
            condicionComboBox.Name = "condicionComboBox";
            condicionComboBox.Size = new Size(148, 23);
            condicionComboBox.TabIndex = 10;
            // 
            // notaTextBox
            // 
            notaTextBox.Location = new Point(163, 208);
            notaTextBox.Name = "notaTextBox";
            notaTextBox.Size = new Size(100, 23);
            notaTextBox.TabIndex = 12;
            // 
            // InscripcionDetalle
            // 
            AcceptButton = aceptarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(536, 355);
            Controls.Add(notaTextBox);
            Controls.Add(condicionComboBox);
            Controls.Add(cursoComboBox);
            Controls.Add(alumnoComboBox);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(notaLabel);
            Controls.Add(condicionLabel);
            Controls.Add(idTextBox);
            Controls.Add(cursoLabel);
            Controls.Add(alumnoLabel);
            Controls.Add(idLabel);
            Name = "InscripcionDetalle";
            Text = "Inscripcion - Detalle";
            Load += InscripcionDetalle_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private Label alumnoLabel;
        private Label cursoLabel;
        private TextBox idTextBox;
        private Label condicionLabel;
        private Label notaLabel;
        private Button aceptarButton;
        private Button cancelarButton;
        private ComboBox alumnoComboBox;
        private ComboBox cursoComboBox;
        private ComboBox condicionComboBox;
        private TextBox notaTextBox;
    }
}