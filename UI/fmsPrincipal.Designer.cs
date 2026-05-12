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
            SuspendLayout();
            // 
            // btnGestionProducto
            // 
            btnGestionProducto.BackColor = Color.Azure;
            btnGestionProducto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnGestionProducto.Location = new Point(12, 185);
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
            btnGestionProveedor.Location = new Point(12, 265);
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
            btnGestionSucursal.Location = new Point(12, 347);
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
            btnGestionUsuario.Location = new Point(12, 419);
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
            btnGestionStock.Location = new Point(12, 49);
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
            btnCerrarSesión.Location = new Point(12, 613);
            btnCerrarSesión.Name = "btnCerrarSesión";
            btnCerrarSesión.Size = new Size(83, 50);
            btnCerrarSesión.TabIndex = 5;
            btnCerrarSesión.Text = "Cerrar Sesión";
            btnCerrarSesión.UseVisualStyleBackColor = false;
            btnCerrarSesión.Click += btnCerrarSesión_Click;
            // 
            // fmsPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1456, 675);
            Controls.Add(btnCerrarSesión);
            Controls.Add(btnGestionStock);
            Controls.Add(btnGestionUsuario);
            Controls.Add(btnGestionSucursal);
            Controls.Add(btnGestionProveedor);
            Controls.Add(btnGestionProducto);
            Name = "fmsPrincipal";
            Text = "fmsPrincipal";
            ResumeLayout(false);
        }

        #endregion

        private Button btnGestionProducto;
        private Button btnGestionProveedor;
        private Button btnGestionSucursal;
        private Button btnGestionUsuario;
        private Button btnGestionStock;
        private Button btnCerrarSesión;
    }
}