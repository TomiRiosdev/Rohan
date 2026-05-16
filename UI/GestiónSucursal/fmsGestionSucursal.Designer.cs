namespace UI.GestiónSucursal
{
    partial class fmsGestionSucursal
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
            txtBuscar = new TextBox();
            cbxBuscar = new ComboBox();
            label2 = new Label();
            btnBuscar = new Button();
            label1 = new Label();
            btnActualizar = new Button();
            btnListarDeshabilitados = new Button();
            btnActivar = new Button();
            btnDeshabilitar = new Button();
            btnModificar = new Button();
            btnAgregar = new Button();
            dgvSucursal = new DataGridView();
            btnAgregarTipoSucursal = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSucursal).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(257, 163);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(224, 23);
            txtBuscar.TabIndex = 23;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // cbxBuscar
            // 
            cbxBuscar.FormattingEnabled = true;
            cbxBuscar.Location = new Point(104, 163);
            cbxBuscar.Name = "cbxBuscar";
            cbxBuscar.Size = new Size(140, 23);
            cbxBuscar.TabIndex = 22;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 163);
            label2.Name = "label2";
            label2.Size = new Size(86, 21);
            label2.TabIndex = 21;
            label2.Text = "Buscar Por";
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnBuscar.Location = new Point(487, 158);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(69, 33);
            btnBuscar.TabIndex = 20;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(170, 30);
            label1.TabIndex = 19;
            label1.Text = "Gestión Sucursal";
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.Azure;
            btnActualizar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnActualizar.Location = new Point(562, 71);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(104, 49);
            btnActualizar.TabIndex = 18;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnListarDeshabilitados
            // 
            btnListarDeshabilitados.BackColor = Color.Azure;
            btnListarDeshabilitados.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnListarDeshabilitados.Location = new Point(342, 71);
            btnListarDeshabilitados.Name = "btnListarDeshabilitados";
            btnListarDeshabilitados.Size = new Size(104, 49);
            btnListarDeshabilitados.TabIndex = 17;
            btnListarDeshabilitados.Text = "Listar \r\nDeshabilitados\r\n";
            btnListarDeshabilitados.UseVisualStyleBackColor = false;
            btnListarDeshabilitados.Click += btnListarDeshabilitados_Click;
            // 
            // btnActivar
            // 
            btnActivar.BackColor = Color.Azure;
            btnActivar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnActivar.Location = new Point(452, 71);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(104, 49);
            btnActivar.TabIndex = 16;
            btnActivar.Text = "Activar";
            btnActivar.UseVisualStyleBackColor = false;
            btnActivar.Click += btnActivar_Click;
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.BackColor = Color.Azure;
            btnDeshabilitar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnDeshabilitar.Location = new Point(232, 71);
            btnDeshabilitar.Name = "btnDeshabilitar";
            btnDeshabilitar.Size = new Size(104, 49);
            btnDeshabilitar.TabIndex = 15;
            btnDeshabilitar.Text = "Deshabilitar ";
            btnDeshabilitar.UseVisualStyleBackColor = false;
            btnDeshabilitar.Click += btnDeshabilitar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.Azure;
            btnModificar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnModificar.Location = new Point(122, 71);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(104, 49);
            btnModificar.TabIndex = 14;
            btnModificar.Text = "Modificar ";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregar.Location = new Point(12, 71);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(104, 49);
            btnAgregar.TabIndex = 13;
            btnAgregar.Text = "Agregar ";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // dgvSucursal
            // 
            dgvSucursal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSucursal.Location = new Point(12, 224);
            dgvSucursal.Name = "dgvSucursal";
            dgvSucursal.Size = new Size(1087, 440);
            dgvSucursal.TabIndex = 24;
            // 
            // btnAgregarTipoSucursal
            // 
            btnAgregarTipoSucursal.BackColor = Color.Azure;
            btnAgregarTipoSucursal.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregarTipoSucursal.Location = new Point(672, 71);
            btnAgregarTipoSucursal.Name = "btnAgregarTipoSucursal";
            btnAgregarTipoSucursal.Size = new Size(104, 49);
            btnAgregarTipoSucursal.TabIndex = 26;
            btnAgregarTipoSucursal.Text = "Agregar Tipo Sucursal";
            btnAgregarTipoSucursal.UseVisualStyleBackColor = false;
            btnAgregarTipoSucursal.Click += btnAgregarTipoSucursal_Click;
            // 
            // fmsGestionSucursal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1111, 700);
            Controls.Add(btnAgregarTipoSucursal);
            Controls.Add(dgvSucursal);
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
            Name = "fmsGestionSucursal";
            Text = "Gestion de Sucursal";
            Load += fmsGestionSucursal_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSucursal).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBuscar;
        private ComboBox cbxBuscar;
        private Label label2;
        private Button btnBuscar;
        private Label label1;
        private Button btnActualizar;
        private Button btnListarDeshabilitados;
        private Button btnActivar;
        private Button btnDeshabilitar;
        private Button btnModificar;
        private Button btnAgregar;
        private DataGridView dgvSucursal;
        private Button btnAgregarTipoSucursal;
    }
}