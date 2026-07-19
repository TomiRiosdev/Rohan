using Service.DateAccess.Implementations;
using Service.DomainModel.Logging;
using Service.Facade;
using System;
using System.Collections.Generic;

namespace Service.Logic
{
    public class BitácoraService
    {
        private readonly BitácoraRepository _bitacoraRepo;

        public BitácoraService()
        {
            _bitacoraRepo = new BitácoraRepository();
        }

        // Este es el método que vas a llamar desde todo el sistema
        public void RegistrarLog(string mensaje, Criticidad criticidad, Guid? idUsuario = null, string nombreUsuario = null, Guid? idSucursal = null)
        {
            try
            {
                Bitácora nuevoLog = new Bitácora
                {
                    Mensaje = mensaje,
                    Criticidad = criticidad,
                    IdUsuario = idUsuario,
                    NombreUsuario = nombreUsuario,
                    IdSucursal = idSucursal ?? SessionManager.Current.IdSucursalActual ?? Guid.Empty
                };

                _bitacoraRepo.Insertar(nuevoLog);
            }
            catch (Exception ex)
            {
                // Si la bitácora falla, no queremos que se caiga el sistema principal.
                // En un caso real, acá podríamos escribir en un archivo .txt de emergencia.
                System.Diagnostics.Debug.WriteLine($"Error al guardar en bitácora: {ex.Message}");
            }
        }

        public List<Bitácora> ListarBitacoraSegunRol()
        {
            // Obtenemos el perfil del usuario actual
            var usuario = SessionManager.Current.UsuarioLogueado;

            // Si es Administrador, pasamos null (trae todo)
            // Si no lo es, le pasamos su sucursal actual (filtra)
            Guid? filtroSucursal = SessionManager.Current.TienePermiso("Administrador")
                                  ? (Guid?)null
                                  : SessionManager.Current.IdSucursalActual;

            return _bitacoraRepo.Listar(filtroSucursal);
        }
    }
}
