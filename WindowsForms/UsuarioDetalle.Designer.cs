namespace WindowsForms
{
    partial class UsuarioDetalle
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
            tipoPersonaLabel = new Label();
            tipoPersonaComboBox = new ComboBox();
            legajoLabel = new Label();
            legajoNumericUpDown = new NumericUpDown();
            nombreLabel = new Label();
            nombreTextBox = new TextBox();
            apellidoLabel = new Label();
            apellidoTextBox = new TextBox();
            emailLabel = new Label();
            emailTextBox = new TextBox();
            telefonoLabel = new Label();
            telefonoTextBox = new TextBox();
            direccionLabel = new Label();
            direccionTextBox = new TextBox();
            fechaNacimientoLabel = new Label();
            fechaNacimientoDateTimePicker = new DateTimePicker();
            planLabel = new Label();
            planComboBox = new ComboBox();
            habilitadoCheckBox = new CheckBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)legajoNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // tipoPersonaLabel
            // 
            tipoPersonaLabel.AutoSize = true;
            tipoPersonaLabel.Location = new Point(30, 25);
            tipoPersonaLabel.Name = "tipoPersonaLabel";
            tipoPersonaLabel.Size = new Size(100, 15);
            tipoPersonaLabel.TabIndex = 0;
            tipoPersonaLabel.Text = "Tipo de Persona:";
            // 
            // tipoPersonaComboBox
            // 
            tipoPersonaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tipoPersonaComboBox.FormattingEnabled = true;
            tipoPersonaComboBox.Location = new Point(170, 22);
            tipoPersonaComboBox.Name = "tipoPersonaComboBox";
            tipoPersonaComboBox.Size = new Size(280, 23);
            tipoPersonaComboBox.TabIndex = 1;
            // 
            // legajoLabel
            // 
            legajoLabel.AutoSize = true;
            legajoLabel.Location = new Point(30, 60);
            legajoLabel.Name = "legajoLabel";
            legajoLabel.Size = new Size(45, 15);
            legajoLabel.TabIndex = 2;
            legajoLabel.Text = "Legajo:";
            // 
            // legajoNumericUpDown
            // 
            legajoNumericUpDown.Location = new Point(170, 57);
            legajoNumericUpDown.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            legajoNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            legajoNumericUpDown.Name = "legajoNumericUpDown";
            legajoNumericUpDown.Size = new Size(280, 23);
            legajoNumericUpDown.TabIndex = 3;
            legajoNumericUpDown.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Location = new Point(30, 95);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(54, 15);
            nombreLabel.TabIndex = 4;
            nombreLabel.Text = "Nombre:";
            // 
            // nombreTextBox
            // 
            nombreTextBox.Location = new Point(170, 92);
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(280, 23);
            nombreTextBox.TabIndex = 5;
            // 
            // apellidoLabel
            // 
            apellidoLabel.AutoSize = true;
            apellidoLabel.Location = new Point(30, 130);
            apellidoLabel.Name = "apellidoLabel";
            apellidoLabel.Size = new Size(54, 15);
            apellidoLabel.TabIndex = 6;
            apellidoLabel.Text = "Apellido:";
            // 
            // apellidoTextBox
            // 
            apellidoTextBox.Location = new Point(170, 127);
            apellidoTextBox.Name = "apellidoTextBox";
            apellidoTextBox.Size = new Size(280, 23);
            apellidoTextBox.TabIndex = 7;
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(30, 165);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(39, 15);
            emailLabel.TabIndex = 8;
            emailLabel.Text = "Email:";
            // 
            // emailTextBox
            // 
            emailTextBox.Location = new Point(170, 162);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(280, 23);
            emailTextBox.TabIndex = 9;
            // 
            // telefonoLabel
            // 
            telefonoLabel.AutoSize = true;
            telefonoLabel.Location = new Point(30, 200);
            telefonoLabel.Name = "telefonoLabel";
            telefonoLabel.Size = new Size(55, 15);
            telefonoLabel.TabIndex = 10;
            telefonoLabel.Text = "Teléfono:";
            // 
            // telefonoTextBox
            // 
            telefonoTextBox.Location = new Point(170, 197);
            telefonoTextBox.Name = "telefonoTextBox";
            telefonoTextBox.Size = new Size(280, 23);
            telefonoTextBox.TabIndex = 11;
            // 
            // direccionLabel
            // 
            direccionLabel.AutoSize = true;
            direccionLabel.Location = new Point(30, 235);
            direccionLabel.Name = "direccionLabel";
            direccionLabel.Size = new Size(60, 15);
            direccionLabel.TabIndex = 12;
            direccionLabel.Text = "Dirección:";
            // 
            // direccionTextBox
            // 
            direccionTextBox.Location = new Point(170, 232);
            direccionTextBox.Name = "direccionTextBox";
            direccionTextBox.Size = new Size(280, 23);
            direccionTextBox.TabIndex = 13;
            // 
            // fechaNacimientoLabel
            // 
            fechaNacimientoLabel.AutoSize = true;
            fechaNacimientoLabel.Location = new Point(30, 270);
            fechaNacimientoLabel.Name = "fechaNacimientoLabel";
            fechaNacimientoLabel.Size = new Size(106, 15);
            fechaNacimientoLabel.TabIndex = 14;
            fechaNacimientoLabel.Text = "Fecha Nacimiento:";
            // 
            // fechaNacimientoDateTimePicker
            // 
            fechaNacimientoDateTimePicker.Format = DateTimePickerFormat.Short;
            fechaNacimientoDateTimePicker.Location = new Point(170, 267);
            fechaNacimientoDateTimePicker.Name = "fechaNacimientoDateTimePicker";
            fechaNacimientoDateTimePicker.Size = new Size(280, 23);
            fechaNacimientoDateTimePicker.TabIndex = 15;
            // 
            // planLabel
            // 
            planLabel.AutoSize = true;
            planLabel.Location = new Point(30, 305);
            planLabel.Name = "planLabel";
            planLabel.Size = new Size(93, 15);
            planLabel.TabIndex = 16;
            planLabel.Text = "Plan de Estudio:";
            // 
            // planComboBox
            // 
            planComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            planComboBox.FormattingEnabled = true;
            planComboBox.Location = new Point(170, 302);
            planComboBox.Name = "planComboBox";
            planComboBox.Size = new Size(280, 23);
            planComboBox.TabIndex = 17;
            // 
            // habilitadoCheckBox
            // 
            habilitadoCheckBox.AutoSize = true;
            habilitadoCheckBox.Checked = true;
            habilitadoCheckBox.CheckState = CheckState.Checked;
            habilitadoCheckBox.Location = new Point(170, 345);
            habilitadoCheckBox.Name = "habilitadoCheckBox";
            habilitadoCheckBox.Size = new Size(80, 19);
            habilitadoCheckBox.TabIndex = 18;
            habilitadoCheckBox.Text = "Habilitado";
            habilitadoCheckBox.UseVisualStyleBackColor = true;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(230, 395);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(105, 35);
            aceptarButton.TabIndex = 19;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(345, 395);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(105, 35);
            cancelarButton.TabIndex = 20;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // UsuarioDetalle
            // 
            AcceptButton = aceptarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(494, 450);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(habilitadoCheckBox);
            Controls.Add(planComboBox);
            Controls.Add(planLabel);
            Controls.Add(fechaNacimientoDateTimePicker);
            Controls.Add(fechaNacimientoLabel);
            Controls.Add(direccionTextBox);
            Controls.Add(direccionLabel);
            Controls.Add(telefonoTextBox);
            Controls.Add(telefonoLabel);
            Controls.Add(emailTextBox);
            Controls.Add(emailLabel);
            Controls.Add(apellidoTextBox);
            Controls.Add(apellidoLabel);
            Controls.Add(nombreTextBox);
            Controls.Add(nombreLabel);
            Controls.Add(legajoNumericUpDown);
            Controls.Add(legajoLabel);
            Controls.Add(tipoPersonaComboBox);
            Controls.Add(tipoPersonaLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "UsuarioDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Detalle de Usuario";
            ((System.ComponentModel.ISupportInitialize)legajoNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tipoPersonaLabel;
        private ComboBox tipoPersonaComboBox;
        private Label legajoLabel;
        private NumericUpDown legajoNumericUpDown;
        private Label nombreLabel;
        private TextBox nombreTextBox;
        private Label apellidoLabel;
        private TextBox apellidoTextBox;
        private Label emailLabel;
        private TextBox emailTextBox;
        private Label telefonoLabel;
        private TextBox telefonoTextBox;
        private Label direccionLabel;
        private TextBox direccionTextBox;
        private Label fechaNacimientoLabel;
        private DateTimePicker fechaNacimientoDateTimePicker;
        private Label planLabel;
        private ComboBox planComboBox;
        private CheckBox habilitadoCheckBox;
        private Button aceptarButton;
        private Button cancelarButton;
    }
}