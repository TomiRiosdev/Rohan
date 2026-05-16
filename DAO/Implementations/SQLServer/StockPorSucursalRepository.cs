using DAO.Interface;
using Models;
using System.Data.Entity;


namespace DAO.Implementations.SQLServer
{
    public class StockPorSucursalRepository : IStockPorSucursalRepository
    {
        private readonly RohanContext _dbContext;

        public StockPorSucursalRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "El contexto no puede ser nulo.");
        }

        // 1. BUSCAR UN REGISTRO ESPECÍFICO (Para saber si ya hay stock de ese producto en la sucursal)
        public StockPorSucursal GetByIds(Guid idSucursal, Guid idProducto)
        {
            try
            {
                // Busca la primera fila que coincida con ambos IDs. Si no hay nada, devuelve null.
                return _dbContext.StockPorSucursal
                    .FirstOrDefault(s => s.IdSucursal == idSucursal && s.IdProducto == idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Falló la búsqueda de stock para Producto {idProducto} en Sucursal {idSucursal}.", ex);
            }
        }

        // 2. LEER TODO EL STOCK DE LA SUCURSAL (Para llenar la grilla de la UI)
        public IEnumerable<StockPorSucursal> GetConsolidadoBySucursal(Guid idSucursal)
        {
            try
            {
                // .Include(s => s.IdProductoNavigation) es OBLIGATORIO. 
                // Si no lo ponés, EF no hace el INNER JOIN con la tabla Producto,
                // y cuando quieras mostrar el "Nombre del Producto" en la grilla, te va a tirar NullReferenceException.
                return _dbContext.StockPorSucursal
                    .Include(s => s.IdProductoNavigation)
                    .Where(s => s.IdSucursal == idSucursal)
                    .AsNoTracking() // Optimiza la velocidad de lectura ya que estos datos solo se van a mostrar
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: No se pudo cargar el stock consolidado de la sucursal {idSucursal}.", ex);
            }
        }

        // 3. AGREGAR REGISTRO DE STOCK (Si el producto nunca tuvo stock en esa sucursal)
        public void Add(StockPorSucursal entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Solo lo ponemos en la cola de espera de la RAM. No impacta la DB todavía.
            _dbContext.StockPorSucursal.Add(entity);
        }

        // 4. AGREGAR LOTE (Para la trazabilidad física del ingreso)
        public void AddLote(Lote entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Solo lo ponemos en la cola de espera de la RAM. No impacta la DB todavía.
            _dbContext.Lote.Add(entity);
        }
    }
}
