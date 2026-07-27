using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónStock.Interface
{
    public interface ITraspasoService
    {
        void GenerarTraspasoDesdeSolicitud(Guid idSucursalOrigen, Guid idSolicitud);
    }
}
    