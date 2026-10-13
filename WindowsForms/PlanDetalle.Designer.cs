namespace WindowsForms
{
    partial class PlanDetalle
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
            especialidadLabel = new Label();
            especialidadComboBox = new ComboBox();
            idTextBox = new TextBox();
            descripcionTextBox = new TextBox();
            materiaDataGridView = new DataGridView();
            agregarButton = new Button();
            modificarButton = new Button();
            eliminarButton = new Button();
            cancelarButton = new Button();
            aceptarButton = new Button();
            descripcionLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)materiaDataGridView).BeginInit();
            SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(79, 18);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(23, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id: ";
            // 
            // especialidadLabel
            // 
            especialidadLabel.AutoSize = true;
            especialidadLabel.Location = new Point(27, 61);
            especialidadLabel.Name = "especialidadLabel";
            especialidadLabel.Size = new Size(75, 15);
            especialidadLabel.TabIndex = 1;
            especialidadLabel.Text = "Especialidad:";
            // 
            // especialidadComboBox
            // 
            especialidadComboBox.FormattingEnabled = true;
            especialidadComboBox.Location = new Point(112, 53);
            especialidadComboBox.Name = "especialidadComboBox";
            especialidadComboBox.Size = new Size(121, 23);
            especialidadComboBox.TabIndex = 2;
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(112, 10);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(100, 23);
            idTextBox.TabIndex = 3;
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(112, 93);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(100, 23);
            descripcionTextBox.TabIndex = 4;
            // 
            // materiaDataGridView
            // 
            materiaDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            materiaDataGridView.Location = new Point(30, 139);
            materiaDataGridView.Name = "materiaDataGridView";
            materiaDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            materiaDataGridView.Size = new Size(656, 246);
            materiaDataGridView.TabIndex = 5;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(395, 391);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(75, 23);
            agregarButton.TabIndex = 6;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // modificarButton
            // 
            modificarButton.Location = new Point(496, 391);
            modificarButton.Name = "modificarButton";
            modificarButton.Size = new Size(75, 23);
            modificarButton.TabIndex = 7;
            modificarButton.Text = "Modificar";
            modificarButton.UseVisualStyleBackColor = true;
            modificarButton.Click += modificarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Location = new Point(592, 391);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(75, 23);
            eliminarButton.TabIndex = 8;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(487, 428);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(75, 30);
            cancelarButton.TabIndex = 9;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(611, 428);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(75, 30);
            aceptarButton.TabIndex = 10;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // descripcionLabel
            // 
            descripcionLabel.AutoSize = true;
            descripcionLabel.Location = new Point(30, 101);
            descripcionLabel.Name = "descripcionLabel";
            descripcionLabel.Size = new Size(72, 15);
            descripcionLabel.TabIndex = 11;
            descripcionLabel.Text = "Descripción:";
            // 
            // PlanDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(721, 470);
            Controls.Add(descripcionLabel);
            Controls.Add(aceptarButton);
            Controls.Add(cancelarButton);
            Controls.Add(eliminarButton);
            Controls.Add(modificarButton);
            Controls.Add(agregarButton);
            Controls.Add(materiaDataGridView);
            Controls.Add(descripcionTextBox);
            Controls.Add(idTextBox);
            Controls.Add(especialidadComboBox);
            Controls.Add(especialidadLabel);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "PlanDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Plan - Detalle";
            ((System.ComponentModel.ISupportInitialize)materiaDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private Label especialidadLabel;
        private ComboBox especialidadComboBox;
        private TextBox idTextBox;
        private TextBox descripcionTextBox;
        private DataGridView materiaDataGridView;
        private Button agregarButton;
        private Button modificarButton;
        private Button eliminarButton;
        private Button cancelarButton;
        private Button aceptarButton;
        private Label descripcionLabel;
    }
}