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
        // Operaciones Principales de la OC
        void GenerarOrdenCompra(OrdenCompraDTO dto);
        void ModificarEstadoOc(Guid idOc, int nuevoEstadoId);
        void CancelarOrdenCompra(Guid idOc); // Baja lógica comercial

        // Consultas para los combos, filtros y grillas de la UI
        OrdenCompraDTO ObtenerPorId(Guid idOc);
        IEnumerable<OrdenCompraDTO> ListarHistorialOc(Guid? idProveedor, int? idEstado);

        // Filtros cruzados rápidos para la carga manual
        IEnumerable<ProductoDTO> ListarProductosDeProveedor(Guid idProveedor);

        // AUTOMATIZACIÓN E INDICADORES
        bool VerificarSolicitudesPendientes(); // Alimentará tu lblPendiente
        void GenerarOcAutomaticasDesdeSolicitudes(); // El motor automático transaccional

        // DOCUMENTACIÓN FISICA
        void ExportarOcABlocDeNotas(Guid idOc, string rutaDirectorio);
    }
}
