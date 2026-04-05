using BLL.GestiónSucursal.Facade;
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

namespace UI.GestiónSucursal
{
    public partial class fmsGestionSucursal : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SucursalFacade _sucursalFacade;
        private readonly TipoSucursalFacade _tipoSucursalFacade;
        public fmsGestionSucursal
        (
            IServiceProvider serviceProvider,
            SucursalFacade sucursalFacade,
            TipoSucursalFacade tipoSucursalFacade

        )
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _sucursalFacade = sucursalFacade;
            _tipoSucursalFacade = tipoSucursalFacade;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var frmCrear = _serviceProvider.GetRequiredService<fmsCrearSucursal>();
            frmCrear.ShowDialog();
        }
    }
}
