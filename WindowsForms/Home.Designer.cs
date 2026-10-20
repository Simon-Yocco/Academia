namespace WindowsForms
{
    partial class Home
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
            menuStrip1 = new MenuStrip();
            cursosToolStripMenuItem = new ToolStripMenuItem();
            especialidadesToolStripMenuItem = new ToolStripMenuItem();
            planesToolStripMenuItem = new ToolStripMenuItem();
            comisionesToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesionToolStripMenuItem = new ToolStripMenuItem();
            usuariosToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { cursosToolStripMenuItem, especialidadesToolStripMenuItem, planesToolStripMenuItem, comisionesToolStripMenuItem, usuariosToolStripMenuItem, cerrarSesionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(11, 4, 0, 4);
            menuStrip1.Size = new Size(1296, 46);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // cursosToolStripMenuItem
            // 
            cursosToolStripMenuItem.Name = "cursosToolStripMenuItem";
            cursosToolStripMenuItem.Size = new Size(105, 38);
            cursosToolStripMenuItem.Text = "Cursos";
            cursosToolStripMenuItem.Click += cursosToolStripMenuItem_Click;
            // 
            // especialidadesToolStripMenuItem
            // 
            especialidadesToolStripMenuItem.Name = "especialidadesToolStripMenuItem";
            especialidadesToolStripMenuItem.Size = new Size(187, 38);
            especialidadesToolStripMenuItem.Text = "Especialidades";
            especialidadesToolStripMenuItem.Click += especialidadesToolStripMenuItem_Click;
            // 
            // planesToolStripMenuItem
            // 
            planesToolStripMenuItem.Name = "planesToolStripMenuItem";
            planesToolStripMenuItem.Size = new Size(102, 38);
            planesToolStripMenuItem.Text = "Planes";
            planesToolStripMenuItem.Click += planesToolStripMenuItem_Click;
            // 
            // comisionesToolStripMenuItem
            // 
            comisionesToolStripMenuItem.Name = "comisionesToolStripMenuItem";
            comisionesToolStripMenuItem.Size = new Size(157, 38);
            comisionesToolStripMenuItem.Text = "Comisiones";
            comisionesToolStripMenuItem.Click += comisionesToolStripMenuItem_Click;
            // 
            // cerrarSesionToolStripMenuItem
            // 
            cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            cerrarSesionToolStripMenuItem.Size = new Size(175, 38);
            cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
            cerrarSesionToolStripMenuItem.Click += cerrarSesionToolStripMenuItem_Click;
            // 
            // usuariosToolStripMenuItem
            // 
            usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            usuariosToolStripMenuItem.Size = new Size(124, 38);
            usuariosToolStripMenuItem.Text = "Usuarios";
            usuariosToolStripMenuItem.Click += usuariosToolStripMenuItem_Click;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1296, 644);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(6, 6, 6, 6);
            Name = "Home";
            Text = "Home";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem cursosToolStripMenuItem;
        private ToolStripMenuItem especialidadesToolStripMenuItem;
        private ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private ToolStripMenuItem planesToolStripMenuItem;
        private ToolStripMenuItem comisionesToolStripMenuItem;
        private ToolStripMenuItem usuariosToolStripMenuItem;
    }
}