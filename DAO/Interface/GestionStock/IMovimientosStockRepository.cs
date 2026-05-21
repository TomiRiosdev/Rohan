using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface.GestionStock
{
    public interface IMovimientosStockRepository
    {
        // Registro atómico sin SaveChanges (Principio de la UOW)
        void Add(MovimientosStock entity);

        // Consulta para el historial del Kardex filtrado por sucursal y fechas
        IEnumerable<MovimientosStock> GetHistorial(Guid idSucursal, DateTime desde, DateTime hasta);

        // Recuperar movimientos específicos de un lote (Útil para trazabilidad de mermas)
        IEnumerable<MovimientosStock> GetByLote(Guid idLote);
    }
}
