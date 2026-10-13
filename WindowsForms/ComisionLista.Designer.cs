namespace WindowsForms
{
    partial class ComisionLista
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
            comisionDataGridView = new DataGridView();
            buscarTextBox = new TextBox();
            buscarButton = new Button();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)comisionDataGridView).BeginInit();
            SuspendLayout();
            // 
            // comisionDataGridView
            // 
            comisionDataGridView.AllowUserToAddRows = false;
            comisionDataGridView.AllowUserToDeleteRows = false;
            comisionDataGridView.AllowUserToOrderColumns = true;
            comisionDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            comisionDataGridView.Location = new Point(21, 50);
            comisionDataGridView.MultiSelect = false;
            comisionDataGridView.Name = "comisionDataGridView";
            comisionDataGridView.ReadOnly = true;
            comisionDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            comisionDataGridView.Size = new Size(756, 308);
            comisionDataGridView.TabIndex = 0;
            // 
            // buscarTextBox
            // 
            buscarTextBox.Location = new Point(21, 12);
            buscarTextBox.Name = "buscarTextBox";
            buscarTextBox.PlaceholderText = "Ingrese el nombre";
            buscarTextBox.Size = new Size(207, 23);
            buscarTextBox.TabIndex = 1;
            // 
            // buscarButton
            // 
            buscarButton.Location = new Point(234, 12);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(75, 23);
            buscarButton.TabIndex = 2;
            buscarButton.Text = "Buscar";
            buscarButton.UseVisualStyleBackColor = true;
            buscarButton.Click += buscarButton_Click;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(702, 367);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(75, 23);
            agregarButton.TabIndex = 7;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Location = new Point(601, 367);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(75, 23);
            actualizarButton.TabIndex = 6;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Location = new Point(504, 367);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(75, 23);
            eliminarButton.TabIndex = 5;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // ComisionLista
            // 
            AcceptButton = buscarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 400);
            Controls.Add(agregarButton);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(buscarButton);
            Controls.Add(buscarTextBox);
            Controls.Add(comisionDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ComisionLista";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Comisiones";
            Load += ComisionLista_Load;
            ((System.ComponentModel.ISupportInitialize)comisionDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView comisionDataGridView;
        private TextBox buscarTextBox;
        private Button buscarButton;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
    }
}