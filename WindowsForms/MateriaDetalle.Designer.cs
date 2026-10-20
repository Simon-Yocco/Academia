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
            descripcionLabel = new Label();
            hsSemanalesLabel = new Label();
            hsTotalesLabel = new Label();
            descripcionTextBox = new TextBox();
            hsSemanalesNumericUpDown = new NumericUpDown();
            hsTotalesNumericUpDown = new NumericUpDown();
            aceptarButton = new Button();
            cancelarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)hsSemanalesNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hsTotalesNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // descripcionLabel
            // 
            descripcionLabel.AutoSize = true;
            descripcionLabel.Location = new Point(43, 47);
            descripcionLabel.Name = "descripcionLabel";
            descripcionLabel.Size = new Size(72, 15);
            descripcionLabel.TabIndex = 1;
            descripcionLabel.Text = "Descripción:";
            // 
            // hsSemanalesLabel
            // 
            hsSemanalesLabel.AutoSize = true;
            hsSemanalesLabel.Location = new Point(15, 81);
            hsSemanalesLabel.Name = "hsSemanalesLabel";
            hsSemanalesLabel.Size = new Size(100, 15);
            hsSemanalesLabel.TabIndex = 2;
            hsSemanalesLabel.Text = "Horas Semanales:";
            // 
            // hsTotalesLabel
            // 
            hsTotalesLabel.AutoSize = true;
            hsTotalesLabel.Location = new Point(34, 120);
            hsTotalesLabel.Name = "hsTotalesLabel";
            hsTotalesLabel.Size = new Size(81, 15);
            hsTotalesLabel.TabIndex = 3;
            hsTotalesLabel.Text = "Horas Totales:";
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(123, 39);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(233, 23);
            descripcionTextBox.TabIndex = 5;
            // 
            // hsSemanalesNumericUpDown
            // 
            hsSemanalesNumericUpDown.Location = new Point(123, 73);
            hsSemanalesNumericUpDown.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            hsSemanalesNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            hsSemanalesNumericUpDown.Name = "hsSemanalesNumericUpDown";
            hsSemanalesNumericUpDown.Size = new Size(120, 23);
            hsSemanalesNumericUpDown.TabIndex = 6;
            // 
            // hsTotalesNumericUpDown
            // 
            hsTotalesNumericUpDown.Location = new Point(123, 112);
            hsTotalesNumericUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            hsTotalesNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            hsTotalesNumericUpDown.Name = "hsTotalesNumericUpDown";
            hsTotalesNumericUpDown.Size = new Size(120, 23);
            hsTotalesNumericUpDown.TabIndex = 7;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(251, 201);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(75, 23);
            aceptarButton.TabIndex = 8;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(351, 201);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(75, 23);
            cancelarButton.TabIndex = 9;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // MateriaDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(453, 252);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(hsTotalesNumericUpDown);
            Controls.Add(hsSemanalesNumericUpDown);
            Controls.Add(descripcionTextBox);
            Controls.Add(hsTotalesLabel);
            Controls.Add(hsSemanalesLabel);
            Controls.Add(descripcionLabel);
            Name = "MateriaDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Materia";
            ((System.ComponentModel.ISupportInitialize)hsSemanalesNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)hsTotalesNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label descripcionLabel;
        private Label hsSemanalesLabel;
        private Label hsTotalesLabel;
        private TextBox descripcionTextBox;
        private NumericUpDown hsSemanalesNumericUpDown;
        private NumericUpDown hsTotalesNumericUpDown;
        private Button aceptarButton;
        private Button cancelarButton;
    }
}