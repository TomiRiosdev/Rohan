namespace UI.GestiónProveedor
{
    partial class fmsAsignarProductoAProveedor
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
            lblProveedor = new Label();
            txtProvRazonSocial = new TextBox();
            txtProvNombre = new TextBox();
            lblRazonSocial = new Label();
            lblNombreProveedor = new Label();
            btnAgregar = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            lblCodigoSKU = new Label();
            lblNombreProducto = new Label();
            txtProdNombre = new TextBox();
            txtProdSku = new TextBox();
            label3 = new Label();
            btnBuscar = new Button();
            btnEliminar = new Button();
            btnAtras = new Button();
            dgvProducto = new DataGridView();
            label1 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducto).BeginInit();
            SuspendLayout();
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblProveedor.Location = new Point(0, 0);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(81, 20);
            lblProveedor.TabIndex = 24;
            lblProveedor.Text = "Proveedor";
            // 
            // txtProvRazonSocial
            // 
            txtProvRazonSocial.Location = new Point(111, 75);
            txtProvRazonSocial.Name = "txtProvRazonSocial";
            txtProvRazonSocial.Size = new Size(250, 23);
            txtProvRazonSocial.TabIndex = 22;
            // 
            // txtProvNombre
            // 
            txtProvNombre.Location = new Point(111, 24);
            txtProvNombre.Name = "txtProvNombre";
            txtProvNombre.Size = new Size(250, 23);
            txtProvNombre.TabIndex = 21;
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblRazonSocial.Location = new Point(6, 78);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(95, 20);
            lblRazonSocial.TabIndex = 17;
            lblRazonSocial.Text = "Razon Social";
            // 
            // lblNombreProveedor
            // 
            lblNombreProveedor.AutoSize = true;
            lblNombreProveedor.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblNombreProveedor.Location = new Point(6, 27);
            lblNombreProveedor.Name = "lblNombreProveedor";
            lblNombreProveedor.Size = new Size(66, 20);
            lblNombreProveedor.TabIndex = 16;
            lblNombreProveedor.Text = "Nombre";
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregar.Location = new Point(110, 408);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(91, 55);
            btnAgregar.TabIndex = 15;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblRazonSocial);
            groupBox1.Controls.Add(lblNombreProveedor);
            groupBox1.Controls.Add(txtProvNombre);
            groupBox1.Controls.Add(txtProvRazonSocial);
            groupBox1.Controls.Add(lblProveedor);
            groupBox1.Location = new Point(12, 74);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(379, 120);
            groupBox1.TabIndex = 27;
            groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblCodigoSKU);
            groupBox2.Controls.Add(lblNombreProducto);
            groupBox2.Controls.Add(txtProdNombre);
            groupBox2.Controls.Add(txtProdSku);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(12, 227);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(379, 120);
            groupBox2.TabIndex = 28;
            groupBox2.TabStop = false;
            // 
            // lblCodigoSKU
            // 
            lblCodigoSKU.AutoSize = true;
            lblCodigoSKU.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCodigoSKU.Location = new Point(6, 78);
            lblCodigoSKU.Name = "lblCodigoSKU";
            lblCodigoSKU.Size = new Size(37, 20);
            lblCodigoSKU.TabIndex = 17;
            lblCodigoSKU.Text = "SKU";
            // 
            // lblNombreProducto
            // 
            lblNombreProducto.AutoSize = true;
            lblNombreProducto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblNombreProducto.Location = new Point(6, 27);
            lblNombreProducto.Name = "lblNombreProducto";
            lblNombreProducto.Size = new Size(66, 20);
            lblNombreProducto.TabIndex = 16;
            lblNombreProducto.Text = "Nombre";
            // 
            // txtProdNombre
            // 
            txtProdNombre.Location = new Point(111, 24);
            txtProdNombre.Name = "txtProdNombre";
            txtProdNombre.Size = new Size(250, 23);
            txtProdNombre.TabIndex = 21;
            // 
            // txtProdSku
            // 
            txtProdSku.Location = new Point(111, 75);
            txtProdSku.Name = "txtProdSku";
            txtProdSku.Size = new Size(250, 23);
            txtProdSku.TabIndex = 22;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 29;
            label3.Text = "Producto";
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnBuscar.Location = new Point(307, 353);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(84, 33);
            btnBuscar.TabIndex = 30;
            btnBuscar.Text = "buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Azure;
            btnEliminar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnEliminar.Location = new Point(227, 408);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(91, 55);
            btnEliminar.TabIndex = 30;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnAtras
            // 
            btnAtras.BackColor = Color.Azure;
            btnAtras.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAtras.Location = new Point(800, 441);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(84, 33);
            btnAtras.TabIndex = 31;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // dgvProducto
            // 
            dgvProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducto.Location = new Point(420, 74);
            dgvProducto.Name = "dgvProducto";
            dgvProducto.Size = new Size(464, 326);
            dgvProducto.TabIndex = 32;
            dgvProducto.CellValueChanged += dgvProducto_CellValueChanged;
            dgvProducto.CurrentCellDirtyStateChanged += dgvProducto_CurrentCellDirtyStateChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label1.Location = new Point(307, 20);
            label1.Name = "label1";
            label1.Size = new Size(232, 21);
            label1.TabIndex = 33;
            label1.Text = "Asignar producto a proveedor";
            // 
            // fmsAsignarProductoAProveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(896, 486);
            Controls.Add(label1);
            Controls.Add(btnBuscar);
            Controls.Add(dgvProducto);
            Controls.Add(btnAtras);
            Controls.Add(btnEliminar);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btnAgregar);
            Name = "fmsAsignarProductoAProveedor";
            Text = "Asignar Producto a Proveedor";
            Load += fmsAsignarProductoAProveedor_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProveedor;
        private TextBox txtProvRazonSocial;
        private TextBox txtProvNombre;
        private Label lblRazonSocial;
        private Label lblNombreProveedor;
        private Button btnAgregar;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnBuscar;
        private Label lblCodigoSKU;
        private Label lblNombreProducto;
        private TextBox txtProdNombre;
        private TextBox txtProdSku;
        private Label label3;
        private Button btnEliminar;
        private Button btnAtras;
        private DataGridView dgvProducto;
        private Label label1;
    }
}