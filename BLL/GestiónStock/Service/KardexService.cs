using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónStock.Interface;
using BLL.GestiónStock.Mapper;
using DAO.Interface;
using Models;

namespace BLL.GestiónStock.Service
{
    public class KardexService : IKardexService
    {
        private readonly IUnitOfWork _uow;

        public KardexService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        // REGISTRO DE MOVIMIENTO (ESCLAVO: No lleva SaveChanges interno)
        public void RegistrarMovimiento(Guid idSucursal, Guid idLote, TipoMovimientoEnum tipo, int cantidad, string observaciones)
        {
            // 1. Buscamos el Guid del TipoMovimiento haciendo matching riguroso con el Enum de la BLL
            var tipoMovimientoDb = _uow.TipoMovimientoRepository.GetAll()
                .FirstOrDefault(t => t.Descripcion.ToLower() == tipo.ToString().ToLower());

            if (tipoMovimientoDb == null)
                throw new Exception($"Error de configuración: El tipo de movimiento '{tipo}' no está registrado en la base de datos.");

            // 2. Mapeo directo usando los tipos del DomainModel que me pasaste
            var movimiento = new MovimientosStock
            {
                IdMovimiento = Guid.NewGuid(),
                IdSucursal = idSucursal,
                IdLote = idLote,
                IdTipoMovimiento = tipoMovimientoDb.IdTipoMovimiento,
                Cantidad = cantidad,
                FechaMovimiento = DateTime.Now,
                Observaciones = observaciones
            };

            // 3. Encolamos en el contexto transaccional único de la UOW
            _uow.MovimientosStockRepository.Add(movimiento);
        }

        // CONSULTA DE HISTORIAL (Para la pantalla de auditoría contable de stock)
        public IEnumerable<MovimientoStockDTO> ObtenerHistorial(Guid idSucursal, DateTime desde, DateTime hasta)
        {
            if (idSucursal == Guid.Empty) throw new ArgumentException("Sucursal inválida.");

            var entidades = _uow.MovimientosStockRepository.GetHistorial(idSucursal, desde, hasta);
            return entidades.ToDTOList(); // Nuestro método de extensión del mapper
        }
    }
}
