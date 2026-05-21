using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface.GestionStock
{
    public interface ILoteRepository
    {
        void Add(Lote entity);
        Lote GetById(Guid idLote);
        // Trae los lotes de una sucursal específica que todavía tengan stock físico disponible
        IEnumerable<Lote> GetLotesActivosPorSucursal(Guid idSucursal);
    }
}
