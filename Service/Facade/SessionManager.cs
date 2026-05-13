using Service.DomainModel.Composite;
using System;

namespace Service.Facade
{
    public class SessionManager
    {
        private static SessionManager _instance;

        // Singleton
        public static SessionManager Current
        {
            get
            {
                if (_instance == null)
                    _instance = new SessionManager();
                return _instance;
            }
        }

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

        // Método clave para que los Forms validen permisos rápidamente
        public bool TienePermiso(string dataKeyPermiso)
        {
            if (UsuarioLogueado == null) return false;

            // Ahora lee correctamente la mochila del usuario
            foreach (var privilegio in UsuarioLogueado.Privilegios)
            {
                if (ValidarPermisoRecursivo(privilegio, dataKeyPermiso))
                    return true;
            }
            return false;
        }

        private bool ValidarPermisoRecursivo(Component componente, string nombreODataKey)
        {
            // Si es una Patente, comparamos el DataKey
            if (componente is Patente patente)
            {
                if (patente.DataKey == nombreODataKey) return true;
            }
            // Si es una Familia, comparamos el Nombre Y buscamos en los hijos
            else if (componente is Familia familia)
            {
                // CAMBIO CLAVE: También comparamos el nombre de la familia
                if (familia.Nombre == nombreODataKey) return true;

                foreach (var hijo in familia.GetHijos())
                {
                    if (ValidarPermisoRecursivo(hijo, nombreODataKey)) return true;
                }
            }
            return false;
        }
    }
}
