namespace UI.GestiónStock
{
    partial class fmsSolicitudPedido
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
            btnEliminarRenglon = new Button();
            lblProducto = new Label();
            btnAgregarAutomatico = new Button();
            dgvProductosSolicitud = new DataGridView();
            btnAgregar = new Button();
            sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            btnEnviarSolicitud = new Button();
            btnBuscar = new Button();
            lblCodigoSku = new Label();
            txtProductoNombre = new TextBox();
            txtSKU = new TextBox();
            numCantidadBultos = new NumericUpDown();
            lblCantBulto = new Label();
            lblUnidadesSueltas = new Label();
            lblCargaAutomatica = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvProductosSolicitud).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidadBultos).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnEliminarRenglon
            // 
            btnEliminarRenglon.BackColor = Color.Azure;
            btnEliminarRenglon.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminarRenglon.Location = new Point(994, 166);
            btnEliminarRenglon.Name = "btnEliminarRenglon";
            btnEliminarRenglon.Size = new Size(86, 32);
            btnEliminarRenglon.TabIndex = 31;
            btnEliminarRenglon.Text = "Eliminar";
            btnEliminarRenglon.UseVisualStyleBackColor = false;
            btnEliminarRenglon.Click += btnEliminarRenglon_Click_1;
            // 
            // lblProducto
            // 
            lblProducto.AutoEllipsis = true;
            lblProducto.AutoSize = true;
            lblProducto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProducto.Location = new Point(6, 34);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(76, 20);
            lblProducto.TabIndex = 30;
            lblProducto.Text = "Producto:";
            // 
            // btnAgregarAutomatico
            // 
            btnAgregarAutomatico.BackColor = Color.Azure;
            btnAgregarAutomatico.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregarAutomatico.Location = new Point(234, 24);
            btnAgregarAutomatico.Name = "btnAgregarAutomatico";
            btnAgregarAutomatico.Size = new Size(96, 38);
            btnAgregarAutomatico.TabIndex = 28;
            btnAgregarAutomatico.Text = "Cargar";
            btnAgregarAutomatico.UseVisualStyleBackColor = false;
            btnAgregarAutomatico.Click += btnAgregarAutomatico_Click_1;
            // 
            // dgvProductosSolicitud
            // 
            dgvProductosSolicitud.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductosSolicitud.Location = new Point(14, 221);
            dgvProductosSolicitud.Name = "dgvProductosSolicitud";
            dgvProductosSolicitud.Size = new Size(1114, 204);
            dgvProductosSolicitud.TabIndex = 27;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregar.Location = new Point(749, 89);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(84, 32);
            btnAgregar.TabIndex = 32;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEnviarSolicitud
            // 
            btnEnviarSolicitud.BackColor = Color.Azure;
            btnEnviarSolicitud.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEnviarSolicitud.Location = new Point(994, 431);
            btnEnviarSolicitud.Name = "btnEnviarSolicitud";
            btnEnviarSolicitud.Size = new Size(134, 37);
            btnEnviarSolicitud.TabIndex = 33;
            btnEnviarSolicitud.Text = "Confirmar Solicitud";
            btnEnviarSolicitud.UseVisualStyleBackColor = false;
            btnEnviarSolicitud.Click += btnEnviarSolicitud_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.Location = new Point(322, 23);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(84, 32);
            btnBuscar.TabIndex = 34;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // lblCodigoSku
            // 
            lblCodigoSku.AutoEllipsis = true;
            lblCodigoSku.AutoSize = true;
            lblCodigoSku.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCodigoSku.Location = new Point(6, 79);
            lblCodigoSku.Name = "lblCodigoSku";
            lblCodigoSku.Size = new Size(41, 20);
            lblCodigoSku.TabIndex = 36;
            lblCodigoSku.Text = "SKU:";
            // 
            // txtProductoNombre
            // 
            txtProductoNombre.Location = new Point(110, 27);
            txtProductoNombre.Name = "txtProductoNombre";
            txtProductoNombre.Size = new Size(195, 27);
            txtProductoNombre.TabIndex = 37;
            // 
            // txtSKU
            // 
            txtSKU.Location = new Point(149, 79);
            txtSKU.Name = "txtSKU";
            txtSKU.Size = new Size(156, 27);
            txtSKU.TabIndex = 38;
            // 
            // numCantidadBultos
            // 
            numCantidadBultos.Location = new Point(619, 28);
            numCantidadBultos.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidadBultos.Name = "numCantidadBultos";
            numCantidadBultos.Size = new Size(197, 27);
            numCantidadBultos.TabIndex = 39;
            numCantidadBultos.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidadBultos.ValueChanged += numCantidadBultos_ValueChanged;
            // 
            // lblCantBulto
            // 
            lblCantBulto.AutoEllipsis = true;
            lblCantBulto.AutoSize = true;
            lblCantBulto.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblCantBulto.Location = new Point(619, 58);
            lblCantBulto.Name = "lblCantBulto";
            lblCantBulto.Size = new Size(34, 15);
            lblCantBulto.TabIndex = 40;
            lblCantBulto.Text = "Cant:";
            // 
            // lblUnidadesSueltas
            // 
            lblUnidadesSueltas.AutoEllipsis = true;
            lblUnidadesSueltas.AutoSize = true;
            lblUnidadesSueltas.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblUnidadesSueltas.Location = new Point(519, 30);
            lblUnidadesSueltas.Name = "lblUnidadesSueltas";
            lblUnidadesSueltas.Size = new Size(94, 20);
            lblUnidadesSueltas.TabIndex = 41;
            lblUnidadesSueltas.Text = "Cant. Bultos:";
            lblUnidadesSueltas.Click += lblUnidadesSueltas_Click;
            // 
            // lblCargaAutomatica
            // 
            lblCargaAutomatica.AutoEllipsis = true;
            lblCargaAutomatica.AutoSize = true;
            lblCargaAutomatica.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCargaAutomatica.Location = new Point(14, 33);
            lblCargaAutomatica.Name = "lblCargaAutomatica";
            lblCargaAutomatica.Size = new Size(214, 20);
            lblCargaAutomatica.TabIndex = 42;
            lblCargaAutomatica.Text = "Carga automática (Bajo Stock)";
            lblCargaAutomatica.Click += label1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblProducto);
            groupBox1.Controls.Add(btnBuscar);
            groupBox1.Controls.Add(lblCodigoSku);
            groupBox1.Controls.Add(lblUnidadesSueltas);
            groupBox1.Controls.Add(txtProductoNombre);
            groupBox1.Controls.Add(lblCantBulto);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(txtSKU);
            groupBox1.Controls.Add(numCantidadBultos);
            groupBox1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(116, 77);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(839, 127);
            groupBox1.TabIndex = 43;
            groupBox1.TabStop = false;
            groupBox1.Text = "Carga manual ";
            // 
            // fmsSolicitudPedido
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1146, 480);
            Controls.Add(groupBox1);
            Controls.Add(dgvProductosSolicitud);
            Controls.Add(lblCargaAutomatica);
            Controls.Add(btnEnviarSolicitud);
            Controls.Add(btnEliminarRenglon);
            Controls.Add(btnAgregarAutomatico);
            Name = "fmsSolicitudPedido";
            Text = "Solicitud de Pedido";
            Load += fmsSolicitudPedido_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvProductosSolicitud).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantidadBultos).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnEliminarRenglon;
        private Label lblProducto;
        private Button btnAgregarAutomatico;
        private DataGridView dgvProductosSolicitud;
        private Button btnAgregar;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
        private Button btnEnviarSolicitud;
        private Button btnBuscar;
        private Label lblCodigoSku;
        private TextBox txtProductoNombre;
        private TextBox txtSKU;
        private NumericUpDown numCantidadBultos;
        private Label lblCantBulto;
        private Label lblUnidadesSueltas;
        private Label lblCargaAutomatica;
        private GroupBox groupBox1;
    }
}