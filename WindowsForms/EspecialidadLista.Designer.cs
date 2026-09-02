namespace WindowsForms
{
    partial class EspecialidadLista
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
            buscarTextBox = new TextBox();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            buscarButton = new Button();
            especialidadesDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)especialidadesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // buscarTextBox
            // 
            buscarTextBox.Location = new Point(24, 24);
            buscarTextBox.Name = "buscarTextBox";
            buscarTextBox.PlaceholderText = "Ingrese el nombre";
            buscarTextBox.Size = new Size(232, 23);
            buscarTextBox.TabIndex = 11;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(659, 337);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(75, 23);
            agregarButton.TabIndex = 10;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Location = new Point(558, 337);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(75, 23);
            actualizarButton.TabIndex = 9;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Location = new Point(461, 337);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(75, 23);
            eliminarButton.TabIndex = 8;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // buscarButton
            // 
            buscarButton.Location = new Point(262, 24);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(75, 23);
            buscarButton.TabIndex = 7;
            buscarButton.Text = "Buscar";
            buscarButton.UseVisualStyleBackColor = true;
            buscarButton.Click += buscarButton_Click;
            // 
            // especialidadesDataGridView
            // 
            especialidadesDataGridView.AllowUserToAddRows = false;
            especialidadesDataGridView.AllowUserToDeleteRows = false;
            especialidadesDataGridView.AllowUserToOrderColumns = true;
            especialidadesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            especialidadesDataGridView.Location = new Point(24, 52);
            especialidadesDataGridView.MultiSelect = false;
            especialidadesDataGridView.Name = "especialidadesDataGridView";
            especialidadesDataGridView.ReadOnly = true;
            especialidadesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            especialidadesDataGridView.Size = new Size(710, 265);
            especialidadesDataGridView.TabIndex = 6;
            // 
            // EspecialidadLista
            // 
            AcceptButton = buscarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(757, 375);
            Controls.Add(buscarTextBox);
            Controls.Add(agregarButton);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(buscarButton);
            Controls.Add(especialidadesDataGridView);
            Name = "EspecialidadLista";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Especialidades";
            Load += EspecialidadLista_Load;
            ((System.ComponentModel.ISupportInitialize)especialidadesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox buscarTextBox;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
        private Button buscarButton;
        private DataGridView especialidadesDataGridView;
    }
}