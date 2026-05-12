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

        public UsuarioService()
        {
            _usuarioRepo = new UsuarioRepository();
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
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");

            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe en la base de datos.");

            // Le bajamos el pulgar
            usuarioExistente.Habilitado = false;

            _usuarioRepo.Update(usuarioExistente);

            BitácoraService bitacora = new BitácoraService();
            bitacora.RegistrarLog($"Se deshabilitó al usuario: {usuarioExistente.Nombre}", Criticidad.Warning);
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
    }
}
