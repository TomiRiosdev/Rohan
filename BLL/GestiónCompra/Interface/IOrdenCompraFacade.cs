using BLL.DomainDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónCompra.Interface
{
    public interface IOrdenCompraFacade
    {
        // Escrituras 
        void RegistrarNuevaOrdenCompra(OrdenCompraDTO Oc);
        void CambiarEstado(Guid idOc, int nuevoEstadoId);
        void DarDeBajaOrdenCompra(Guid idOc);
        void EjecutarGeneracionAutomatica(Guid idSucursal, Guid idSolicitud);
        void ActualizarOrdenCompra(OrdenCompraDTO Oc);

        // Lecturas y Filtros para Grillas / Combos
        OrdenCompraDTO BuscarPorId(Guid idOc);
        IEnumerable<OrdenCompraDTO> ConsultarHistorial(Guid idSucursal, Guid? idProveedor, int? idEstado, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<ProductoDTO> ConsultarProductosDeProveedor(Guid idProveedor);

        // Alertas e Indicadores Visuales
        bool ExistenSolicitudesPendientes(Guid idSucursal);

        // Emisión de Documento Físico
        void GenerarDocumentoTexto(Guid idOc, string rutaDirectorio);
        IEnumerable<SolicitudPedidoDTO> ConsultarSolicitudesPendientes(Guid idSucursal);
    }
}
