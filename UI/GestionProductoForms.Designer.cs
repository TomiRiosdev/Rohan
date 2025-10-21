namespace UI
{
    partial class GestionProductoForms
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            cbxCategoria = new ComboBox();
            cbxUnidadMedida = new ComboBox();
            cbxProveedor = new ComboBox();
            cbxTipoProducto = new ComboBox();
            txbGuid = new TextBox();
            txbNombre = new TextBox();
            lbCodigo = new Label();
            lbNombre = new Label();
            lbCategoria = new Label();
            lbUnidadMedida = new Label();
            lbTipoProducto = new Label();
            lbProveedor = new Label();
            btnAgregar = new Button();
            dgwListaProducto = new DataGridView();
            gbGestionProducto = new GroupBox();
            btnBuscar = new Button();
            btnModificar = new Button();
            btnBajar = new Button();
            btnAtras = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgwListaProducto).BeginInit();
            gbGestionProducto.SuspendLayout();
            SuspendLayout();
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // cbxCategoria
            // 
            cbxCategoria.FormattingEnabled = true;
            cbxCategoria.Location = new Point(126, 142);
            cbxCategoria.Name = "cbxCategoria";
            cbxCategoria.Size = new Size(184, 23);
            cbxCategoria.TabIndex = 0;
            cbxCategoria.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // cbxUnidadMedida
            // 
            cbxUnidadMedida.FormattingEnabled = true;
            cbxUnidadMedida.Location = new Point(126, 194);
            cbxUnidadMedida.Name = "cbxUnidadMedida";
            cbxUnidadMedida.Size = new Size(184, 23);
            cbxUnidadMedida.TabIndex = 1;
            // 
            // cbxProveedor
            // 
            cbxProveedor.FormattingEnabled = true;
            cbxProveedor.Location = new Point(126, 286);
            cbxProveedor.Name = "cbxProveedor";
            cbxProveedor.Size = new Size(184, 23);
            cbxProveedor.TabIndex = 2;
            // 
            // cbxTipoProducto
            // 
            cbxTipoProducto.FormattingEnabled = true;
            cbxTipoProducto.Location = new Point(126, 242);
            cbxTipoProducto.Name = "cbxTipoProducto";
            cbxTipoProducto.Size = new Size(184, 23);
            cbxTipoProducto.TabIndex = 3;
            // 
            // txbGuid
            // 
            txbGuid.Location = new Point(126, 41);
            txbGuid.Name = "txbGuid";
            txbGuid.Size = new Size(54, 23);
            txbGuid.TabIndex = 4;
            // 
            // txbNombre
            // 
            txbNombre.Location = new Point(126, 92);
            txbNombre.Name = "txbNombre";
            txbNombre.Size = new Size(184, 23);
            txbNombre.TabIndex = 5;
            // 
            // lbCodigo
            // 
            lbCodigo.AutoSize = true;
            lbCodigo.Location = new Point(15, 41);
            lbCodigo.Name = "lbCodigo";
            lbCodigo.Size = new Size(46, 15);
            lbCodigo.TabIndex = 6;
            lbCodigo.Text = "Codigo";
            // 
            // lbNombre
            // 
            lbNombre.AutoSize = true;
            lbNombre.Location = new Point(14, 100);
            lbNombre.Name = "lbNombre";
            lbNombre.Size = new Size(51, 15);
            lbNombre.TabIndex = 7;
            lbNombre.Text = "Nombre";
            // 
            // lbCategoria
            // 
            lbCategoria.AutoSize = true;
            lbCategoria.Location = new Point(14, 150);
            lbCategoria.Name = "lbCategoria";
            lbCategoria.Size = new Size(58, 15);
            lbCategoria.TabIndex = 8;
            lbCategoria.Text = "Categoria";
            // 
            // lbUnidadMedida
            // 
            lbUnidadMedida.AutoSize = true;
            lbUnidadMedida.Location = new Point(14, 202);
            lbUnidadMedida.Name = "lbUnidadMedida";
            lbUnidadMedida.Size = new Size(88, 15);
            lbUnidadMedida.TabIndex = 9;
            lbUnidadMedida.Text = "Unidad Medida";
            // 
            // lbTipoProducto
            // 
            lbTipoProducto.AutoSize = true;
            lbTipoProducto.Location = new Point(14, 250);
            lbTipoProducto.Name = "lbTipoProducto";
            lbTipoProducto.Size = new Size(79, 15);
            lbTipoProducto.TabIndex = 10;
            lbTipoProducto.Text = "TipoProducto";
            // 
            // lbProveedor
            // 
            lbProveedor.AutoSize = true;
            lbProveedor.Location = new Point(14, 294);
            lbProveedor.Name = "lbProveedor";
            lbProveedor.Size = new Size(61, 15);
            lbProveedor.TabIndex = 11;
            lbProveedor.Text = "Proveedor";
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(20, 426);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(95, 32);
            btnAgregar.TabIndex = 12;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // dgwListaProducto
            // 
            dgwListaProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgwListaProducto.Location = new Point(376, 71);
            dgwListaProducto.Name = "dgwListaProducto";
            dgwListaProducto.Size = new Size(754, 331);
            dgwListaProducto.TabIndex = 13;
            dgwListaProducto.CellContentClick += dgwListaProducto_CellContentClick;
            // 
            // gbGestionProducto
            // 
            gbGestionProducto.Controls.Add(txbGuid);
            gbGestionProducto.Controls.Add(lbCodigo);
            gbGestionProducto.Controls.Add(txbNombre);
            gbGestionProducto.Controls.Add(lbProveedor);
            gbGestionProducto.Controls.Add(cbxCategoria);
            gbGestionProducto.Controls.Add(lbTipoProducto);
            gbGestionProducto.Controls.Add(cbxUnidadMedida);
            gbGestionProducto.Controls.Add(lbUnidadMedida);
            gbGestionProducto.Controls.Add(cbxProveedor);
            gbGestionProducto.Controls.Add(lbCategoria);
            gbGestionProducto.Controls.Add(cbxTipoProducto);
            gbGestionProducto.Controls.Add(lbNombre);
            gbGestionProducto.Location = new Point(22, 53);
            gbGestionProducto.Name = "gbGestionProducto";
            gbGestionProducto.Size = new Size(348, 349);
            gbGestionProducto.TabIndex = 14;
            gbGestionProducto.TabStop = false;
            gbGestionProducto.Text = "Gestion Producto";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(1023, 426);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(95, 32);
            btnBuscar.TabIndex = 15;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(148, 426);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(95, 32);
            btnModificar.TabIndex = 16;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnBajar
            // 
            btnBajar.Location = new Point(275, 426);
            btnBajar.Name = "btnBajar";
            btnBajar.Size = new Size(95, 32);
            btnBajar.TabIndex = 17;
            btnBajar.Text = "Bajar";
            btnBajar.UseVisualStyleBackColor = true;
            btnBajar.Click += btnBajar_Click;
            // 
            // btnAtras
            // 
            btnAtras.Location = new Point(1023, 538);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(95, 32);
            btnAtras.TabIndex = 18;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(386, 53);
            label1.Name = "label1";
            label1.Size = new Size(104, 15);
            label1.TabIndex = 12;
            label1.Text = "Lista de Productos";
            // 
            // GestionProductoForms
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1142, 595);
            Controls.Add(label1);
            Controls.Add(btnAtras);
            Controls.Add(btnBajar);
            Controls.Add(btnModificar);
            Controls.Add(btnBuscar);
            Controls.Add(gbGestionProducto);
            Controls.Add(dgwListaProducto);
            Controls.Add(btnAgregar);
            Name = "GestionProductoForms";
            Text = "Gestion de Producto";
            ((System.ComponentModel.ISupportInitialize)dgwListaProducto).EndInit();
            gbGestionProducto.ResumeLayout(false);
            gbGestionProducto.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private ComboBox cbxCategoria;
        private ComboBox cbxUnidadMedida;
        private ComboBox cbxProveedor;
        private ComboBox cbxTipoProducto;
        private TextBox txbGuid;
        private TextBox txbNombre;
        private Label lbCodigo;
        private Label lbNombre;
        private Label lbCategoria;
        private Label lbUnidadMedida;
        private Label lbTipoProducto;
        private Label lbProveedor;
        private Button btnAgregar;
        private DataGridView dgwListaProducto;
        private GroupBox gbGestionProducto;
        private Button btnBuscar;
        private Button btnModificar;
        private Button btnBajar;
        private Button btnAtras;
        private Label label1;
    }
}
