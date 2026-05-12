using Service.DateAccess.Implementations;
using Service.DomainModel.Logging;
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
        public void RegistrarLog(string mensaje, Criticidad criticidad, Guid? idUsuario = null)
        {
            try
            {
                Bitácora nuevoLog = new Bitácora
                {
                    Mensaje = mensaje,
                    Criticidad = criticidad,
                    IdUsuario = idUsuario
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

        public List<Bitácora> ListarBitacora()
        {
            try
            {
                return _bitacoraRepo.ListarTodos();
            }
            catch (Exception ex)
            {
                // Si hay error, lo ideal es avisar a la UI
                throw new Exception("Error al consultar la bitácora: " + ex.Message);
            }
        }
    }
}
