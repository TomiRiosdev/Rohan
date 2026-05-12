namespace UI.GestionUsuario
{
    partial class fmsAgregarPermisos
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
            btnEliminar = new Button();
            cbxPermiso = new ComboBox();
            lblNombre = new Label();
            cbxUsuario = new ComboBox();
            label2 = new Label();
            btnAgregar = new Button();
            label1 = new Label();
            btnAtras = new Button();
            dgvUsuarioFamilia = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarioFamilia).BeginInit();
            SuspendLayout();
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Azure;
            btnEliminar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnEliminar.Location = new Point(139, 551);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(93, 43);
            btnEliminar.TabIndex = 15;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // cbxPermiso
            // 
            cbxPermiso.FormattingEnabled = true;
            cbxPermiso.Location = new Point(161, 147);
            cbxPermiso.Name = "cbxPermiso";
            cbxPermiso.Size = new Size(273, 23);
            cbxPermiso.TabIndex = 31;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblNombre.Location = new Point(12, 90);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(121, 20);
            lblNombre.TabIndex = 16;
            lblNombre.Text = "Nombre usuario";
            // 
            // cbxUsuario
            // 
            cbxUsuario.FormattingEnabled = true;
            cbxUsuario.Location = new Point(161, 87);
            cbxUsuario.Name = "cbxUsuario";
            cbxUsuario.Size = new Size(273, 23);
            cbxUsuario.TabIndex = 30;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(12, 147);
            label2.Name = "label2";
            label2.Size = new Size(63, 20);
            label2.TabIndex = 29;
            label2.Text = "Permiso";
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregar.Location = new Point(12, 551);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(98, 43);
            btnAgregar.TabIndex = 15;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(185, 25);
            label1.TabIndex = 35;
            label1.Text = "Gestión de permisos";
            // 
            // btnAtras
            // 
            btnAtras.BackColor = Color.Azure;
            btnAtras.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAtras.Location = new Point(460, 643);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(75, 28);
            btnAtras.TabIndex = 32;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // dgvUsuarioFamilia
            // 
            dgvUsuarioFamilia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarioFamilia.Location = new Point(12, 205);
            dgvUsuarioFamilia.Name = "dgvUsuarioFamilia";
            dgvUsuarioFamilia.Size = new Size(523, 329);
            dgvUsuarioFamilia.TabIndex = 36;
            // 
            // fmsAgregarPermisos
            // 
            AccessibleRole = AccessibleRole.Dialog;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(543, 680);
            Controls.Add(dgvUsuarioFamilia);
            Controls.Add(btnEliminar);
            Controls.Add(btnAgregar);
            Controls.Add(btnAtras);
            Controls.Add(cbxPermiso);
            Controls.Add(lblNombre);
            Controls.Add(cbxUsuario);
            Controls.Add(label1);
            Controls.Add(label2);
            Name = "fmsAgregarPermisos";
            Text = "Gestión de permisos";
            Load += fmsAgregarPermisos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuarioFamilia).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblNombre;
        private ComboBox cbxUsuario;
        private Label label2;
        private Button btnAgregar;
        private Label label1;
        private ComboBox cbxPermiso;
        private Button btnEliminar;
        private Button btnAtras;
        private DataGridView dgvUsuarioFamilia;
    }
}