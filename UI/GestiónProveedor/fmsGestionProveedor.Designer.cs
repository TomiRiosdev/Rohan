namespace UI.GestiónProveedor
{
    partial class fmsGestionProveedor
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
            btnModificar = new Button();
            btnDeshabilitar = new Button();
            btnActivar = new Button();
            btnListarDeshabilitados = new Button();
            btnActualizar = new Button();
            dgvProveedor = new DataGridView();
            label1 = new Label();
            btnBuscar = new Button();
            label2 = new Label();
            cbxBuscar = new ComboBox();
            txtBuscar = new TextBox();
            btnAgregarProducto = new Button();
            dgvProductoProveedor = new DataGridView();
            groupBox1 = new GroupBox();
            label4 = new Label();
            groupBox2 = new GroupBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvProveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductoProveedor).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregar.Location = new Point(12, 71);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(104, 49);
            btnAgregar.TabIndex = 0;
            btnAgregar.Text = "Agregar ";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.Azure;
            btnModificar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnModificar.Location = new Point(122, 71);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(104, 49);
            btnModificar.TabIndex = 1;
            btnModificar.Text = "Modificar ";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.BackColor = Color.Azure;
            btnDeshabilitar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnDeshabilitar.Location = new Point(232, 71);
            btnDeshabilitar.Name = "btnDeshabilitar";
            btnDeshabilitar.Size = new Size(104, 49);
            btnDeshabilitar.TabIndex = 2;
            btnDeshabilitar.Text = "Deshabilitar ";
            btnDeshabilitar.UseVisualStyleBackColor = false;
            btnDeshabilitar.Click += btnDeshabilitar_Click;
            // 
            // btnActivar
            // 
            btnActivar.BackColor = Color.Azure;
            btnActivar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnActivar.Location = new Point(452, 71);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(104, 49);
            btnActivar.TabIndex = 3;
            btnActivar.Text = "Activar";
            btnActivar.UseVisualStyleBackColor = false;
            btnActivar.Click += btnActivar_Click;
            // 
            // btnListarDeshabilitados
            // 
            btnListarDeshabilitados.BackColor = Color.Azure;
            btnListarDeshabilitados.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnListarDeshabilitados.Location = new Point(342, 71);
            btnListarDeshabilitados.Name = "btnListarDeshabilitados";
            btnListarDeshabilitados.Size = new Size(104, 49);
            btnListarDeshabilitados.TabIndex = 4;
            btnListarDeshabilitados.Text = "Listar \r\nDeshabilitados\r\n";
            btnListarDeshabilitados.UseVisualStyleBackColor = false;
            btnListarDeshabilitados.Click += btnListarDeshabilitados_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.Azure;
            btnActualizar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnActualizar.Location = new Point(562, 71);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(104, 49);
            btnActualizar.TabIndex = 5;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // dgvProveedor
            // 
            dgvProveedor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProveedor.Location = new Point(7, 23);
            dgvProveedor.Name = "dgvProveedor";
            dgvProveedor.Size = new Size(794, 417);
            dgvProveedor.TabIndex = 6;
            dgvProveedor.SelectionChanged += dgvProveedor_SelectionChanged_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(192, 30);
            label1.TabIndex = 7;
            label1.Text = "Gestión Proveedor";
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnBuscar.Location = new Point(453, 137);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(69, 33);
            btnBuscar.TabIndex = 9;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 142);
            label2.Name = "label2";
            label2.Size = new Size(86, 21);
            label2.TabIndex = 10;
            label2.Text = "Buscar Por";
            // 
            // cbxBuscar
            // 
            cbxBuscar.FormattingEnabled = true;
            cbxBuscar.Location = new Point(104, 142);
            cbxBuscar.Name = "cbxBuscar";
            cbxBuscar.Size = new Size(140, 23);
            cbxBuscar.TabIndex = 11;
            cbxBuscar.SelectedIndexChanged += cbxBuscar_SelectedIndexChanged;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(257, 142);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(189, 23);
            txtBuscar.TabIndex = 12;
            // 
            // btnAgregarProducto
            // 
            btnAgregarProducto.BackColor = Color.Azure;
            btnAgregarProducto.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregarProducto.Location = new Point(672, 71);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(104, 49);
            btnAgregarProducto.TabIndex = 13;
            btnAgregarProducto.Text = "Agregar producto";
            btnAgregarProducto.UseVisualStyleBackColor = false;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            // 
            // dgvProductoProveedor
            // 
            dgvProductoProveedor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductoProveedor.Location = new Point(6, 22);
            dgvProductoProveedor.Name = "dgvProductoProveedor";
            dgvProductoProveedor.Size = new Size(345, 417);
            dgvProductoProveedor.TabIndex = 14;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(dgvProductoProveedor);
            groupBox1.Font = new Font("Segoe UI", 9F);
            groupBox1.Location = new Point(816, 187);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(357, 446);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label4.Location = new Point(0, 0);
            label4.Name = "label4";
            label4.Size = new Size(66, 19);
            label4.TabIndex = 18;
            label4.Text = "Producto";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(dgvProveedor);
            groupBox2.Font = new Font("Segoe UI", 9F);
            groupBox2.Location = new Point(3, 187);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(807, 446);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label3.Location = new Point(6, 0);
            label3.Name = "label3";
            label3.Size = new Size(72, 19);
            label3.TabIndex = 17;
            label3.Text = "Proveedor";
            // 
            // fmsGestionProveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1175, 638);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btnAgregarProducto);
            Controls.Add(txtBuscar);
            Controls.Add(cbxBuscar);
            Controls.Add(label2);
            Controls.Add(btnBuscar);
            Controls.Add(label1);
            Controls.Add(btnActualizar);
            Controls.Add(btnListarDeshabilitados);
            Controls.Add(btnActivar);
            Controls.Add(btnDeshabilitar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Name = "fmsGestionProveedor";
            Text = "Gestion de Proveedor";
            Load += fmsGestionProveedor_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductoProveedor).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAgregar;
        private Button btnModificar;
        private Button btnDeshabilitar;
        private Button btnActivar;
        private Button btnListarDeshabilitados;
        private Button btnActualizar;
        private DataGridView dgvProveedor;
        private Label label1;
        private Button btnBuscar;
        private Label label2;
        private ComboBox cbxBuscar;
        private TextBox txtBuscar;
        private Button btnAgregarProducto;
        private DataGridView dgvProductoProveedor;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label4;
        private Label label3;
    }
}