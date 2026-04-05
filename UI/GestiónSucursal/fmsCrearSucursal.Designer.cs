namespace UI.GestiónSucursal
{
    partial class fmsCrearSucursal
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
            txtTel = new TextBox();
            label1 = new Label();
            btnAtras = new Button();
            txtEmail = new TextBox();
            txtDireccion = new TextBox();
            txtNombre = new TextBox();
            lblCuit = new Label();
            lblTelefono = new Label();
            lblEmail = new Label();
            lblRazonSocial = new Label();
            lblNombre = new Label();
            btnAgregar = new Button();
            txtCodPostal = new TextBox();
            label2 = new Label();
            cbxTipoSucursal = new ComboBox();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // txtTel
            // 
            txtTel.Location = new Point(111, 190);
            txtTel.Name = "txtTel";
            txtTel.Size = new Size(273, 27);
            txtTel.TabIndex = 27;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(154, 9);
            label1.Name = "label1";
            label1.Size = new Size(107, 20);
            label1.TabIndex = 25;
            label1.Text = "Crear Sucursal";
            // 
            // btnAtras
            // 
            btnAtras.BackColor = Color.Azure;
            btnAtras.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAtras.Location = new Point(354, 464);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(73, 33);
            btnAtras.TabIndex = 24;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = false;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(111, 91);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(273, 27);
            txtEmail.TabIndex = 23;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(111, 138);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(273, 27);
            txtDireccion.TabIndex = 22;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(111, 40);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(273, 27);
            txtNombre.TabIndex = 21;
            // 
            // lblCuit
            // 
            lblCuit.AutoSize = true;
            lblCuit.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCuit.Location = new Point(6, 239);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(85, 20);
            lblCuit.TabIndex = 20;
            lblCuit.Text = "Cod. postal";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblTelefono.Location = new Point(6, 197);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(68, 20);
            lblTelefono.TabIndex = 19;
            lblTelefono.Text = "Telefono";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblEmail.Location = new Point(6, 145);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(74, 20);
            lblEmail.TabIndex = 18;
            lblEmail.Text = "Dirección";
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblRazonSocial.Location = new Point(6, 98);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(46, 20);
            lblRazonSocial.TabIndex = 17;
            lblRazonSocial.Text = "Email";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblNombre.Location = new Point(6, 43);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(66, 20);
            lblNombre.TabIndex = 16;
            lblNombre.Text = "Nombre";
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregar.Location = new Point(160, 348);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(89, 38);
            btnAgregar.TabIndex = 15;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            // 
            // txtCodPostal
            // 
            txtCodPostal.Location = new Point(111, 232);
            txtCodPostal.Name = "txtCodPostal";
            txtCodPostal.Size = new Size(273, 27);
            txtCodPostal.TabIndex = 28;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(6, 287);
            label2.Name = "label2";
            label2.Size = new Size(95, 20);
            label2.TabIndex = 29;
            label2.Text = "Tip. Sucursal";
            // 
            // cbxTipoSucursal
            // 
            cbxTipoSucursal.FormattingEnabled = true;
            cbxTipoSucursal.Location = new Point(111, 279);
            cbxTipoSucursal.Name = "cbxTipoSucursal";
            cbxTipoSucursal.Size = new Size(273, 28);
            cbxTipoSucursal.TabIndex = 30;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblNombre);
            groupBox1.Controls.Add(cbxTipoSucursal);
            groupBox1.Controls.Add(lblRazonSocial);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(lblEmail);
            groupBox1.Controls.Add(txtCodPostal);
            groupBox1.Controls.Add(lblTelefono);
            groupBox1.Controls.Add(txtTel);
            groupBox1.Controls.Add(lblCuit);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(txtDireccion);
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 52);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(405, 392);
            groupBox1.TabIndex = 31;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos de la sucursal";
            // 
            // fmsCrearSucursal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(439, 509);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(btnAtras);
            Name = "fmsCrearSucursal";
            Text = "fmsCrearSucursal";
            Load += fmsCrearSucursal_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTel;
        private MaskedTextBox mtxtCuit;
        private Label label1;
        private Button btnAtras;
        private TextBox txtEmail;
        private TextBox txtDireccion;
        private TextBox txtNombre;
        private Label lblCuit;
        private Label lblTelefono;
        private Label lblEmail;
        private Label lblRazonSocial;
        private Label lblNombre;
        private Button btnAgregar;
        private TextBox txtCodPostal;
        private Label label2;
        private ComboBox cbxTipoSucursal;
        private GroupBox groupBox1;
    }
}