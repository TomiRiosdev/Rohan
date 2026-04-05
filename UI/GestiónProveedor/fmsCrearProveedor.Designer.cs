namespace UI.GestiónProveedor
{
    partial class fmsCrearProveedor
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
            btnAgregar = new Button();
            lblNombre = new Label();
            lblRazonSocial = new Label();
            lblEmail = new Label();
            lblTelefono = new Label();
            lblCuit = new Label();
            txtNombre = new TextBox();
            txtRazonSocial = new TextBox();
            txtEmail = new TextBox();
            btnAtras = new Button();
            label1 = new Label();
            mtxtCuit = new MaskedTextBox();
            txtTel = new TextBox();
            SuspendLayout();
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregar.Location = new Point(149, 333);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(84, 33);
            btnAgregar.TabIndex = 0;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblNombre.Location = new Point(14, 96);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(66, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre";
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblRazonSocial.Location = new Point(14, 147);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(95, 20);
            lblRazonSocial.TabIndex = 2;
            lblRazonSocial.Text = "Razon Social";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblEmail.Location = new Point(14, 190);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 3;
            lblEmail.Text = "Email";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblTelefono.Location = new Point(14, 238);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(68, 20);
            lblTelefono.TabIndex = 4;
            lblTelefono.Text = "Telefono";
            // 
            // lblCuit
            // 
            lblCuit.AutoSize = true;
            lblCuit.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCuit.Location = new Point(14, 279);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(53, 20);
            lblCuit.TabIndex = 5;
            lblCuit.Text = "C.U.I.T";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(119, 88);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(250, 23);
            txtNombre.TabIndex = 6;
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.Location = new Point(119, 139);
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.Size = new Size(250, 23);
            txtRazonSocial.TabIndex = 7;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(119, 182);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(250, 23);
            txtEmail.TabIndex = 8;
            // 
            // btnAtras
            // 
            btnAtras.BackColor = Color.Azure;
            btnAtras.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAtras.Location = new Point(296, 404);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(73, 33);
            btnAtras.TabIndex = 11;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(111, 9);
            label1.Name = "label1";
            label1.Size = new Size(122, 20);
            label1.TabIndex = 12;
            label1.Text = "Crear Proveedor";
            // 
            // mtxtCuit
            // 
            mtxtCuit.Location = new Point(119, 279);
            mtxtCuit.Mask = "00-00000000-0";
            mtxtCuit.Name = "mtxtCuit";
            mtxtCuit.Size = new Size(250, 23);
            mtxtCuit.TabIndex = 13;
            // 
            // txtTel
            // 
            txtTel.Location = new Point(119, 230);
            txtTel.Name = "txtTel";
            txtTel.Size = new Size(250, 23);
            txtTel.TabIndex = 14;
            // 
            // fmsCrearProveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(386, 452);
            Controls.Add(txtTel);
            Controls.Add(mtxtCuit);
            Controls.Add(label1);
            Controls.Add(btnAtras);
            Controls.Add(txtEmail);
            Controls.Add(txtRazonSocial);
            Controls.Add(txtNombre);
            Controls.Add(lblCuit);
            Controls.Add(lblTelefono);
            Controls.Add(lblEmail);
            Controls.Add(lblRazonSocial);
            Controls.Add(lblNombre);
            Controls.Add(btnAgregar);
            Name = "fmsCrearProveedor";
            Text = "fmsCrearProveedor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAgregar;
        private Label lblNombre;
        private Label lblRazonSocial;
        private Label lblEmail;
        private Label lblTelefono;
        private Label lblCuit;
        private TextBox txtNombre;
        private TextBox txtRazonSocial;
        private TextBox txtEmail;
        private TextBox txtTelefono;
        private TextBox txtTel;
        private TextBox txtCuit;
        private Button btnAtras;
        private Label label1;
        private MaskedTextBox mtxtCuit;
        
    }
}