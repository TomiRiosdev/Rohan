
namespace UI.GestiónProducto
{
    partial class fmsGestiónProducto
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
            dgvProductos = new DataGridView();
            button1 = new Button();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnProductoEliminado = new Button();
            btnHabilitar = new Button();
            btnBuscar = new Button();
            cboBuscarPor = new ComboBox();
            label1 = new Label();
            txtBusquedaLibre = new TextBox();
            btnAgregarCatUnMed = new Button();
            cboFiltroMaestro = new ComboBox();
            btnLimpiar = new Button();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // dgvProductos
            // 
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(12, 204);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(1086, 444);
            dgvProductos.TabIndex = 0;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
            // 
            // button1
            // 
            button1.BackColor = Color.Azure;
            button1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            button1.Location = new Point(1018, 654);
            button1.Name = "button1";
            button1.Size = new Size(80, 34);
            button1.TabIndex = 1;
            button1.Text = "Atras";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregar.Location = new Point(12, 70);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(104, 49);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.Azure;
            btnModificar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnModificar.Location = new Point(122, 70);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(104, 49);
            btnModificar.TabIndex = 3;
            btnModificar.Text = "Modificar ";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Azure;
            btnEliminar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminar.Location = new Point(342, 71);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(104, 48);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Deshabilitar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnProductoEliminado
            // 
            btnProductoEliminado.BackColor = Color.Azure;
            btnProductoEliminado.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnProductoEliminado.Location = new Point(452, 70);
            btnProductoEliminado.Name = "btnProductoEliminado";
            btnProductoEliminado.Size = new Size(104, 49);
            btnProductoEliminado.TabIndex = 12;
            btnProductoEliminado.Text = "Listar \r\nDeshabilitado";
            btnProductoEliminado.UseVisualStyleBackColor = false;
            btnProductoEliminado.Click += btnProductoEliminado_Click;
            // 
            // btnHabilitar
            // 
            btnHabilitar.BackColor = Color.Azure;
            btnHabilitar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHabilitar.Location = new Point(562, 71);
            btnHabilitar.Name = "btnHabilitar";
            btnHabilitar.Size = new Size(104, 49);
            btnHabilitar.TabIndex = 6;
            btnHabilitar.Text = "Activar ";
            btnHabilitar.UseVisualStyleBackColor = false;
            btnHabilitar.Click += btnHabilitar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.Location = new Point(771, 158);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(69, 33);
            btnBuscar.TabIndex = 7;
            btnBuscar.Text = "Buscar ";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // cboBuscarPor
            // 
            cboBuscarPor.FormattingEnabled = true;
            cboBuscarPor.Location = new Point(112, 164);
            cboBuscarPor.Name = "cboBuscarPor";
            cboBuscarPor.Size = new Size(203, 23);
            cboBuscarPor.TabIndex = 8;
            cboBuscarPor.SelectedIndexChanged += cboBuscarPor_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoEllipsis = true;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 9);
            label1.Name = "label1";
            label1.Size = new Size(181, 30);
            label1.TabIndex = 10;
            label1.Text = "Gestión Producto";
            // 
            // txtBusquedaLibre
            // 
            txtBusquedaLibre.Location = new Point(553, 164);
            txtBusquedaLibre.Name = "txtBusquedaLibre";
            txtBusquedaLibre.Size = new Size(203, 23);
            txtBusquedaLibre.TabIndex = 11;
            txtBusquedaLibre.TextChanged += txtBusquedaLibre_TextChanged;
            // 
            // btnAgregarCatUnMed
            // 
            btnAgregarCatUnMed.BackColor = Color.Azure;
            btnAgregarCatUnMed.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregarCatUnMed.Location = new Point(232, 70);
            btnAgregarCatUnMed.Name = "btnAgregarCatUnMed";
            btnAgregarCatUnMed.Size = new Size(104, 49);
            btnAgregarCatUnMed.TabIndex = 12;
            btnAgregarCatUnMed.Text = "Categoria \r\nUnd.Medida\r\n";
            btnAgregarCatUnMed.UseVisualStyleBackColor = false;
            btnAgregarCatUnMed.Click += btnAgregarCatUnMed_Click;
            // 
            // cboFiltroMaestro
            // 
            cboFiltroMaestro.FormattingEnabled = true;
            cboFiltroMaestro.Location = new Point(330, 164);
            cboFiltroMaestro.Name = "cboFiltroMaestro";
            cboFiltroMaestro.Size = new Size(203, 23);
            cboFiltroMaestro.TabIndex = 13;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.Azure;
            btnLimpiar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.Location = new Point(672, 71);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(104, 49);
            btnLimpiar.TabIndex = 14;
            btnLimpiar.Text = "Actualizar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label2
            // 
            label2.AutoEllipsis = true;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(15, 164);
            label2.Name = "label2";
            label2.Size = new Size(82, 20);
            label2.TabIndex = 15;
            label2.Text = "Buscar por";
            // 
            // fmsGestiónProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1111, 700);
            Controls.Add(label2);
            Controls.Add(btnLimpiar);
            Controls.Add(cboFiltroMaestro);
            Controls.Add(btnAgregarCatUnMed);
            Controls.Add(txtBusquedaLibre);
            Controls.Add(label1);
            Controls.Add(cboBuscarPor);
            Controls.Add(btnBuscar);
            Controls.Add(btnHabilitar);
            Controls.Add(btnProductoEliminado);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(button1);
            Controls.Add(dgvProductos);
            Name = "fmsGestiónProducto";
            Text = "GestionProducto";
            Load += fmsGestiónProducto_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProductos;
        private Button button1;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private Button btnProductoEliminado;
        private Button btnHabilitar;
        private Button btnBuscar;
        private ComboBox cboBuscarPor;
        private Label label1;
        private TextBox txtBusquedaLibre;
        private Button btnAgregarCatUnMed;
        private ComboBox cboFiltroMaestro;
        private Button btnLimpiar;
        private Label label2;
    }
}