namespace WindowsForms
{
    partial class CursoDetalle
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
            cancelarButton = new Button();
            aceptarButton = new Button();
            descripcionTextBox = new TextBox();
            idTextBox = new TextBox();
            descripcionLabel = new Label();
            idLabel = new Label();
            anioCalendarioLabel = new Label();
            anioCalendarioTextBox = new TextBox();
            cupoLabel = new Label();
            cupoTextBox = new TextBox();
            materiaLabel = new Label();
            comisionLabel = new Label();
            materiaComboBox = new ComboBox();
            comisionComboBox = new ComboBox();
            SuspendLayout();
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(446, 301);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(75, 23);
            cancelarButton.TabIndex = 11;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(347, 301);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(75, 23);
            aceptarButton.TabIndex = 10;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(163, 217);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(305, 23);
            descripcionTextBox.TabIndex = 9;
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(163, 21);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(100, 23);
            idTextBox.TabIndex = 8;
            // 
            // descripcionLabel
            // 
            descripcionLabel.AutoSize = true;
            descripcionLabel.Location = new Point(85, 225);
            descripcionLabel.Name = "descripcionLabel";
            descripcionLabel.Size = new Size(72, 15);
            descripcionLabel.TabIndex = 7;
            descripcionLabel.Text = "Descripción:";
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(133, 29);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(24, 15);
            idLabel.TabIndex = 6;
            idLabel.Text = "ID: ";
            // 
            // anioCalendarioLabel
            // 
            anioCalendarioLabel.AutoSize = true;
            anioCalendarioLabel.Location = new Point(67, 70);
            anioCalendarioLabel.Name = "anioCalendarioLabel";
            anioCalendarioLabel.Size = new Size(90, 15);
            anioCalendarioLabel.TabIndex = 12;
            anioCalendarioLabel.Text = "Año calendario:";
            // 
            // anioCalendarioTextBox
            // 
            anioCalendarioTextBox.Location = new Point(163, 62);
            anioCalendarioTextBox.Name = "anioCalendarioTextBox";
            anioCalendarioTextBox.Size = new Size(100, 23);
            anioCalendarioTextBox.TabIndex = 13;
            // 
            // cupoLabel
            // 
            cupoLabel.AutoSize = true;
            cupoLabel.Location = new Point(115, 189);
            cupoLabel.Name = "cupoLabel";
            cupoLabel.Size = new Size(42, 15);
            cupoLabel.TabIndex = 14;
            cupoLabel.Text = "Cupo: ";
            // 
            // cupoTextBox
            // 
            cupoTextBox.Location = new Point(163, 181);
            cupoTextBox.Name = "cupoTextBox";
            cupoTextBox.Size = new Size(100, 23);
            cupoTextBox.TabIndex = 15;
            // 
            // materiaLabel
            // 
            materiaLabel.AutoSize = true;
            materiaLabel.Location = new Point(107, 109);
            materiaLabel.Name = "materiaLabel";
            materiaLabel.Size = new Size(50, 15);
            materiaLabel.TabIndex = 16;
            materiaLabel.Text = "Materia:";
            // 
            // comisionLabel
            // 
            comisionLabel.AutoSize = true;
            comisionLabel.Location = new Point(96, 151);
            comisionLabel.Name = "comisionLabel";
            comisionLabel.Size = new Size(61, 15);
            comisionLabel.TabIndex = 17;
            comisionLabel.Text = "Comision:";
            // 
            // materiaComboBox
            // 
            materiaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            materiaComboBox.FormattingEnabled = true;
            materiaComboBox.Location = new Point(163, 101);
            materiaComboBox.Name = "materiaComboBox";
            materiaComboBox.Size = new Size(305, 23);
            materiaComboBox.TabIndex = 18;
            // 
            // comisionComboBox
            // 
            comisionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comisionComboBox.FormattingEnabled = true;
            comisionComboBox.Location = new Point(163, 143);
            comisionComboBox.Name = "comisionComboBox";
            comisionComboBox.Size = new Size(100, 23);
            comisionComboBox.TabIndex = 19;
            // 
            // CursoDetalle
            // 
            AcceptButton = aceptarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(553, 347);
            Controls.Add(comisionComboBox);
            Controls.Add(materiaComboBox);
            Controls.Add(comisionLabel);
            Controls.Add(materiaLabel);
            Controls.Add(cupoTextBox);
            Controls.Add(cupoLabel);
            Controls.Add(anioCalendarioTextBox);
            Controls.Add(anioCalendarioLabel);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(descripcionTextBox);
            Controls.Add(idTextBox);
            Controls.Add(descripcionLabel);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "CursoDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Curso";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cancelarButton;
        private Button aceptarButton;
        private TextBox descripcionTextBox;
        private TextBox idTextBox;
        private Label descripcionLabel;
        private Label idLabel;
        private Label anioCalendarioLabel;
        private TextBox anioCalendarioTextBox;
        private Label cupoLabel;
        private TextBox cupoTextBox;
        private Label materiaLabel;
        private Label comisionLabel;
        private ComboBox materiaComboBox;
        private ComboBox comisionComboBox;
    }
}