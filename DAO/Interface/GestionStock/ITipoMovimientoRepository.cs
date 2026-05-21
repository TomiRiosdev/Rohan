using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface.GestionStock
{
    public interface ITipoMovimientoRepository
    {
        IEnumerable<TipoMovimiento> GetAll();
    }
}
