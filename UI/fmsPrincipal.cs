using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.GestiónProducto;
using UI.GestiónProveedor;
using UI.GestiónSucursal;

namespace UI
{
    public partial class fmsPrincipal : Form
    {
        private readonly IServiceProvider _serviceProvider;
        public fmsPrincipal
        (
            IServiceProvider serviceProvider
        )
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void btnGestionProducto_Click(object sender, EventArgs e)
        {
            var fmsGestiónProducto = _serviceProvider.GetRequiredService<fmsGestiónProducto>();
            this.Hide();
            fmsGestiónProducto.ShowDialog();

        }

        private void btnCerrarSesión_Click(object sender, EventArgs e)
        {
            var loginForm = _serviceProvider.GetRequiredService<Login>();
            this.Close();
            loginForm.Show();
        }

        private void btnGestionProveedor_Click(object sender, EventArgs e)
        {
            var fmsGestiónProveedor = _serviceProvider.GetRequiredService<fmsGestionProveedor>();
            this.Hide();
            fmsGestiónProveedor.ShowDialog();
        }

        private void btnGestionSucursal_Click(object sender, EventArgs e)
        {
            var fmsGestiónSucursal = _serviceProvider.GetRequiredService<fmsGestionSucursal>();
            this.Hide();
            fmsGestiónSucursal.ShowDialog();
        }
    }
}
