namespace UI
{
    partial class fmsPrincipal
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
            btnGestionProducto = new Button();
            btnGestionProveedor = new Button();
            btnGestionSucursal = new Button();
            btnGestionUsuario = new Button();
            btnGestionStock = new Button();
            btnCerrarSesión = new Button();
            lblSucursalDireccion = new Label();
            btnCambiarSucursal = new Button();
            lblAdministrador = new Label();
            panelContenedor = new Panel();
            SuspendLayout();
            // 
            // btnGestionProducto
            // 
            btnGestionProducto.BackColor = Color.Azure;
            btnGestionProducto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnGestionProducto.Location = new Point(12, 393);
            btnGestionProducto.Name = "btnGestionProducto";
            btnGestionProducto.Size = new Size(93, 60);
            btnGestionProducto.TabIndex = 0;
            btnGestionProducto.Text = "Gestión Productos";
            btnGestionProducto.UseVisualStyleBackColor = false;
            btnGestionProducto.Click += btnGestionProducto_Click;
            // 
            // btnGestionProveedor
            // 
            btnGestionProveedor.BackColor = Color.Azure;
            btnGestionProveedor.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnGestionProveedor.Location = new Point(12, 473);
            btnGestionProveedor.Name = "btnGestionProveedor";
            btnGestionProveedor.Size = new Size(93, 61);
            btnGestionProveedor.TabIndex = 1;
            btnGestionProveedor.Text = "Gestión Proveedor";
            btnGestionProveedor.UseVisualStyleBackColor = false;
            btnGestionProveedor.Click += btnGestionProveedor_Click;
            // 
            // btnGestionSucursal
            // 
            btnGestionSucursal.BackColor = Color.Azure;
            btnGestionSucursal.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnGestionSucursal.Location = new Point(12, 558);
            btnGestionSucursal.Name = "btnGestionSucursal";
            btnGestionSucursal.Size = new Size(93, 56);
            btnGestionSucursal.TabIndex = 2;
            btnGestionSucursal.Text = "Gestión Sucursal";
            btnGestionSucursal.UseVisualStyleBackColor = false;
            btnGestionSucursal.Click += btnGestionSucursal_Click;
            // 
            // btnGestionUsuario
            // 
            btnGestionUsuario.BackColor = Color.Azure;
            btnGestionUsuario.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnGestionUsuario.Location = new Point(12, 635);
            btnGestionUsuario.Name = "btnGestionUsuario";
            btnGestionUsuario.Size = new Size(93, 52);
            btnGestionUsuario.TabIndex = 3;
            btnGestionUsuario.Text = "Gestión Usuario";
            btnGestionUsuario.UseVisualStyleBackColor = false;
            btnGestionUsuario.Click += btnGestionUsuario_Click;
            // 
            // btnGestionStock
            // 
            btnGestionStock.BackColor = Color.Azure;
            btnGestionStock.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnGestionStock.Location = new Point(12, 75);
            btnGestionStock.Name = "btnGestionStock";
            btnGestionStock.Size = new Size(93, 59);
            btnGestionStock.TabIndex = 4;
            btnGestionStock.Text = "Gestión Stock";
            btnGestionStock.UseVisualStyleBackColor = false;
            // 
            // btnCerrarSesión
            // 
            btnCerrarSesión.BackColor = Color.Azure;
            btnCerrarSesión.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnCerrarSesión.Location = new Point(1170, 716);
            btnCerrarSesión.Name = "btnCerrarSesión";
            btnCerrarSesión.Size = new Size(142, 35);
            btnCerrarSesión.TabIndex = 5;
            btnCerrarSesión.Text = "Cerrar Sesión";
            btnCerrarSesión.UseVisualStyleBackColor = false;
            btnCerrarSesión.Click += btnCerrarSesión_Click;
            // 
            // lblSucursalDireccion
            // 
            lblSucursalDireccion.AutoSize = true;
            lblSucursalDireccion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblSucursalDireccion.Location = new Point(1087, 9);
            lblSucursalDireccion.Name = "lblSucursalDireccion";
            lblSucursalDireccion.Size = new Size(52, 15);
            lblSucursalDireccion.TabIndex = 6;
            lblSucursalDireccion.Text = "Sucursal";
            // 
            // btnCambiarSucursal
            // 
            btnCambiarSucursal.BackColor = Color.Azure;
            btnCambiarSucursal.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnCambiarSucursal.Location = new Point(12, 695);
            btnCambiarSucursal.Name = "btnCambiarSucursal";
            btnCambiarSucursal.Size = new Size(93, 56);
            btnCambiarSucursal.TabIndex = 7;
            btnCambiarSucursal.Text = "Cambiar sucursal";
            btnCambiarSucursal.UseVisualStyleBackColor = false;
            btnCambiarSucursal.Click += btnCambiarSucursal_Click;
            // 
            // lblAdministrador
            // 
            lblAdministrador.AutoSize = true;
            lblAdministrador.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblAdministrador.Location = new Point(12, 9);
            lblAdministrador.Name = "lblAdministrador";
            lblAdministrador.Size = new Size(83, 15);
            lblAdministrador.TabIndex = 8;
            lblAdministrador.Text = "Administrador";
            // 
            // panelContenedor
            // 
            panelContenedor.Location = new Point(129, 33);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1183, 677);
            panelContenedor.TabIndex = 9;
            // 
            // fmsPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1327, 761);
            Controls.Add(panelContenedor);
            Controls.Add(lblAdministrador);
            Controls.Add(btnCambiarSucursal);
            Controls.Add(lblSucursalDireccion);
            Controls.Add(btnCerrarSesión);
            Controls.Add(btnGestionStock);
            Controls.Add(btnGestionUsuario);
            Controls.Add(btnGestionSucursal);
            Controls.Add(btnGestionProveedor);
            Controls.Add(btnGestionProducto);
            Name = "fmsPrincipal";
            Text = "Principal";
            Load += fmsPrincipal_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGestionProducto;
        private Button btnGestionProveedor;
        private Button btnGestionSucursal;
        private Button btnGestionUsuario;
        private Button btnGestionStock;
        private Button btnCerrarSesión;
        private Label lblSucursalDireccion;
        private Button btnCambiarSucursal;
        private Label lblAdministrador;
        private Panel panelContenedor;
    }
}