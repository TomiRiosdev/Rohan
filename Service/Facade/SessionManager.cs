using Service.DomainModel.Composite;
using System;
using System.Linq;

namespace Service.Facade
{
    /// <summary>
    /// Maneja el estado global de la sesión del usuario (Patrón Singleton).
    /// </summary>
    public class SessionManager
    {
        private static SessionManager _instance;
        public static SessionManager Current => _instance ?? (_instance = new SessionManager());

        private SessionManager() { }

        public Usuario UsuarioLogueado { get; private set; }
        public Guid? IdSucursalActual { get; set; }
        public string NombreSucursalActual { get; set; }

        public void Login(Usuario usuario)
        { 
            UsuarioLogueado = usuario;
            IdSucursalActual = usuario.IdSucursal; 
        }
        public void Logout() 
        { 
            UsuarioLogueado = null;
            IdSucursalActual = null; 
            NombreSucursalActual = null;
        }

        /// <summary>
        /// Valida si el usuario actual posee un permiso específico (basado en el Composite).
        /// </summary>
        public bool TienePermiso(string dataKeyPermiso)
        {// 1. Control de seguridad defensivo
            if (UsuarioLogueado == null)
            {
                System.Diagnostics.Debug.WriteLine("DEBUG: Intento de validación sin usuario logueado.");
                return false;
            }

            // 2. Control de privilegios nulos (si no se cargaron, no tiene permisos)
            if (UsuarioLogueado.Privilegios == null)
            {
                System.Diagnostics.Debug.WriteLine("DEBUG: El usuario logueado no tiene privilegios cargados.");
                return false;
            }

            foreach (var privilegio in UsuarioLogueado.Privilegios)
            {
                if (ValidarPermisoRecursivo(privilegio, dataKeyPermiso)) return true;
            }
            return false;
        }

        /// <summary>
        /// Método recursivo para navegar el árbol de permisos (Patrón Composite).
        /// </summary>
        private bool ValidarPermisoRecursivo(Component componente, string nombreODataKey)
        {
            if (componente is Patente patente)
            {
                return patente.DataKey == nombreODataKey;
            }
            else if (componente is Familia familia)
            {
                if (familia.Nombre == nombreODataKey) return true;
                return familia.GetHijos().Any(hijo => ValidarPermisoRecursivo(hijo, nombreODataKey));
            }
            return false;
        }
    }
}
