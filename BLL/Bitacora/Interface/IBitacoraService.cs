using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Bitacora.Interface
{
    public interface IBitacoraService
    {
        void GetAllBitacora(Guid idEntidad, string operacion, string detalle); 
        void Registrar(Guid idEntidad, string operacion, string detalle);


    }
}
