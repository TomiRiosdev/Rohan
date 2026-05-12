namespace UI.GestionUsuario
{
    partial class fmsAgregarUsuario
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
            groupBox1 = new GroupBox();
            txtConfContraseña = new TextBox();
            label4 = new Label();
            txtEmail = new TextBox();
            label3 = new Label();
            lblNombre = new Label();
            lblRazonSocial = new Label();
            cbxSucursal = new ComboBox();
            label2 = new Label();
            btnAgregar = new Button();
            lblTelefono = new Label();
            lblEmail = new Label();
            txtNombreUsuario = new TextBox();
            txtTelefono = new TextBox();
            txtContraseña = new TextBox();
            txtNombre = new TextBox();
            label1 = new Label();
            btnAtras = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtConfContraseña);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(lblNombre);
            groupBox1.Controls.Add(lblRazonSocial);
            groupBox1.Controls.Add(cbxSucursal);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(lblTelefono);
            groupBox1.Controls.Add(lblEmail);
            groupBox1.Controls.Add(txtNombreUsuario);
            groupBox1.Controls.Add(txtTelefono);
            groupBox1.Controls.Add(txtContraseña);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(4, 52);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(457, 491);
            groupBox1.TabIndex = 34;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del usuario";
            // 
            // txtConfContraseña
            // 
            txtConfContraseña.Location = new Point(166, 199);
            txtConfContraseña.Name = "txtConfContraseña";
            txtConfContraseña.Size = new Size(273, 27);
            txtConfContraseña.TabIndex = 34;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label4.Location = new Point(3, 206);
            label4.Name = "label4";
            label4.Size = new Size(157, 20);
            label4.TabIndex = 33;
            label4.Text = "Confirmar contraseña";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(166, 255);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(273, 27);
            txtEmail.TabIndex = 32;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label3.Location = new Point(8, 148);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 31;
            label3.Text = "Contraseña";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblNombre.Location = new Point(6, 46);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(121, 20);
            lblNombre.TabIndex = 16;
            lblNombre.Text = "Nombre usuario";
            lblNombre.Click += lblNombre_Click;
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblRazonSocial.Location = new Point(8, 262);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(46, 20);
            lblRazonSocial.TabIndex = 17;
            lblRazonSocial.Text = "Email";
            // 
            // cbxSucursal
            // 
            cbxSucursal.FormattingEnabled = true;
            cbxSucursal.Location = new Point(166, 370);
            cbxSucursal.Name = "cbxSucursal";
            cbxSucursal.Size = new Size(273, 28);
            cbxSucursal.TabIndex = 30;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(8, 378);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 29;
            label2.Text = "Sucursal";
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregar.Location = new Point(156, 432);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(89, 38);
            btnAgregar.TabIndex = 15;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblTelefono.Location = new Point(8, 319);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(68, 20);
            lblTelefono.TabIndex = 19;
            lblTelefono.Text = "Telefono";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblEmail.Location = new Point(8, 101);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(66, 20);
            lblEmail.TabIndex = 18;
            lblEmail.Text = "Nombre";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new Point(166, 43);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(273, 27);
            txtNombreUsuario.TabIndex = 21;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(166, 312);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(273, 27);
            txtTelefono.TabIndex = 28;
            // 
            // txtContraseña
            // 
            txtContraseña.Location = new Point(166, 141);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(273, 27);
            txtContraseña.TabIndex = 22;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(166, 94);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(273, 27);
            txtNombre.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(146, 9);
            label1.Name = "label1";
            label1.Size = new Size(103, 20);
            label1.TabIndex = 33;
            label1.Text = "Crear Usuario";
            // 
            // btnAtras
            // 
            btnAtras.BackColor = Color.Azure;
            btnAtras.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAtras.Location = new Point(388, 563);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(73, 33);
            btnAtras.TabIndex = 32;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // fmsAgregarUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(470, 608);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(btnAtras);
            Name = "fmsAgregarUsuario";
            Text = "fmsAgregarUsuario";
            Load += fmsAgregarUsuario_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtEmail;
        private Label label3;
        private Label lblNombre;
        private ComboBox cbxSucursal;
        private Label lblRazonSocial;
        private Label label2;
        private Button btnAgregar;
        private Label lblTelefono;
        private Label lblEmail;
        private TextBox txtNombreUsuario;
        private TextBox txtTelefono;
        private TextBox txtContraseña;
        private TextBox txtNombre;
        private Label label1;
        private Button btnAtras;
        private Label label4;
        private TextBox txtConfContraseña;
    }
}