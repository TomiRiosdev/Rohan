using BLL.DomainDtos;
using BLL.GestiónProducto.Facade;
using BLL.GestiónProducto.Interface;
using BLL.GestiónProducto.Service;
using BLL.GestiónProducto.Validator;
using BLL.GestiónSucursal.Facade;
using BLL.GestiónSucursal.Interface;
using BLL.GestiónSucursal.Service;
using BLL.GestiónSucursal.Validator;
using BLL.GestioónProveedor.Facade;
using BLL.GestioónProveedor.Interface;
using BLL.GestioónProveedor.Service;
using BLL.GestioónProveedor.Validator;
using DAO.Implementations.SQLServer;
using DAO.Interface;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Service.DateAccess.Implementations;
using Service.DateAccess.Interface;
using Service.Logic;
using Service.Logic.Validation;


namespace Bootstrapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // ====================== DAO / Repositories ======================
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            // ====================== DAO Prodducto
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IProductoRepository, ProductoRepository>();
            services.AddTransient<IUnidadMedidaRepository, UnidadMedidaRepository>();
            // ====================== DAO Proveedor
            services.AddTransient<IProveedorRepository, ProveedorRepository>();
            // ====================== DAO Sucursal
            services.AddTransient<ISucursalRepository, SucursalRepository>();
            services.AddTransient<ITipoSucursalRepository, TipoSucursalRepository>();


            // ====================== Services (BLL) ======================
            // ====================== Services Producto
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IUnidadMedidaService, UnidadMedidaService>();
            // ====================== Services Proveedor
            services.AddScoped<IProveedorService, ProveedorService>();
            // ====================== Services Sucursal
            services.AddScoped<ISucursalService, SucursalSerice>();
            services.AddScoped<ITipoSucursalService, TipoSucursalService>();


            // ====================== BLL Validators ======================
            services.AddScoped<IValidator<ProductoDTO>, ProductoValidator>();
            services.AddScoped<IValidator<CategoriaDTO>, CategoriaValidator>();
            services.AddScoped<IValidator<UnidadMedidaDTO>, UnidadMedidaValidator>();      
            services.AddScoped<IValidator<ProveedorDTO>, ProveedorValidator>();
            services.AddScoped<IValidator<SucursalDTO>, SucursalValidator>();
            services.AddScoped<IValidator<TipoSucursalDTO>, TipoSucursalValidator>();

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
