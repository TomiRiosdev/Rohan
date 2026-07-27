using Service.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.GestiónStock
{
    public partial class fmsTraspasoSucursal : Form
    {
       
        public fmsTraspasoSucursal
        (
             
        )
        {
        
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxSucursal.SelectedValue == null)
                {
                    MessageBox.Show("Por favor, seleccione una sucursal para filtrar.", "Atención",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Guid idSucursalSeleccionada = (Guid)cbxSucursal.SelectedValue;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al filtrar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
