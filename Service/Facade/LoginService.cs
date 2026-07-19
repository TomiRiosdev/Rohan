using Service.DomainModel.Composite;
using Service.DomainModel.Logging;
using Service.Logic;
using System;


namespace Service.Facade
{
    /// <summary>
    /// Fachada de Login: Orquesta la autenticación, carga de sesión y seguridad.
    /// </summary>
    public static class LoginService
    {
        private static readonly BitácoraService _bitacora = new BitácoraService();
        private static readonly UsuarioService _usuarioService = new UsuarioService();

        /// <summary>
        /// Valida credenciales y devuelve el usuario si es correcto.
        /// </summary>
        public static Usuario Autenticar(string username, string passwordClara)
        {
       
            try
            {
                return _usuarioService.ValidarCredenciales(username, passwordClara);
            }
            catch (Exception ex)
            {
                _bitacora.RegistrarLog($"Intento fallido: {username}. Error: {ex.Message}",
                                        Criticidad.Warning);
                throw; 
            }
        }

        /// <summary>
        /// Finaliza el proceso de login, establece la sesión y registra el ingreso.
        /// </summary>
        public static void FinalizarLogin(Usuario usuario, Guid? idSucursal, string nombreSucursal)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));

            // Seteamos la sesión
            SessionManager.Current.Login(usuario);
            SessionManager.Current.IdSucursalActual = idSucursal;
            SessionManager.Current.NombreSucursalActual = nombreSucursal;

            _bitacora.RegistrarLog($"Inicio de sesión: {usuario.Nombre} en sucursal: {nombreSucursal}",
                                   Criticidad.Info, usuario.IdUsuario, usuario.Nombre, idSucursal);
        }

        /// <summary>
        /// Cierra sesión y registra la salida.
        /// </summary>
        public static void Logout()
        {
            var usuario = SessionManager.Current.UsuarioLogueado;
            if (usuario == null) return; 

            var idSucursal = SessionManager.Current.IdSucursalActual;
            var sucursal = SessionManager.Current.NombreSucursalActual;

            _bitacora.RegistrarLog($"Cierre de sesión: {usuario.Nombre} en Sucursal: {sucursal ?? "N/A"}",
                                  Criticidad.Info, usuario.IdUsuario, usuario.Nombre, idSucursal);

            SessionManager.Current.Logout();
        }
    }
}
