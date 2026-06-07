namespace UI.GestiónStock
{
    partial class fmsInventario
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label2 = new Label();
            txtBusquedaLibre = new TextBox();
            cboBuscarPor = new ComboBox();
            btnBuscar = new Button();
            btnActualizar = new Button();
            dgvInventario = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvInventario).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoEllipsis = true;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 19);
            label2.Name = "label2";
            label2.Size = new Size(82, 20);
            label2.TabIndex = 20;
            label2.Text = "Buscar por";
            // 
            // txtBusquedaLibre
            // 
            txtBusquedaLibre.Location = new Point(343, 20);
            txtBusquedaLibre.Name = "txtBusquedaLibre";
            txtBusquedaLibre.Size = new Size(203, 23);
            txtBusquedaLibre.TabIndex = 18;
            txtBusquedaLibre.TextChanged += txtBusquedaLibre_TextChanged_1;
            // 
            // cboBuscarPor
            // 
            cboBuscarPor.FormattingEnabled = true;
            cboBuscarPor.Location = new Point(109, 19);
            cboBuscarPor.Name = "cboBuscarPor";
            cboBuscarPor.Size = new Size(203, 23);
            cboBuscarPor.TabIndex = 17;
            cboBuscarPor.SelectedIndexChanged += cboBuscarPor_SelectedIndexChanged_1;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.Location = new Point(570, 19);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(84, 29);
            btnBuscar.TabIndex = 16;
            btnBuscar.Text = "Buscar ";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.Azure;
            btnActualizar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnActualizar.Location = new Point(675, 19);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(84, 29);
            btnActualizar.TabIndex = 21;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // dgvInventario
            // 
            dgvInventario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = Color.PowderBlue;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.Desktop;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvInventario.DefaultCellStyle = dataGridViewCellStyle1;
            dgvInventario.Location = new Point(12, 64);
            dgvInventario.Name = "dgvInventario";
            dgvInventario.Size = new Size(1122, 404);
            dgvInventario.TabIndex = 22;
            dgvInventario.CellDoubleClick += dgvInventario_CellDoubleClick;
            dgvInventario.CellFormatting += dgvInventario_CellFormatting;
            // 
            // fmsInventario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1146, 480);
            Controls.Add(dgvInventario);
            Controls.Add(btnActualizar);
            Controls.Add(label2);
            Controls.Add(txtBusquedaLibre);
            Controls.Add(cboBuscarPor);
            Controls.Add(btnBuscar);
            Name = "fmsInventario";
            Text = "Inventario";
            Load += fmsInventario_Load;
            ((System.ComponentModel.ISupportInitialize)dgvInventario).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox txtBusquedaLibre;
        private ComboBox cboBuscarPor;
        private Button btnBuscar;
        private Button btnActualizar;
        private DataGridView dgvInventario;
    }
}