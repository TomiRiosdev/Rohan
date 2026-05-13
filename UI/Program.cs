using Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using UI.GestiónProducto;
using UI.GestiónProveedor;
using UI.GestiónSucursal;
using UI.GestionUsuario;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            // 1. Configuración de tu capa de aplicación (Bootstrapper)
            services.AddApplicationLayer();        

            // 2. Registrar todos los formularios 
            ConfigureForms(services);

            // 3. Construir el ServiceProvider
            var serviceProvider = services.BuildServiceProvider();

            // 4. Obtener y ejecutar el Login como punto de entrada
            var loginForm = serviceProvider.GetRequiredService<Login>();

            Application.Run(loginForm);

        }
        private static void ConfigureForms(IServiceCollection services)
        {
            // FORMULARIOS 
            services.AddTransient<Login>();
            services.AddTransient<fmsPrincipal>();

            // Formularios de Gestión de Producto
            services.AddTransient<fmsGestiónProducto>();
            services.AddTransient<fmsCrearProducto>();
            services.AddTransient<fmsModificarProducto>();
            services.AddTransient<fmsCrudCatUMed>();

            // Formularios de Gestión de Proveedor
            services.AddTransient<fmsGestionProveedor>();
            services.AddTransient<fmsCrearProveedor>();
            services.AddTransient<fmsModificarProveedor>();

            // Formularios de Gestión de Sucursal
            services.AddTransient<fmsGestionSucursal>();
            services.AddTransient<fmsCrearSucursal>();
            services.AddTransient<fmsModificarSucursal>();
            services.AddTransient<fmsCrearTipoSucursal>();

            // Formularios de Gestión de Usuario
            services.AddTransient<fmsAgregarUsuario>();
            services.AddTransient<fmsGestionUsuario>();
            services.AddTransient<fmsModificarUsuario>();
            services.AddTransient<fmsAgregarPermisos>();
            services.AddTransient<fmsRecuperarContraseña>();
            services.AddTransient<fmsSeleccionarSucursal>();
        }
    }
}