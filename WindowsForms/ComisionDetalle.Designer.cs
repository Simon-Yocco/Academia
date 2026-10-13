namespace WindowsForms
{
    partial class ComisionDetalle
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
            idTextBox = new TextBox();
            idLabel = new Label();
            cancelarButton = new Button();
            aceptarButton = new Button();
            descripcionTextBox = new TextBox();
            descripcionLabel = new Label();
            anioEspecialidadLabel = new Label();
            anioEspecialidadNumericUpDown = new NumericUpDown();
            planComboBox = new ComboBox();
            planLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)anioEspecialidadNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(169, 41);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(100, 23);
            idTextBox.TabIndex = 4;
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(140, 49);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(23, 15);
            idLabel.TabIndex = 3;
            idLabel.Text = "Id: ";
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(435, 274);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(75, 23);
            cancelarButton.TabIndex = 7;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(336, 274);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(75, 23);
            aceptarButton.TabIndex = 6;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(169, 82);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(305, 23);
            descripcionTextBox.TabIndex = 9;
            // 
            // descripcionLabel
            // 
            descripcionLabel.AutoSize = true;
            descripcionLabel.Location = new Point(91, 90);
            descripcionLabel.Name = "descripcionLabel";
            descripcionLabel.Size = new Size(72, 15);
            descripcionLabel.TabIndex = 8;
            descripcionLabel.Text = "Descripción:";
            // 
            // anioEspecialidadLabel
            // 
            anioEspecialidadLabel.AutoSize = true;
            anioEspecialidadLabel.Location = new Point(35, 128);
            anioEspecialidadLabel.Name = "anioEspecialidadLabel";
            anioEspecialidadLabel.Size = new Size(128, 15);
            anioEspecialidadLabel.TabIndex = 10;
            anioEspecialidadLabel.Text = "Año de la especialidad:";
            // 
            // anioEspecialidadNumericUpDown
            // 
            anioEspecialidadNumericUpDown.Location = new Point(169, 120);
            anioEspecialidadNumericUpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            anioEspecialidadNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            anioEspecialidadNumericUpDown.Name = "anioEspecialidadNumericUpDown";
            anioEspecialidadNumericUpDown.Size = new Size(120, 23);
            anioEspecialidadNumericUpDown.TabIndex = 11;
            anioEspecialidadNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // planComboBox
            // 
            planComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            planComboBox.FormattingEnabled = true;
            planComboBox.Location = new Point(169, 159);
            planComboBox.Name = "planComboBox";
            planComboBox.Size = new Size(121, 23);
            planComboBox.TabIndex = 12;
            // 
            // planLabel
            // 
            planLabel.AutoSize = true;
            planLabel.Location = new Point(130, 167);
            planLabel.Name = "planLabel";
            planLabel.Size = new Size(33, 15);
            planLabel.TabIndex = 13;
            planLabel.Text = "Plan:";
            // 
            // ComisionDetalle
            // 
            AcceptButton = aceptarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(538, 336);
            Controls.Add(planLabel);
            Controls.Add(planComboBox);
            Controls.Add(anioEspecialidadNumericUpDown);
            Controls.Add(anioEspecialidadLabel);
            Controls.Add(descripcionTextBox);
            Controls.Add(descripcionLabel);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            Name = "ComisionDetalle";
            Text = "Comision - Detalle";
            ((System.ComponentModel.ISupportInitialize)anioEspecialidadNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox idTextBox;
        private Label idLabel;
        private Button cancelarButton;
        private Button aceptarButton;
        private TextBox descripcionTextBox;
        private Label descripcionLabel;
        private Label anioEspecialidadLabel;
        private NumericUpDown anioEspecialidadNumericUpDown;
        private ComboBox planComboBox;
        private Label planLabel;
    }
}