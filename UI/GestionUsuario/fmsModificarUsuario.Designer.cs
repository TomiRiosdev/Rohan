namespace UI.GestionUsuario
{
    partial class fmsModificarUsuario
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
            txtEmail = new TextBox();
            lblNombre = new Label();
            lblRazonSocial = new Label();
            cbxSucursal = new ComboBox();
            label2 = new Label();
            btnModificar = new Button();
            lblTelefono = new Label();
            lblEmail = new Label();
            txtNombreUsuario = new TextBox();
            txtTelefono = new TextBox();
            txtNombre = new TextBox();
            label1 = new Label();
            btnAtras = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(lblNombre);
            groupBox1.Controls.Add(lblRazonSocial);
            groupBox1.Controls.Add(cbxSucursal);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnModificar);
            groupBox1.Controls.Add(lblTelefono);
            groupBox1.Controls.Add(lblEmail);
            groupBox1.Controls.Add(txtNombreUsuario);
            groupBox1.Controls.Add(txtTelefono);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 49);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(420, 365);
            groupBox1.TabIndex = 37;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del usuario";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(133, 145);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(273, 27);
            txtEmail.TabIndex = 32;
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
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblRazonSocial.Location = new Point(8, 152);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(46, 20);
            lblRazonSocial.TabIndex = 17;
            lblRazonSocial.Text = "Email";
            // 
            // cbxSucursal
            // 
            cbxSucursal.FormattingEnabled = true;
            cbxSucursal.Location = new Point(133, 260);
            cbxSucursal.Name = "cbxSucursal";
            cbxSucursal.Size = new Size(273, 28);
            cbxSucursal.TabIndex = 30;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(8, 268);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 29;
            label2.Text = "Sucursal";
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.Azure;
            btnModificar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnModificar.Location = new Point(159, 311);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(89, 38);
            btnModificar.TabIndex = 15;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblTelefono.Location = new Point(8, 209);
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
            txtNombreUsuario.Location = new Point(133, 46);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(273, 27);
            txtNombreUsuario.TabIndex = 21;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(133, 202);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(273, 27);
            txtTelefono.TabIndex = 28;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(133, 94);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(273, 27);
            txtNombre.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(154, 6);
            label1.Name = "label1";
            label1.Size = new Size(128, 20);
            label1.TabIndex = 36;
            label1.Text = "ModificarUsuario";
            // 
            // btnAtras
            // 
            btnAtras.BackColor = Color.Azure;
            btnAtras.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAtras.Location = new Point(359, 441);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(73, 33);
            btnAtras.TabIndex = 35;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // fmsModificarUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(446, 489);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(btnAtras);
            Name = "fmsModificarUsuario";
            Text = "fmsModificarUsuario";
            Load += fmsModificarUsuario_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtEmail;
        private Label lblNombre;
        private Label lblRazonSocial;
        private ComboBox cbxSucursal;
        private Label label2;
        private Button btnModificar;
        private Label lblTelefono;
        private Label lblEmail;
        private TextBox txtNombreUsuario;
        private TextBox txtTelefono;
        private TextBox txtNombre;
        private Label label1;
        private Button btnAtras;
    }
}