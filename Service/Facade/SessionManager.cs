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
            // Si el usuario tiene una sucursal fija (empleado), la cargamos de una vez.
            // Si es Admin (null), esto quedará en null hasta que él elija en el siguiente form.
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

        private bool ValidarPermisoRecursivo(Component componente, string dataKey)
        {
            if (componente is Patente patente)
            {
                if (patente.DataKey == dataKey) return true;
            }
            else if (componente is Familia familia)
            {
                // Protegemos contra nulos por si la familia viene sin hijos
                if (familia.Nombre != null)
                {
                    foreach (var hijo in familia.GetHijos())
                    {
                        if (ValidarPermisoRecursivo(hijo, dataKey)) return true;
                    }
                }
            }
            return false;
        }
    }
}
