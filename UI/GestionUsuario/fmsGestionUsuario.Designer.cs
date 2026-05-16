namespace UI.GestionUsuario
{
    partial class fmsGestionUsuario
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
            btnGestionPermiso = new Button();
            dgvUsuario = new DataGridView();
            cbxSucursal = new ComboBox();
            label2 = new Label();
            btnBuscar = new Button();
            label1 = new Label();
            btnActualizar = new Button();
            btnListarDeshabilitados = new Button();
            btnActivar = new Button();
            btnDeshabilitar = new Button();
            btnModificar = new Button();
            btnAgregar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsuario).BeginInit();
            SuspendLayout();
            // 
            // btnGestionPermiso
            // 
            btnGestionPermiso.BackColor = Color.Azure;
            btnGestionPermiso.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnGestionPermiso.Location = new Point(672, 73);
            btnGestionPermiso.Name = "btnGestionPermiso";
            btnGestionPermiso.Size = new Size(104, 49);
            btnGestionPermiso.TabIndex = 40;
            btnGestionPermiso.Text = "Gestión de permisos";
            btnGestionPermiso.UseVisualStyleBackColor = false;
            btnGestionPermiso.Click += btnGestionPermiso_Click;
            // 
            // dgvUsuario
            // 
            dgvUsuario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuario.Location = new Point(12, 200);
            dgvUsuario.Name = "dgvUsuario";
            dgvUsuario.Size = new Size(562, 327);
            dgvUsuario.TabIndex = 38;
            // 
            // cbxSucursal
            // 
            cbxSucursal.FormattingEnabled = true;
            cbxSucursal.Location = new Point(168, 159);
            cbxSucursal.Name = "cbxSucursal";
            cbxSucursal.Size = new Size(248, 23);
            cbxSucursal.TabIndex = 36;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 157);
            label2.Name = "label2";
            label2.Size = new Size(150, 21);
            label2.TabIndex = 35;
            label2.Text = "Buscar por sucursal";
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnBuscar.Location = new Point(436, 152);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(69, 33);
            btnBuscar.TabIndex = 34;
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
            label1.Size = new Size(162, 30);
            label1.TabIndex = 33;
            label1.Text = "Gestión usuario";
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.Azure;
            btnActualizar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnActualizar.Location = new Point(562, 73);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(104, 49);
            btnActualizar.TabIndex = 32;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnListarDeshabilitados
            // 
            btnListarDeshabilitados.BackColor = Color.Azure;
            btnListarDeshabilitados.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnListarDeshabilitados.Location = new Point(342, 73);
            btnListarDeshabilitados.Name = "btnListarDeshabilitados";
            btnListarDeshabilitados.Size = new Size(104, 49);
            btnListarDeshabilitados.TabIndex = 31;
            btnListarDeshabilitados.Text = "Listar \r\nDeshabilitados\r\n";
            btnListarDeshabilitados.UseVisualStyleBackColor = false;
            btnListarDeshabilitados.Click += btnListarDeshabilitados_Click;
            // 
            // btnActivar
            // 
            btnActivar.BackColor = Color.Azure;
            btnActivar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnActivar.Location = new Point(452, 73);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(104, 49);
            btnActivar.TabIndex = 30;
            btnActivar.Text = "Activar";
            btnActivar.UseVisualStyleBackColor = false;
            btnActivar.Click += btnActivar_Click;
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.BackColor = Color.Azure;
            btnDeshabilitar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnDeshabilitar.Location = new Point(232, 73);
            btnDeshabilitar.Name = "btnDeshabilitar";
            btnDeshabilitar.Size = new Size(104, 49);
            btnDeshabilitar.TabIndex = 29;
            btnDeshabilitar.Text = "Deshabilitar ";
            btnDeshabilitar.UseVisualStyleBackColor = false;
            btnDeshabilitar.Click += btnDeshabilitar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.Azure;
            btnModificar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnModificar.Location = new Point(122, 73);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(104, 49);
            btnModificar.TabIndex = 28;
            btnModificar.Text = "Modificar ";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregar.Location = new Point(12, 73);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(104, 49);
            btnAgregar.TabIndex = 27;
            btnAgregar.Text = "Agregar ";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // fmsGestionUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(794, 581);
            Controls.Add(btnGestionPermiso);
            Controls.Add(dgvUsuario);
            Controls.Add(cbxSucursal);
            Controls.Add(label2);
            Controls.Add(btnBuscar);
            Controls.Add(label1);
            Controls.Add(btnActualizar);
            Controls.Add(btnListarDeshabilitados);
            Controls.Add(btnActivar);
            Controls.Add(btnDeshabilitar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Name = "fmsGestionUsuario";
            Text = "Gestion de Usuario";
            Load += fmsGestionUsuario_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuario).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGestionPermiso;
        private DataGridView dgvUsuario;
        private ComboBox cbxSucursal;
        private Label label2;
        private Button btnBuscar;
        private Label label1;
        private Button btnActualizar;
        private Button btnListarDeshabilitados;
        private Button btnActivar;
        private Button btnDeshabilitar;
        private Button btnModificar;
        private Button btnAgregar;
    }
}