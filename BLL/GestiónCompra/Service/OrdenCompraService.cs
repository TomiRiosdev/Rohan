using BLL.DomainDtos;
using BLL.GestiónCompra.Interface;
using DAO.Interface;
using System;


namespace BLL.GestiónCompra.Service
{
    public class OrdenCompraService : IOrdenCompraService
    {
        private readonly IUnitOfWork _uow;
        public OrdenCompraService
        (
            IUnitOfWork uow
        )
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public void CancelarOrdenCompra(Guid idOc)
        {
            throw new NotImplementedException();
        }

        public void ExportarOcABlocDeNotas(Guid idOc, string rutaDirectorio)
        {
            throw new NotImplementedException();
        }

        public void GenerarOcAutomaticasDesdeSolicitudes()
        {
            throw new NotImplementedException();
        }

        public void GenerarOrdenCompra(OrdenCompraDTO dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrdenCompraDTO> ListarHistorialOc(Guid? idProveedor, int? idEstado)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductoDTO> ListarProductosDeProveedor(Guid idProveedor)
        {
            throw new NotImplementedException();
        }

        public void ModificarEstadoOc(Guid idOc, int nuevoEstadoId)
        {
            throw new NotImplementedException();
        }

        public OrdenCompraDTO ObtenerPorId(Guid idOc)
        {
            throw new NotImplementedException();
        }

        public bool VerificarSolicitudesPendientes()
        {
            throw new NotImplementedException();
        }
    }
}
