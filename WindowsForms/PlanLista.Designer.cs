namespace WindowsForms
{
    partial class PlanLista
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
            planDataGridView = new DataGridView();
            eliminarButton = new Button();
            actualizaButton = new Button();
            agregarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)planDataGridView).BeginInit();
            SuspendLayout();
            // 
            // planDataGridView
            // 
            planDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            planDataGridView.Location = new Point(22, 28);
            planDataGridView.Name = "planDataGridView";
            planDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            planDataGridView.Size = new Size(806, 293);
            planDataGridView.TabIndex = 0;
            // 
            // eliminarButton
            // 
            eliminarButton.Location = new Point(567, 344);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(75, 23);
            eliminarButton.TabIndex = 1;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // actualizaButton
            // 
            actualizaButton.Location = new Point(661, 344);
            actualizaButton.Name = "actualizaButton";
            actualizaButton.Size = new Size(75, 23);
            actualizaButton.TabIndex = 2;
            actualizaButton.Text = "Actualizar";
            actualizaButton.UseVisualStyleBackColor = true;
            actualizaButton.Click += actualizaButton_Click;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(753, 344);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(75, 23);
            agregarButton.TabIndex = 3;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(854, 387);
            Controls.Add(agregarButton);
            Controls.Add(actualizaButton);
            Controls.Add(eliminarButton);
            Controls.Add(planDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)planDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView planDataGridView;
        private Button eliminarButton;
        private Button actualizaButton;
        private Button agregarButton;
    }
}