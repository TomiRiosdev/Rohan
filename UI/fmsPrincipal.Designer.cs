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
            btnGestionProducto.Location = new Point(38, 95);
            btnGestionProducto.Name = "btnGestionProducto";
            btnGestionProducto.Size = new Size(74, 46);
            btnGestionProducto.TabIndex = 0;
            btnGestionProducto.Text = "Gestión Productos";
            btnGestionProducto.UseVisualStyleBackColor = true;
            btnGestionProducto.Click += btnGestionProducto_Click;
            // 
            // btnGestionProveedor
            // 
            btnGestionProveedor.Location = new Point(38, 163);
            btnGestionProveedor.Name = "btnGestionProveedor";
            btnGestionProveedor.Size = new Size(74, 46);
            btnGestionProveedor.TabIndex = 1;
            btnGestionProveedor.Text = "Gestión Proveedor";
            btnGestionProveedor.UseVisualStyleBackColor = true;
            btnGestionProveedor.Click += btnGestionProveedor_Click;
            // 
            // btnGestionSucursal
            // 
            btnGestionSucursal.Location = new Point(38, 235);
            btnGestionSucursal.Name = "btnGestionSucursal";
            btnGestionSucursal.Size = new Size(74, 46);
            btnGestionSucursal.TabIndex = 2;
            btnGestionSucursal.Text = "Gestión Sucursal";
            btnGestionSucursal.UseVisualStyleBackColor = true;
            btnGestionSucursal.Click += btnGestionSucursal_Click;
            // 
            // btnGestionUsuario
            // 
            btnGestionUsuario.Location = new Point(38, 314);
            btnGestionUsuario.Name = "btnGestionUsuario";
            btnGestionUsuario.Size = new Size(74, 46);
            btnGestionUsuario.TabIndex = 3;
            btnGestionUsuario.Text = "Gestión Usuario";
            btnGestionUsuario.UseVisualStyleBackColor = true;
            // 
            // btnGestionStock
            // 
            btnGestionStock.Location = new Point(38, 384);
            btnGestionStock.Name = "btnGestionStock";
            btnGestionStock.Size = new Size(74, 46);
            btnGestionStock.TabIndex = 4;
            btnGestionStock.Text = "Gestión Stock";
            btnGestionStock.UseVisualStyleBackColor = true;
            // 
            // btnCerrarSesión
            // 
            btnCerrarSesión.Location = new Point(179, 475);
            btnCerrarSesión.Name = "btnCerrarSesión";
            btnCerrarSesión.Size = new Size(74, 46);
            btnCerrarSesión.TabIndex = 5;
            btnCerrarSesión.Text = "Cerrar Sesión";
            btnCerrarSesión.UseVisualStyleBackColor = true;
            btnCerrarSesión.Click += btnCerrarSesión_Click;
            // 
            // fmsPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(277, 542);
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