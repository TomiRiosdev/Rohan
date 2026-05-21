using DAO.Interface.GestionStock;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Implementations.SQLServer.GestionStock
{
    public class TipoMovimientoRepository : ITipoMovimientoRepository
    {
        private readonly RohanContext _dbContext;

        public TipoMovimientoRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<TipoMovimiento> GetAll()
        {
            return _dbContext.TipoMovimiento.AsNoTracking().ToList();
        }
    }
}
