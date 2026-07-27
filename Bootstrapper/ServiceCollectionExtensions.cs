using BLL.DomainDtos;
using BLL.GestiónCompra.Interface;
using BLL.GestiónCompra.Service;
using BLL.GestiónCompra.Validator;
using BLL.GestiónProducto.Facade;
using BLL.GestiónProducto.Interface;
using BLL.GestiónProducto.Service;
using BLL.GestiónProducto.Validator;
using BLL.GestiónStock;
using BLL.GestiónStock.Facade;
using BLL.GestiónStock.Interface;
using BLL.GestiónStock.Validator;
using BLL.GestiónSucursal.Facade;
using BLL.GestiónSucursal.Interface;
using BLL.GestiónSucursal.Service;
using BLL.GestiónSucursal.Validator;
using BLL.GestiónProveedor.Facade;
using BLL.GestiónProveedor.Interface;
using BLL.GestiónProveedor.Service;
using BLL.GestiónProveedor.Validator;
using DAO;
using DAO.Implementations.SQLServer;
using Implementations.SQLServer.GestionCompra;
using DAO.Implementations.SQLServer.GestionProducto;
using DAO.Implementations.SQLServer.GestionProveedor;
using DAO.Implementations.SQLServer.GestionStock;
using DAO.Implementations.SQLServer.GestionSucursal;
using DAO.Interface;
using DAO.Interface.GestionCompra;
using DAO.Interface.GestionProducto;
using DAO.Interface.GestionProveedor;
using DAO.Interface.GestionStock;
using DAO.Interface.GestionSucursal;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Service.DateAccess.Implementations;
using Service.DateAccess.Interface;
using Service.Logic;
using BLL.GestiónCompra.Facade;
using BLL.GestiónStock.Service;



namespace Bootstrapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            #region INFRAESTRUCTURA DE PERSISTENCIA (CORE DATA ACCESS)
          
            services.AddScoped<RohanContext>();         
            services.AddTransient<IUnitOfWork, UnitOfWork>();
          
            #endregion

            #region DOMINIO: SEGURIDAD, USUARIOS Y PERMISOS (COMPOSITE PATTERN)
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<PermisosRepository>();
            services.AddScoped<PermisosService>();
            services.AddTransient<UsuarioService>();
            #endregion

            #region DOMINIO: MAESTRO DE PRODUCTOS
            // Repositorios (DAL)
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IProductoRepository, ProductoRepository>();
            services.AddTransient<IUnidadMedidaRepository, UnidadMedidaRepository>();

            // Validadores (FluentValidation)
            services.AddScoped<IValidator<ProductoDTO>, ProductoValidator>();
            services.AddScoped<IValidator<CategoriaDTO>, CategoriaValidator>();
            services.AddScoped<IValidator<UnidadMedidaDTO>, UnidadMedidaValidator>();

            // Servicios de Negocio (BLL)
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IUnidadMedidaService, UnidadMedidaService>();

            // Fachadas (UI Gateway)
            services.AddTransient<ProductoFacade>();
            services.AddTransient<CategoriaFacade>();
            services.AddTransient<UnidadMedidaFacade>();
            #endregion

            #region DOMINIO: PROVEEDORES Y LOGÍSTICA DE COMPRA
            // Repositorios (DAL)
            services.AddTransient<IProveedorRepository, ProveedorRepository>();
            services.AddTransient<IProductoProveedorRepository, ProductoProveedorRepository>();

            // Validadores (FluentValidation)
            services.AddScoped<IValidator<ProveedorDTO>, ProveedorValidator>();
            services.AddScoped<IValidator<ProductoProveedorDTO>, ProductoProveedorValidator>();

            // Servicios de Negocio (BLL)
            services.AddScoped<IProveedorService, ProveedorService>();
            services.AddScoped<IProductoProveedorService, ProductoProveedorService>();

            // Fachadas (UI Gateway)
            services.AddTransient<ProveedorFacade>();
            services.AddTransient<ProductoProveedorFacade>();

            #endregion

            #region DOMINIO: ESTRUCTURA SUCURSALES
            // Repositorios (DAL)
            services.AddTransient<ISucursalRepository, SucursalRepository>();
            services.AddTransient<ITipoSucursalRepository, TipoSucursalRepository>();

            // Validadores (FluentValidation)
            services.AddScoped<IValidator<SucursalDTO>, SucursalValidator>();
            services.AddScoped<IValidator<TipoSucursalDTO>, TipoSucursalValidator>();

            // Servicios de Negocio (BLL)
            services.AddScoped<ISucursalService, SucursalService>();
            services.AddScoped<ITipoSucursalService, TipoSucursalService>();

            // Fachadas (UI Gateway)
            services.AddTransient<SucursalFacade>();
            services.AddTransient<TipoSucursalFacade>();

            #endregion

            #region DOMINIO: GESTIÓN DE CONTROL DE INVENTARIO Y STOCK (SRP)
            // Repositorios (DAL) - Corrección de Typo: IStocklRepository -> IStockRepository
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddTransient<IMovimientosStockRepository, MovimientosStockRepository>();
            services.AddTransient<ILoteRepository, LoteRepository>();                       
            services.AddTransient<ITipoMovimientoRepository, TipoMovimientoRepository>();
            services.AddTransient<IOrdenTraspasoRepository, OrdenTraspasoRepository>();

            // Validadores (FluentValidation)
            services.AddScoped<IValidator<StockPorSucursalDTO>, StockValidator>();

            // Servicios de Negocio Internos Desacoplados (BLL)
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IMermaService, MermaService>();
            services.AddScoped<IKardexService, KardexService>();
            services.AddScoped<ITraspasoService, TraspasoService>();
            services.AddScoped<IFacade, StockFacade>();
            services.AddTransient<StockFacade>();

            #endregion

            #region DOMINIO: GESTIÓN DE COMPRAS Y REPOSICIÓN (MAESTRO-DETALLE)
            // Repositorios (DAL)
            services.AddTransient<ISolicitudPedidoRepository, SolicitudPedidoRepository>();
            services.AddTransient<IOrdenCompraRepository, OrdenCompraRepository>();
            services.AddTransient<ICompraSolicitudQueryRepository, CompraSolicitudQueryRepository>();


            // Validadores (FluentValidation)
            services.AddScoped<IValidator<SolicitudPedidoDTO>, SolicitudPedidoValidator>();
            services.AddScoped<IValidator<OrdenCompraDTO>, OrdenCompraDTOValidator>();
            services.AddScoped<IValidator<OrdenCompraDetalleDTO>, OrdenCompraDetalleDTOValidator>();


            // Servicios de Negocio (BLL)
            services.AddScoped<ISolicitudPedidoService, SolicitudPedidoService>();
            services.AddScoped<IOrdenCompraService, OrdenCompraService>();
            services.AddScoped<IOrdenCompraFacade, OrdenCompraFacade>();

            // Fachadas (UI Gateway)
            services.AddTransient<SolicitudPedidoFacade>();
            services.AddTransient<OrdenCompraFacade>();

            #endregion

            return services;
        }
       
    }
}
