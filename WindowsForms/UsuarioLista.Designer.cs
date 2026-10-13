namespace WindowsForms
{
    partial class UsuarioLista
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
            buscarButton = new Button();
            usuarioDataGridView = new DataGridView();
            agregarButton = new Button();
            modificarButton = new Button();
            eliminarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)usuarioDataGridView).BeginInit();
            SuspendLayout();
            // 
            // buscarTextBox
            // 
            buscarTextBox.Location = new Point(44, 34);
            buscarTextBox.Name = "buscarTextBox";
            buscarTextBox.Size = new Size(502, 39);
            buscarTextBox.TabIndex = 0;
            // 
            // buscarButton
            // 
            buscarButton.Location = new Point(572, 34);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(209, 39);
            buscarButton.TabIndex = 1;
            buscarButton.Text = "BUSCAR";
            buscarButton.UseVisualStyleBackColor = true;
            buscarButton.Click += buscarButton_Click;
            // 
            // usuarioDataGridView
            // 
            usuarioDataGridView.AllowUserToAddRows = false;
            usuarioDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            usuarioDataGridView.Location = new Point(45, 99);
            usuarioDataGridView.MultiSelect = false;
            usuarioDataGridView.Name = "usuarioDataGridView";
            usuarioDataGridView.ReadOnly = true;
            usuarioDataGridView.RowHeadersWidth = 82;
            usuarioDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            usuarioDataGridView.Size = new Size(1099, 469);
            usuarioDataGridView.TabIndex = 2;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(45, 597);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(150, 46);
            agregarButton.TabIndex = 3;
            agregarButton.Text = "AGREGAR";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // modificarButton
            // 
            modificarButton.Location = new Point(210, 597);
            modificarButton.Name = "modificarButton";
            modificarButton.Size = new Size(150, 46);
            modificarButton.TabIndex = 4;
            modificarButton.Text = "MODIFICAR";
            modificarButton.UseVisualStyleBackColor = true;
            modificarButton.Click += modificarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Location = new Point(377, 597);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(150, 46);
            eliminarButton.TabIndex = 5;
            eliminarButton.Text = "ELIMINAR";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // UsuarioLista
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1190, 663);
            Controls.Add(eliminarButton);
            Controls.Add(modificarButton);
            Controls.Add(agregarButton);
            Controls.Add(usuarioDataGridView);
            Controls.Add(buscarButton);
            Controls.Add(buscarTextBox);
            Name = "UsuarioLista";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestión de Usuarios (Alumnos y Profesores)";
            Load += UsuarioLista_Load;
            ((System.ComponentModel.ISupportInitialize)usuarioDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox buscarTextBox;
        private Button buscarButton;
        private DataGridView usuarioDataGridView;
        private Button agregarButton;
        private Button modificarButton;
        private Button eliminarButton;
    }
}