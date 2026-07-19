using BLL.GestionAuditoria.Interface;
using BLL.DomainDtos;
using DAO.Interface;
using Models;

namespace BLL.GestionAuditoria.Service
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly IUnitOfWork _uow;
        public AuditoriaService
        (
            IUnitOfWork uow
        )
        {
            _uow = uow;
        }

        public void Registrar(Guid idEntidad, string operacion, string detalle)
        {
           
            var nuevaAuditoria = new Auditoria
            { 
                IdAuditoria = Guid.NewGuid(),
                Fecha = DateTime.Now,
                IdEntidadRelacionada = idEntidad,
                Operacion = operacion,
                Detalle = detalle,
          
            };

            _uow.AuditoriaRepository.AddAuditoria(nuevaAuditoria);
           
        }
    }
}

