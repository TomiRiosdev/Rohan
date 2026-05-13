using Service.DateAccess.Implementations;
using Service.DomainModel.Composite;
using Service.DomainModel.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Service.Logic
{
    public class PermisosService
    {
        private PermisosRepository _repo = new PermisosRepository();

        public List<Familia> GetAllFamilias()
        {
            return _repo.GetAllFamilias()
                 .Where(f => f.Nombre != "Administrador")
                 .ToList();
        }

        public List<Patente> GetAllPatentes()
        {
            return _repo.GetAllPatentes();
        }

        // 1. Método para asignar una Familia a un Usuario
        public void AsignarFamilia(Guid idUsuario, Guid idFamilia)
        {
            if (idUsuario == Guid.Empty || idFamilia == Guid.Empty)
                throw new Exception("Datos de usuario o familia no válidos.");

            // Validamos que no tenga ya ese permiso (Regla de negocio)
            if (_repo.UsuarioTieneFamilia(idUsuario, idFamilia))
                throw new Exception("El usuario ya posee este permiso asignado.");

            // Impactamos en DB
            _repo.AgregarUsuarioFamilia(idUsuario, idFamilia);

            // Auditoría
            new BitácoraService().RegistrarLog(
                $"Permiso ID {idFamilia} asignado al usuario ID {idUsuario}",
                Criticidad.Warning);
        }

        // 2. Método para quitar una Familia a un Usuario
        public void QuitarFamilia(Guid idUsuario, Guid idFamilia)
        {
            if (idUsuario == Guid.Empty || idFamilia == Guid.Empty)
                throw new Exception("Datos de usuario o familia no válidos.");

            // Impactamos en DB
            _repo.EliminarUsuarioFamilia(idUsuario, idFamilia);

            // Auditoría
            new BitácoraService().RegistrarLog(
                $"Permiso ID {idFamilia} revocado al usuario ID {idUsuario}",
                Criticidad.Warning);
        }

        // 3. Método para alimentar tu dgvUsuarioPermiso
        public DataTable ObtenerListaPermisosUsuarios()
        {
            return _repo.GetUsuarioFamilia();
        }
    }
}
