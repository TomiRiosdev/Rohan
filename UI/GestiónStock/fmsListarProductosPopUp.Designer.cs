namespace UI.GestiónStock
{
    partial class fmsListarProductosPopUp
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
            label2 = new Label();
            txtBusqueda = new TextBox();
            cboBuscarPor = new ComboBox();
            btnBuscar = new Button();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // dgvProductos
            // 
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(12, 65);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(643, 176);
            dgvProductos.TabIndex = 0;
            dgvProductos.CellDoubleClick += dgvProductos_CellDoubleClick;
            // 
            // label2
            // 
            label2.AutoEllipsis = true;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(13, 12);
            label2.Name = "label2";
            label2.Size = new Size(82, 20);
            label2.TabIndex = 24;
            label2.Text = "Buscar por";
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(344, 13);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(203, 23);
            txtBusqueda.TabIndex = 23;
            // 
            // cboBuscarPor
            // 
            cboBuscarPor.FormattingEnabled = true;
            cboBuscarPor.Location = new Point(110, 12);
            cboBuscarPor.Name = "cboBuscarPor";
            cboBuscarPor.Size = new Size(203, 23);
            cboBuscarPor.TabIndex = 22;
            cboBuscarPor.SelectedIndexChanged += cboBuscarPor_SelectedIndexChanged;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.Location = new Point(571, 12);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(84, 29);
            btnBuscar.TabIndex = 21;
            btnBuscar.Text = "Buscar ";
            btnBuscar.UseVisualStyleBackColor = false;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.Azure;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrar.Location = new Point(571, 266);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(84, 29);
            btnCerrar.TabIndex = 25;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            // 
            // fmsListarProductosPopUp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(663, 307);
            Controls.Add(btnCerrar);
            Controls.Add(label2);
            Controls.Add(txtBusqueda);
            Controls.Add(cboBuscarPor);
            Controls.Add(btnBuscar);
            Controls.Add(dgvProductos);
            Name = "fmsListarProductosPopUp";
            Text = "fmsListarProductosPopUp";
            Load += fmsListarProductosPopUp_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProductos;
        private Label label2;
        private TextBox txtBusqueda;
        private ComboBox cboBuscarPor;
        private Button btnBuscar;
        private Button btnCerrar;
    }
}