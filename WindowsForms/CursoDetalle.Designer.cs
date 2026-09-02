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
            SuspendLayout();
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(422, 228);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(75, 23);
            cancelarButton.TabIndex = 11;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(323, 228);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(75, 23);
            aceptarButton.TabIndex = 10;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(144, 153);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(305, 23);
            descripcionTextBox.TabIndex = 9;
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(144, 42);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(100, 23);
            idTextBox.TabIndex = 8;
            // 
            // descripcionLabel
            // 
            descripcionLabel.AutoSize = true;
            descripcionLabel.Location = new Point(66, 161);
            descripcionLabel.Name = "descripcionLabel";
            descripcionLabel.Size = new Size(72, 15);
            descripcionLabel.TabIndex = 7;
            descripcionLabel.Text = "Descripción:";
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(114, 50);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(24, 15);
            idLabel.TabIndex = 6;
            idLabel.Text = "ID: ";
            // 
            // anioCalendarioLabel
            // 
            anioCalendarioLabel.AutoSize = true;
            anioCalendarioLabel.Location = new Point(48, 90);
            anioCalendarioLabel.Name = "anioCalendarioLabel";
            anioCalendarioLabel.Size = new Size(90, 15);
            anioCalendarioLabel.TabIndex = 12;
            anioCalendarioLabel.Text = "Año calendario:";
            // 
            // anioCalendarioTextBox
            // 
            anioCalendarioTextBox.Location = new Point(144, 82);
            anioCalendarioTextBox.Name = "anioCalendarioTextBox";
            anioCalendarioTextBox.Size = new Size(100, 23);
            anioCalendarioTextBox.TabIndex = 13;
            // 
            // cupoLabel
            // 
            cupoLabel.AutoSize = true;
            cupoLabel.Location = new Point(96, 125);
            cupoLabel.Name = "cupoLabel";
            cupoLabel.Size = new Size(42, 15);
            cupoLabel.TabIndex = 14;
            cupoLabel.Text = "Cupo: ";
            // 
            // cupoTextBox
            // 
            cupoTextBox.Location = new Point(144, 117);
            cupoTextBox.Name = "cupoTextBox";
            cupoTextBox.Size = new Size(100, 23);
            cupoTextBox.TabIndex = 15;
            // 
            // CursoDetalle
            // 
            AcceptButton = aceptarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(538, 285);
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
    }
}