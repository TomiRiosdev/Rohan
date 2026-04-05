namespace UI.GestiónProveedor
{
    partial class fmsModificarProveedor
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
            label1 = new Label();
            btnAtras = new Button();
            txtTelefono = new TextBox();
            txtEmail = new TextBox();
            txtRazonSocial = new TextBox();
            txtNombre = new TextBox();
            lblCuit = new Label();
            lblTelefono = new Label();
            lblEmail = new Label();
            lblRazonSocial = new Label();
            lblNombre = new Label();
            btnModificar = new Button();
            mtxtCuit = new MaskedTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(112, 9);
            label1.Name = "label1";
            label1.Size = new Size(151, 20);
            label1.TabIndex = 25;
            label1.Text = "Modificar Proveedor";
            // 
            // btnAtras
            // 
            btnAtras.BackColor = Color.Azure;
            btnAtras.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAtras.Location = new Point(299, 393);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(73, 33);
            btnAtras.TabIndex = 24;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(122, 228);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(250, 23);
            txtTelefono.TabIndex = 22;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(122, 180);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(250, 23);
            txtEmail.TabIndex = 21;
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.Location = new Point(122, 137);
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.Size = new Size(250, 23);
            txtRazonSocial.TabIndex = 20;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(122, 86);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(250, 23);
            txtNombre.TabIndex = 19;
            // 
            // lblCuit
            // 
            lblCuit.AutoSize = true;
            lblCuit.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCuit.Location = new Point(17, 277);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(53, 20);
            lblCuit.TabIndex = 18;
            lblCuit.Text = "C.U.I.T";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblTelefono.Location = new Point(17, 236);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(68, 20);
            lblTelefono.TabIndex = 17;
            lblTelefono.Text = "Telefono";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblEmail.Location = new Point(17, 188);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 16;
            lblEmail.Text = "Email";
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblRazonSocial.Location = new Point(17, 145);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(95, 20);
            lblRazonSocial.TabIndex = 15;
            lblRazonSocial.Text = "Razon Social";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblNombre.Location = new Point(17, 94);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(66, 20);
            lblNombre.TabIndex = 14;
            lblNombre.Text = "Nombre";
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.Azure;
            btnModificar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnModificar.Location = new Point(152, 326);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(84, 33);
            btnModificar.TabIndex = 13;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // mtxtCuit
            // 
            mtxtCuit.Location = new Point(122, 271);
            mtxtCuit.Mask = "00-00000000-0";
            mtxtCuit.Name = "mtxtCuit";
            mtxtCuit.Size = new Size(250, 23);
            mtxtCuit.TabIndex = 26;
            // 
            // fmsModificarProveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(384, 438);
            Controls.Add(mtxtCuit);
            Controls.Add(label1);
            Controls.Add(btnAtras);
            Controls.Add(txtTelefono);
            Controls.Add(txtEmail);
            Controls.Add(txtRazonSocial);
            Controls.Add(txtNombre);
            Controls.Add(lblCuit);
            Controls.Add(lblTelefono);
            Controls.Add(lblEmail);
            Controls.Add(lblRazonSocial);
            Controls.Add(lblNombre);
            Controls.Add(btnModificar);
            Name = "fmsModificarProveedor";
            Text = "s";
            Load += fmsModificarProveedor_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnAtras;
        private TextBox txtTelefono;
        private TextBox txtEmail;
        private TextBox txtRazonSocial;
        private TextBox txtNombre;
        private Label lblCuit;
        private Label lblTelefono;
        private Label lblEmail;
        private Label lblRazonSocial;
        private Label lblNombre;
        private Button btnModificar;
        private MaskedTextBox mtxtCuit;
    }
}