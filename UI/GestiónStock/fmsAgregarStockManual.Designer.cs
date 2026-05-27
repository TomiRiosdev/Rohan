namespace UI.GestiónStock
{
    partial class fmsAgregarStockManual
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
            nupCantidad = new NumericUpDown();
            txtObservacion = new TextBox();
            lblProducto = new Label();
            lblCantidad = new Label();
            lblObservaciones = new Label();
            btnAgregar = new Button();
            btnBuscar = new Button();
            groupBox1 = new GroupBox();
            cxbTipoMovimiento = new ComboBox();
            lblTipoMoviemnto = new Label();
            txtProducto = new TextBox();
            label1 = new Label();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)nupCantidad).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // nupCantidad
            // 
            nupCantidad.Location = new Point(216, 84);
            nupCantidad.Name = "nupCantidad";
            nupCantidad.Size = new Size(152, 23);
            nupCantidad.TabIndex = 0;
            // 
            // txtObservacion
            // 
            txtObservacion.Location = new Point(148, 196);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(223, 23);
            txtObservacion.TabIndex = 2;
            txtObservacion.TextChanged += txtObservacion_TextChanged;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblProducto.Location = new Point(5, 40);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(72, 20);
            lblProducto.TabIndex = 7;
            lblProducto.Text = "Producto";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCantidad.Location = new Point(5, 87);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(70, 20);
            lblCantidad.TabIndex = 8;
            lblCantidad.Text = "Cantidad";
            // 
            // lblObservaciones
            // 
            lblObservaciones.AutoSize = true;
            lblObservaciones.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblObservaciones.Location = new Point(5, 195);
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.Size = new Size(109, 20);
            lblObservaciones.TabIndex = 9;
            lblObservaciones.Text = "Observaciones";
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnAgregar.Location = new Point(187, 240);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(92, 38);
            btnAgregar.TabIndex = 10;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnBuscar.Location = new Point(396, 31);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(89, 29);
            btnBuscar.TabIndex = 11;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cxbTipoMovimiento);
            groupBox1.Controls.Add(lblTipoMoviemnto);
            groupBox1.Controls.Add(txtProducto);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(btnBuscar);
            groupBox1.Controls.Add(nupCantidad);
            groupBox1.Controls.Add(lblObservaciones);
            groupBox1.Controls.Add(txtObservacion);
            groupBox1.Controls.Add(lblCantidad);
            groupBox1.Controls.Add(lblProducto);
            groupBox1.Location = new Point(12, 32);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(491, 284);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            // 
            // cxbTipoMovimiento
            // 
            cxbTipoMovimiento.FormattingEnabled = true;
            cxbTipoMovimiento.Location = new Point(148, 135);
            cxbTipoMovimiento.Name = "cxbTipoMovimiento";
            cxbTipoMovimiento.Size = new Size(223, 23);
            cxbTipoMovimiento.TabIndex = 14;
            // 
            // lblTipoMoviemnto
            // 
            lblTipoMoviemnto.AutoSize = true;
            lblTipoMoviemnto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblTipoMoviemnto.Location = new Point(6, 138);
            lblTipoMoviemnto.Name = "lblTipoMoviemnto";
            lblTipoMoviemnto.Size = new Size(92, 20);
            lblTipoMoviemnto.TabIndex = 13;
            lblTipoMoviemnto.Text = "Movimiento";
            // 
            // txtProducto
            // 
            txtProducto.Location = new Point(148, 36);
            txtProducto.Name = "txtProducto";
            txtProducto.Size = new Size(223, 23);
            txtProducto.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(157, 9);
            label1.Name = "label1";
            label1.Size = new Size(187, 20);
            label1.TabIndex = 12;
            label1.Text = "Agregar producto al stock";
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.Azure;
            btnCerrar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnCerrar.Location = new Point(411, 322);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(92, 38);
            btnCerrar.TabIndex = 13;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // fmsAgregarStockManual
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(515, 372);
            Controls.Add(btnCerrar);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Name = "fmsAgregarStockManual";
            Text = "Agregar Stock Manual";
            Load += fmsAgregarStockManual_Load;
            ((System.ComponentModel.ISupportInitialize)nupCantidad).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown nupCantidad;
        private TextBox txtObservacion;
        private Label lblProducto;
        private Label lblCantidad;
        private Label lblObservaciones;
        private Button btnAgregar;
        private Button btnBuscar;
        private GroupBox groupBox1;
        private Label label1;
        private Button btnCerrar;
        private TextBox txtProducto;
        private ComboBox cxbTipoMovimiento;
        private Label lblTipoMoviemnto;
    }
}