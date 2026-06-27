using BLL.DomainDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónCompra.Interface
{
    public interface IOrdenCompraService
    {
        #region Metodos de IOrdenCompraRepository
        void GenerarOrdenCompra(OrdenCompraDTO dto);
        void ModificarEstadoOc(Guid idOc, int nuevoEstadoId);
        void CancelarOrdenCompra(Guid idOc); // Baja lógica comercial
        OrdenCompraDTO ObtenerPorId(Guid idOc);
        IEnumerable<OrdenCompraDTO> ConsultarHistorial(Guid idSucursal, Guid? idProveedor, int? idEstado, DateTime fechaDesde, DateTime fechaHasta);
        void ExportarOcABlocDeNotas(Guid idOc, string rutaDirectorio);

        #endregion

        #region Metodos de ICompraSolicitudQueryRepository
        IEnumerable<SolicitudPedidoDTO> ObtenerSolicitudesPendientesPorSucursal(Guid idSucursal);
        bool VerificarSolicitudesPendientes(Guid idSucursal);
        void GenerarOcAutomaticasDesdeSolicitudes(Guid idSucursal, Guid idSolicitud);
        IEnumerable<ProductoDTO> ListarProductosDeProveedor(Guid idProveedor);

        #endregion
    }
}
