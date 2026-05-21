using Service.DateAccess.Implementations;
using Service.DomainModel.Composite;
using Service.DomainModel.Logging;
using Service.Facade;
using Service.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Logic
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepo;
        private readonly PermisosRepository _permisosRepo;

        public UsuarioService()
        {
            _usuarioRepo = new UsuarioRepository();
            _permisosRepo = new PermisosRepository();
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            UsuarioValidator.Validar(usuario.Username, usuario.Nombre, usuario.Email, usuario.Password, usuario.Telefono);

            Usuario usuarioExistente = _usuarioRepo.GetByUserName(usuario.Username);
            if (usuarioExistente != null)
            {
                throw new Exception($"El nombre de usuario '{usuario.Username}' ya está en uso. Por favor, elija otro.");
            }

            var todosLosUsuarios = _usuarioRepo.GetAll();
            if (todosLosUsuarios.Any(u => u.Email.Equals(usuario.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception($"El email '{usuario.Email}' ya se encuentra registrado en el sistema.");
            }

            string contraseñaHasheada = CryptographyService.HashMd5(usuario.Password);

            Usuario nuevoUsuario = new Usuario(
                Guid.NewGuid(),
                usuario.Username,
                usuario.Nombre,
                usuario.Email,
                contraseñaHasheada,
                usuario.Telefono,
                usuario.Fecha,
                usuario.Habilitado,
                usuario.IdSucursal  

            );

            _usuarioRepo.Add(nuevoUsuario);

            BitácoraService bitacora = new BitácoraService();
            bitacora.RegistrarLog($"Se dio de alta al nuevo usuario: {usuario.Nombre} ({usuario.Email})", Criticidad.Info);
        }

        public Usuario ValidarCredenciales(string username, string passwordClara)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(passwordClara))
                throw new Exception("Debe ingresar usuario y contraseña.");

            string passwordHasheada = CryptographyService.HashMd5(passwordClara);
            Usuario usuarioLogueado = _usuarioRepo.GetByCredentials(username, passwordHasheada);

            if (usuarioLogueado == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            if (!usuarioLogueado.Habilitado)
                throw new Exception("El usuario se encuentra deshabilitado. Contacte al administrador.");

            PermisosRepository permisosRepo = new PermisosRepository();
            permisosRepo.CargarPrivilegios(usuarioLogueado);

            return usuarioLogueado;
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            return _usuarioRepo.GetAll();
        }

        public List<Usuario> ListarHabilitados()
        {
            try
            {
                var habilitado = _usuarioRepo.GetHabilitado().Where(p => p.Habilitado == true);
                return habilitado.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("No se encontraron Usuarios", ex);
            }
        }

        public List<Usuario> ListarDeshabilitados()
        {
            try
            {
                var deshabilitao = _usuarioRepo.GetDeshabilitado().Where(p => p.Habilitado == false);
                return deshabilitao.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("No se encontraron Usuarios", ex);
            }
        }

        public Usuario GetById(Guid idUsuario)
        {
            if (idUsuario == Guid.Empty) throw new ArgumentException("El ID no puede estar vacío.");

            Usuario usuarioEncontrado = _usuarioRepo.GetById(idUsuario);

            if (usuarioEncontrado != null)
            {
                PermisosRepository permisosRepo = new PermisosRepository();
                permisosRepo.CargarPrivilegios(usuarioEncontrado);
            }

            return usuarioEncontrado;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            try
            {
                if(usuario.IdUsuario == Guid.Empty)
                    throw new Exception("El ID del usuario no puede estar vacío.");

                UsuarioValidator.Validar(usuario.Username, usuario.Nombre, usuario.Email, usuario.Password, usuario.Telefono);


                var entity = _usuarioRepo.GetById(usuario.IdUsuario);
                if (entity == null)
                    throw new Exception("El usuario no existe en la base de datos.");

                entity.Username = usuario.Username;
                entity.Nombre = usuario.Nombre;
                entity.Email = usuario.Email;
                entity.Telefono = usuario.Telefono;
                entity.IdSucursal = usuario.IdSucursal;
             
                _usuarioRepo.Update(entity);
            }
            catch (Exception ex)
            {

                throw new Exception("No se pudo actualizar el usuario. Verifique los datos ingresados.", ex);
            }
          
        }

        public void DeshabilitarUsuario(Guid idUsuario)
        {
            // 1. CONTROL SEGURO: No deshabilitarse a uno mismo
            Guid idUsuarioActual = SessionManager.Current.UsuarioLogueado.IdUsuario;
            if (idUsuario == idUsuarioActual)
            {
                throw new Exception("Operación inválida: No podés deshabilitar tu propio usuario mientras estás en sesión.");
            }

            // 2. Buscamos el usuario completo en la DB para analizarlo (incluyendo sus permisos)
            Usuario usuarioAValidar = _usuarioRepo.GetById(idUsuario);

            // Es mandatorio cargarle los privilegios desde el repositorio de permisos
            // para que la mochila del Composite no esté vacía.
            _permisosRepo.CargarPrivilegios(usuarioAValidar);

            // 3. CONTROL SEGURO: Si es administrador, verificar que no sea el último
            if (UsuarioEsAdministrador(usuarioAValidar))
            {
                int adminsActivos = _usuarioRepo.ContarAdministradoresActivos();
                if (adminsActivos <= 1)
                {
                    throw new Exception("Operación inválida: El sistema no puede quedar huérfano. No se puede deshabilitar al único Administrador activo.");
                }
            }

            // Si pasó ambos filtros, ejecutamos la baja lógica
            _usuarioRepo.Remove(idUsuario);
        }

        public void HabilitarUsuario(Guid idUsuario)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");

            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe en la base de datos.");

            usuarioExistente.Habilitado = true;

            _usuarioRepo.Update(usuarioExistente);

            BitácoraService bitacora = new BitácoraService();
            bitacora.RegistrarLog($"Se habilitó al usuario: {usuarioExistente.Nombre}", Criticidad.Info);
        }

        public IEnumerable<Usuario> BuscarUsuarios(string criterio)
        {
            var todosLosUsuarios = _usuarioRepo.GetAll();

            if (string.IsNullOrWhiteSpace(criterio))
                return todosLosUsuarios;

            criterio = criterio.ToLower();

            return todosLosUsuarios.Where(u =>
                u.Nombre.ToLower().Contains(criterio) ||
                u.Email.ToLower().Contains(criterio)
            ).ToList();
        }

        public void ModificarContraseña(Guid idUsuario, string nuevaContraseñaClara)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");
            if (string.IsNullOrWhiteSpace(nuevaContraseñaClara)) throw new Exception("Debe ingresar la nueva contraseña.");

            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe.");

            usuarioExistente.Password = CryptographyService.HashMd5(nuevaContraseñaClara);

            _usuarioRepo.Update(usuarioExistente);

            BitácoraService bitacora = new BitácoraService();
            bitacora.RegistrarLog($"Se modificó la contraseña del usuario: {usuarioExistente.Nombre}", Criticidad.Warning);
        }

        public void GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new Exception("El email es obligatorio.");
            Usuario usuarioEncontrado = _usuarioRepo.GetByEmail(email);
            if (usuarioEncontrado == null) throw new Exception("No se encontró ningún usuario con ese email.");
        }

        public void RecuperarContraseña(string email, string nuevaPasswordClara)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(nuevaPasswordClara))
                 throw new Exception("El email y la nueva contraseña son obligatorios.");

            Usuario usuario = _usuarioRepo.GetByEmail(email);
            if (usuario != null)
            {
                usuario.Password = CryptographyService.HashMd5(nuevaPasswordClara);
                _usuarioRepo.RecuperarContraseña(email, usuario.Password);

                BitácoraService bitacora = new BitácoraService();
                bitacora.RegistrarLog($"Recuperación de contraseña para el usuario: {usuario.Username}", Criticidad.Warning);
            }
            else
            {
                throw new Exception("No se encontró ningún usuario.");
            }
        }

        private bool UsuarioEsAdministrador(Usuario usuario)
        {
            if (usuario == null || usuario.Privilegios == null) return false;

            // Recorremos sus privilegios acumulados usando una función recursiva local
            foreach (var privilegio in usuario.Privilegios)
            {
                if (ValidarSiTieneAdminRecursivo(privilegio))
                    return true;
            }
            return false;
        }

        private bool ValidarSiTieneAdminRecursivo(Component componente)
        {
            // Si es una Familia, nos fijamos en su nombre
            if (componente is Familia familia)
            {
                if (familia.Nombre == "Administrador") return true;

                // Si no es el nombre, buscamos dentro de sus hijos por si está anidado
                foreach (var hijo in familia.GetHijos())
                {
                    if (ValidarSiTieneAdminRecursivo(hijo)) return true;
                }
            }
            // Si es una Patente, podrías verificar si tiene una patente maestra de administración
            else if (componente is Patente patente)
            {
                if (patente.DataKey == "fmsGestionSucursal") return true;
            }

            return false;
        }


    }
}
