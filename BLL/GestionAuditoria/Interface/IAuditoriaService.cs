using BLL.DomainDtos;


namespace BLL.GestionAuditoria.Interface
{
    public interface IAuditoriaService
    {
        void Registrar(Guid idEntidad, string operacion, string detalle);

    }
}
