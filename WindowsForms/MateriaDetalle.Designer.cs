namespace WindowsForms
{
    partial class MateriaDetalle
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
            idTextBox = new TextBox();
            descripcionLabel = new Label();
            hsSemanalesLabel = new Label();
            hsTotalesLabel = new Label();
            planLabel = new Label();
            descripcionTextBox = new TextBox();
            hsSemanalesNumericUpDown = new NumericUpDown();
            hsTotalesNumericUpDown = new NumericUpDown();
            planComboBox = new ComboBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)hsSemanalesNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hsTotalesNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(43, 20);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(21, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id:";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(140, 17);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(100, 23);
            idTextBox.TabIndex = 1;
            // 
            // descripcionLabel
            // 
            descripcionLabel.AutoSize = true;
            descripcionLabel.Location = new Point(43, 55);
            descripcionLabel.Name = "descripcionLabel";
            descripcionLabel.Size = new Size(72, 15);
            descripcionLabel.TabIndex = 2;
            descripcionLabel.Text = "Descripción:";
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(140, 52);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(280, 23);
            descripcionTextBox.TabIndex = 3;
            // 
            // hsSemanalesLabel
            // 
            hsSemanalesLabel.AutoSize = true;
            hsSemanalesLabel.Location = new Point(43, 90);
            hsSemanalesLabel.Name = "hsSemanalesLabel";
            hsSemanalesLabel.Size = new Size(100, 15);
            hsSemanalesLabel.TabIndex = 4;
            hsSemanalesLabel.Text = "Horas Semanales:";
            // 
            // hsSemanalesNumericUpDown
            // 
            hsSemanalesNumericUpDown.Location = new Point(140, 88);
            hsSemanalesNumericUpDown.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            hsSemanalesNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            hsSemanalesNumericUpDown.Name = "hsSemanalesNumericUpDown";
            hsSemanalesNumericUpDown.Size = new Size(120, 23);
            hsSemanalesNumericUpDown.TabIndex = 5;
            hsSemanalesNumericUpDown.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // hsTotalesLabel
            // 
            hsTotalesLabel.AutoSize = true;
            hsTotalesLabel.Location = new Point(43, 125);
            hsTotalesLabel.Name = "hsTotalesLabel";
            hsTotalesLabel.Size = new Size(81, 15);
            hsTotalesLabel.TabIndex = 6;
            hsTotalesLabel.Text = "Horas Totales:";
            // 
            // hsTotalesNumericUpDown
            // 
            hsTotalesNumericUpDown.Location = new Point(140, 123);
            hsTotalesNumericUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            hsTotalesNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            hsTotalesNumericUpDown.Name = "hsTotalesNumericUpDown";
            hsTotalesNumericUpDown.Size = new Size(120, 23);
            hsTotalesNumericUpDown.TabIndex = 7;
            hsTotalesNumericUpDown.Value = new decimal(new int[] { 64, 0, 0, 0 });
            // 
            // planLabel
            // 
            planLabel.AutoSize = true;
            planLabel.Location = new Point(43, 160);
            planLabel.Name = "planLabel";
            planLabel.Size = new Size(33, 15);
            planLabel.TabIndex = 8;
            planLabel.Text = "Plan:";
            // 
            // planComboBox
            // 
            planComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            planComboBox.FormattingEnabled = true;
            planComboBox.Location = new Point(140, 157);
            planComboBox.Name = "planComboBox";
            planComboBox.Size = new Size(280, 23);
            planComboBox.TabIndex = 9;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(255, 210);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(75, 23);
            aceptarButton.TabIndex = 10;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(345, 210);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(75, 23);
            cancelarButton.TabIndex = 11;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // MateriaDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 260);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(planComboBox);
            Controls.Add(planLabel);
            Controls.Add(hsTotalesNumericUpDown);
            Controls.Add(hsTotalesLabel);
            Controls.Add(hsSemanalesNumericUpDown);
            Controls.Add(hsSemanalesLabel);
            Controls.Add(descripcionTextBox);
            Controls.Add(descripcionLabel);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            Name = "MateriaDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Materia";
            ((System.ComponentModel.ISupportInitialize)hsSemanalesNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)hsTotalesNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label idLabel;
        private TextBox idTextBox;
        private Label descripcionLabel;
        private Label hsSemanalesLabel;
        private Label hsTotalesLabel;
        private Label planLabel;
        private TextBox descripcionTextBox;
        private NumericUpDown hsSemanalesNumericUpDown;
        private NumericUpDown hsTotalesNumericUpDown;
        private ComboBox planComboBox;
        private Button aceptarButton;
        private Button cancelarButton;
    }
}