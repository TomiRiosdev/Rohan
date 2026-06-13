using BLL.DomainDtos;
using BLL.GestiónProveedor.Facade;
using System;


namespace UI.GestiónProveedor
{
    public partial class fmsAsignarProductoAProveedor : Form
    {
        private readonly ProductoProveedorFacade _prodProvService;
        private readonly ProveedorDTO _proveedorOriginal;
        private ProductoDTO? _productoSeleccionado;  

        public fmsAsignarProductoAProveedor
        (
            ProductoProveedorFacade prodProvService,
            ProveedorDTO proveedorSeleccionado 
        )
        {
            InitializeComponent();

            _prodProvService = prodProvService ?? throw new ArgumentNullException(nameof(prodProvService));
            _proveedorOriginal = proveedorSeleccionado ?? throw new ArgumentNullException(nameof(proveedorSeleccionado));
        }

     
    }
}
