using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface.GestionSucursal
{
    public interface ITipoSucursalRepository : IGenericRepository<TipoSucursal>
    {
        TipoSucursal GetByNombre(string name);
        bool ExistsByName(string nombre);
    }
}
