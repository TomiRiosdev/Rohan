using Service.DataAccess.Implementations.Adapters;
using Service.DateAccess.Implementations;
using Service.DomainModel.Composite;
using Service.DomainModel.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Service.Logic
{
    /// <summary>
    /// Servicio de lógica de negocio para la gestión de permisos, familias y patentes.
    /// </summary>
    public class PermisosService
    {
        private readonly PermisosRepository _repo;
        private readonly BitácoraService _bitacora;

        public PermisosService()
        {
            _repo = new PermisosRepository();
            _bitacora = new BitácoraService();
        }

        #region Helpers
        /// <summary>
        /// Método centralizado para manejar excepciones y registrar errores en bitácora.
        /// </summary>
        private void LogAndThrow(string mensajeUsuario, Exception ex, Criticidad criticidad = Criticidad.Error)
        {
            _bitacora.RegistrarLog($"{mensajeUsuario}. Detalle: {ex.Message}", criticidad);
            throw new Exception(mensajeUsuario, ex);
        }
        #endregion

        #region Consultas
        /// <summary>
        /// Obtiene todas las familias, excluyendo al Administrador.
        /// </summary>
        public List<Familia> GetAllFamilias()
        {
            try
            {
                return _repo.GetAllFamilias()
                            .Where(f => f.Nombre != "Administrador")
                            .ToList();
            }
            catch (Exception ex)
            {
                LogAndThrow("Error al obtener las familias.", ex);
                return new List<Familia>(); // Nunca llega aquí pero es necesario por el compilador
            }
        }

        /// <summary>
        /// Obtiene todas las patentes registradas.
        /// </summary>
        public List<Patente> GetAllPatentes()
        {
            try
            {
                return _repo.GetAllPatentes();
            }
            catch (Exception ex)
            {
                LogAndThrow("Error al obtener las patentes.", ex);
                return new List<Patente>();
            }
        }

        /// <summary>
        /// Obtiene la lista relacional de permisos de usuarios para la interfaz.
        /// </summary>
        public DataTable ObtenerListaPermisosUsuarios()
        {
            try
            {
                return _repo.GetUsuarioFamilia();
            }
            catch (Exception ex)
            {
                LogAndThrow("Error al consultar la lista de permisos.", ex);
                return null;
            }
        }
        #endregion

        #region Gestión de Permisos
        /// <summary>
        /// Asigna una familia a un usuario.
        /// </summary>
        public void AsignarFamilia(Guid idUsuario, Guid idFamilia)
        {
            try
            {
                if (idUsuario == Guid.Empty || idFamilia == Guid.Empty)
                    throw new Exception("Datos de usuario o familia no válidos.");

                if (_repo.UsuarioTieneFamilia(idUsuario, idFamilia))
                    throw new Exception("El usuario ya posee este permiso asignado.");

                _repo.AgregarUsuarioFamilia(idUsuario, idFamilia);

                _bitacora.RegistrarLog($"Permiso ID {idFamilia} asignado al usuario ID {idUsuario}", Criticidad.Warning);
            }
            catch (Exception ex)
            {
                LogAndThrow("Error al asignar la familia al usuario.", ex);
            }
        }

        /// <summary>
        /// Revoca una familia a un usuario.
        /// </summary>
        public void QuitarFamilia(Guid idUsuario, Guid idFamilia)
        {
            try
            {
                if (idUsuario == Guid.Empty || idFamilia == Guid.Empty)
                    throw new Exception("Datos de usuario o familia no válidos.");

                _repo.EliminarUsuarioFamilia(idUsuario, idFamilia);

                _bitacora.RegistrarLog($"Permiso ID {idFamilia} revocado al usuario ID {idUsuario}", Criticidad.Warning);
            }
            catch (Exception ex)
            {
                LogAndThrow("Error al revocar la familia al usuario.", ex);
            }
        }
        #endregion


        public void CargarPrivilegios(Usuario usuario)
        {
            // GARANTÍA: Siempre inicializamos la lista, incluso si no hay permisos en DB.
            // Esto evita el null reference al llegar al SessionManager.
            usuario.Privilegios = new List<Component>();

            try
            {
                // 1. Cargar Patentes
                var patentesData = _repo.GetPatentesByUsuario(usuario.IdUsuario);
                foreach (var data in patentesData)
                {
                    usuario.Privilegios.Add(PatenteAdapter.Current.Get(data));
                }

                // 2. Cargar Familias y sus patentes
                var familiasData = _repo.GetFamiliasByUsuario(usuario.IdUsuario);
                foreach (var dataFam in familiasData)
                {
                    var familia = FamiliaAdapter.Current.Get(dataFam);
                    var patentesFamiliaData = _repo.GetPatentesByFamilia(familia.Id);

                    foreach (var dataPat in patentesFamiliaData)
                    {
                        familia.Add(PatenteAdapter.Current.Get(dataPat));
                    }
                    usuario.Privilegios.Add(familia);
                }

                System.Diagnostics.Debug.WriteLine($"DEBUG: Se cargaron {usuario.Privilegios.Count} privilegios para {usuario.Nombre}");
            }
            catch (Exception ex)
            {
                // Si falla la DB, lanzamos error para que no sigas con un usuario "vacío"
                throw new Exception("Error al hidratar privilegios del usuario", ex);
            }
        }
    }
}
