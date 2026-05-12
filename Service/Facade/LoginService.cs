using Service.DomainModel.Composite;
using Service.Logic;


namespace Service.Facade
{
    public static class LoginService
    {
        public static void Login(string username, string passwordClara)
        {
            UsuarioService usuarioBll = new UsuarioService();

            // 1. Validamos que el usuario exista, no esté bloqueado y la clave sea correcta
            Usuario usuarioValido = usuarioBll.ValidarCredenciales(username, passwordClara);

            // 2. Si pasa la validación, lo guardamos en la sesión global de la aplicación
            SessionManager.Current.Login(usuarioValido);

            // 3. (Opcional) Podemos registrar el login aquí en lugar de hacerlo en el FormPrincipal_Load
            // BitácoraBll bitacora = new BitácoraBll();
            // bitacora.RegistrarLog($"El usuario {usuarioValido.Nombre} inició sesión en el sistema.", Criticidad.Info, usuarioValido.IdUsuario);
        }

        public static void Logout()
        {
            // Cerramos la sesión actual
            SessionManager.Current.Logout();
        }
    }
}
