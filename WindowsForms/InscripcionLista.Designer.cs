namespace WindowsForms
{
    partial class InscripcionLista
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
            inscripcionDataGridView = new DataGridView();
            buscarButton = new Button();
            eliminarButton = new Button();
            actualizarButton = new Button();
            agregarButton = new Button();
            buscarTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)inscripcionDataGridView).BeginInit();
            SuspendLayout();
            // 
            // inscripcionDataGridView
            // 
            inscripcionDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            inscripcionDataGridView.Location = new Point(21, 62);
            inscripcionDataGridView.Name = "inscripcionDataGridView";
            inscripcionDataGridView.Size = new Size(789, 325);
            inscripcionDataGridView.TabIndex = 0;
            // 
            // buscarButton
            // 
            buscarButton.Location = new Point(287, 23);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(75, 23);
            buscarButton.TabIndex = 1;
            buscarButton.Text = "Buscar";
            buscarButton.UseVisualStyleBackColor = true;
            buscarButton.Click += buscarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Location = new Point(535, 407);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(75, 23);
            eliminarButton.TabIndex = 2;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Location = new Point(639, 407);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(75, 23);
            actualizarButton.TabIndex = 3;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(735, 407);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(75, 23);
            agregarButton.TabIndex = 4;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // buscarTextBox
            // 
            buscarTextBox.Location = new Point(21, 23);
            buscarTextBox.Name = "buscarTextBox";
            buscarTextBox.PlaceholderText = "Ingrese el nombre";
            buscarTextBox.Size = new Size(247, 23);
            buscarTextBox.TabIndex = 5;
            // 
            // InscripcionLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 455);
            Controls.Add(buscarTextBox);
            Controls.Add(agregarButton);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(buscarButton);
            Controls.Add(inscripcionDataGridView);
            Name = "InscripcionLista";
            Text = "Inscripciones";
            ((System.ComponentModel.ISupportInitialize)inscripcionDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView inscripcionDataGridView;
        private Button buscarButton;
        private Button eliminarButton;
        private Button actualizarButton;
        private Button agregarButton;
        private TextBox buscarTextBox;
    }
}