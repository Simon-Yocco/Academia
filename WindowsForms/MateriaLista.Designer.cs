namespace WindowsForms
{
    partial class MateriaLista
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
            materiaDataGridView = new DataGridView();
            buscarTextBox = new TextBox();
            buscarButton = new Button();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)materiaDataGridView).BeginInit();
            SuspendLayout();
            // 
            // materiaDataGridView
            // 
            materiaDataGridView.AllowUserToAddRows = false;
            materiaDataGridView.AllowUserToDeleteRows = false;
            materiaDataGridView.AllowUserToOrderColumns = true;
            materiaDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            materiaDataGridView.Location = new Point(21, 50);
            materiaDataGridView.MultiSelect = false;
            materiaDataGridView.Name = "materiaDataGridView";
            materiaDataGridView.ReadOnly = true;
            materiaDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            materiaDataGridView.Size = new Size(756, 308);
            materiaDataGridView.TabIndex = 0;
            // 
            // buscarTextBox
            // 
            buscarTextBox.Location = new Point(21, 12);
            buscarTextBox.Name = "buscarTextBox";
            buscarTextBox.PlaceholderText = "Ingrese el nombre de la materia o plan";
            buscarTextBox.Size = new Size(250, 23);
            buscarTextBox.TabIndex = 1;
            // 
            // buscarButton
            // 
            buscarButton.Location = new Point(280, 12);
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
            actualizarButton.Text = "Modificar";
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
            // MateriaLista
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
            Controls.Add(materiaDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MateriaLista";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Materias";
            Load += MateriaLista_Load;
            ((System.ComponentModel.ISupportInitialize)materiaDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView materiaDataGridView;
        private TextBox buscarTextBox;
        private Button buscarButton;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
    }
}
