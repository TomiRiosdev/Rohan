using BLL.DomainDtos;
using BLL.GestiónCompra.Interface;
using BLL.GestiónCompra.Service;
using BLL.GestiónCompra.Validator;
using BLL.GestiónProducto.Facade;
using BLL.GestiónProducto.Interface;
using BLL.GestiónProducto.Service;
using BLL.GestiónProducto.Validator;
using BLL.GestiónStock.Interface;
using BLL.GestiónStock.Service;
using BLL.GestiónStock.Validator;
using BLL.GestiónSucursal.Facade;
using BLL.GestiónSucursal.Interface;
using BLL.GestiónSucursal.Service;
using BLL.GestiónSucursal.Validator;
using BLL.GestioónProveedor.Facade;
using BLL.GestioónProveedor.Interface;
using BLL.GestioónProveedor.Service;
using BLL.GestioónProveedor.Validator;
using DAO;
using DAO.Implementations.SQLServer;
using DAO.Implementations.SQLServer.GestionCompra;
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



namespace Bootstrapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
           
       

            // ====================== DAO / Repositories ======================
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<RohanContext>();
            // ====================== DAO Prodducto
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IProductoRepository, ProductoRepository>();
            services.AddTransient<IUnidadMedidaRepository, UnidadMedidaRepository>();
            // ====================== DAO Proveedor
            services.AddTransient<IProveedorRepository, ProveedorRepository>();
            services.AddTransient<IProductoProveedorRepository, ProductoProveedorRepository>();
            // ====================== DAO Sucursal
            services.AddTransient<ISucursalRepository, SucursalRepository>();
            services.AddTransient<ITipoSucursalRepository, TipoSucursalRepository>();
            // ====================== DAO Stock
            services.AddTransient<IStocklRepository, StockRepository>();
            services.AddTransient<IMovimientosStockRepository, MovimientosStockRepository>();
            // ====================== DAO Compra
            services.AddTransient<ISolicitudPedidoRepository, SolicitudPedidoRepository>();



            // ====================== Services (BLL) ======================
            // ====================== Services Producto
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IUnidadMedidaService, UnidadMedidaService>();
            // ====================== Services Proveedor
            services.AddScoped<IProveedorService, ProveedorService>();
            services.AddScoped<IProductoProveedorService, ProductoProveedorService>();
            // ====================== Services Sucursal
            services.AddScoped<ISucursalService, SucursalSerice>();
            services.AddScoped<ITipoSucursalService, TipoSucursalService>();
            // ====================== Services Stock
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IMermaService, MermaService>();
            services.AddScoped<IKardexService, KardexService>();

            // ====================== BLL Compra
            services.AddScoped<ISolicitudPedidoService, SolicitudPedidoService>();


            // ====================== BLL Validators ======================
            services.AddScoped<IValidator<ProductoDTO>, ProductoValidator>();
            services.AddScoped<IValidator<CategoriaDTO>, CategoriaValidator>();
            services.AddScoped<IValidator<UnidadMedidaDTO>, UnidadMedidaValidator>();      
            services.AddScoped<IValidator<ProveedorDTO>, ProveedorValidator>();
            services.AddScoped<IValidator<SucursalDTO>, SucursalValidator>();
            services.AddScoped<IValidator<TipoSucursalDTO>, TipoSucursalValidator>();
            services.AddScoped<IValidator<StockPorSucursalDTO>, StockValidator>();
            services.AddScoped<IValidator<ProductoProveedorDTO>, ProductoProveedorValidator>();
            services.AddScoped<IValidator<SolicitudPedidoDTO>, SolicitudPedidoValidator>();


            // ====================== Facades (Capa para UI) ======================
            services.AddTransient<ProductoFacade>();
            services.AddTransient<CategoriaFacade>();
            services.AddTransient<UnidadMedidaFacade>();
            services.AddTransient<ProveedorFacade>();
            services.AddTransient<SucursalFacade>();
            services.AddTransient<TipoSucursalFacade>();

            // ====================== Services Usuario ======================
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<PermisosRepository>();
            services.AddScoped<PermisosService>();
            services.AddTransient<UsuarioService>(); 

            return services;
        }
    }
}
