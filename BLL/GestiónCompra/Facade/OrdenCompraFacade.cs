using BLL.DomainDtos;
using BLL.GestiónCompra.Exceptions;
using BLL.GestiónCompra.Interface;
using System;

namespace BLL.GestiónCompra.Facade
{
    public class OrdenCompraFacade : IOrdenCompraFacade
    {
        private readonly IOrdenCompraService _comprasService;

        public OrdenCompraFacade
        (
            IOrdenCompraService comprasService
        )
        {
            _comprasService = comprasService ?? throw new ArgumentNullException(nameof(comprasService));
        }

        #region  Operaciones de Escritura

        public void RegistrarNuevaOrdenCompra(OrdenCompraDTO dto)
        {
            try
            {
                _comprasService.GenerarOrdenCompra(dto);
            }
            catch (ComprasValidationException ex)
            {
                // Error de tipeo, faltan campos, importes en 0
                throw new Exception($"[Validación Comercial]: {ex.Message}");
            }
            catch (ReglaNegocioComprasException ex)
            {
                // El proveedor no existe o está deshabilitado
                throw new Exception($"[Regla de Negocio]: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Error de base de datos o caída de red envuelta
                throw new Exception("Error crítico en el subsistema de compras al intentar persistir la OC.", ex);
            }
        }

        public void CambiarEstado(Guid idOc, int nuevoEstadoId)
        {
            try
            {
                _comprasService.ModificarEstadoOc(idOc, nuevoEstadoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo cambiar el estado de la Orden de Compra: {ex.Message}", ex);
            }
        }

        public void DarDeBajaOrdenCompra(Guid idOc)
        {
            try
            {
                _comprasService.CancelarOrdenCompra(idOc);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar la cancelación del documento comercial: {ex.Message}", ex);
            }
        }

        public void EjecutarGeneracionAutomatica(Guid idSucursal, Guid idSolicitud)
        {
            try
            {
                _comprasService.GenerarOcAutomaticasDesdeSolicitudes(idSucursal, idSolicitud);
            }
            catch (ReglaNegocioComprasException ex)
            {
                throw new Exception(ex.Message); // "No existen solicitudes bajo mínimo..."
            }
            catch (Exception ex)
            {
                throw new Exception("Error interno al ejecutar el motor de unificación transaccional.", ex);
            }
        }

        #endregion

        #region  Operaciones de Lectura y Filtros

        public OrdenCompraDTO BuscarPorId(Guid idOc)
        {
            try
            {
                return _comprasService.ObtenerPorId(idOc);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar los datos del comprobante solicitado.", ex);
            }
        }

        public IEnumerable<OrdenCompraDTO> ConsultarHistorial(Guid idSucursal, Guid? idProveedor, int? idEstado)
        {
            try
            {
                return _comprasService.ConsultarHistorial(idSucursal, idProveedor, idEstado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar los filtros del historial en el servidor.", ex);
            }
        }

        public IEnumerable<ProductoDTO> ConsultarProductosDeProveedor(Guid idProveedor)
        {
            try
            {
                return _comprasService.ListarProductosDeProveedor(idProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar el catálogo indexado del proveedor.", ex);
            }
        }
        public IEnumerable<SolicitudPedidoDTO> ConsultarSolicitudesPendientes(Guid idSucursal)
        {
            try
            {
                var entidades = _comprasService.ObtenerSolicitudesPendientesPorSucursal(idSucursal);
                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar las solicitudes pendientes de la sucursal activa.", ex);
            }
        }

        #endregion

        #region  Alertas e Indicadores

        public bool ExistenSolicitudesPendientes(Guid idSucursal)
        {
            try
            {
                return _comprasService.VerificarSolicitudesPendientes(idSucursal);
            }
            catch
            {
                return false; // Silenciamos por seguridad en la UI si hay problemas de red
            }
        }

        #endregion

        #region  Documentación 

        public void GenerarDocumentoTexto(Guid idOc, string rutaDirectorio)
        {
            try
            {
                _comprasService.ExportarOcABlocDeNotas(idOc, rutaDirectorio);
            }
            catch (Exception ex)
            {
                throw new Exception($"Fallo en el subsistema de Entrada/Salida (I/O) al guardar el archivo: {ex.Message}", ex);
            }
        }

        #endregion
    }
}

