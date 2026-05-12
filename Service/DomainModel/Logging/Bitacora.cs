using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DomainModel.Logging
{
    public class Bitácora
    {
        public int IdBitacora { get; set; }
        public DateTime Fecha { get; set; }
        public Guid? IdUsuario { get; set; }
        public string Mensaje { get; set; }
        public Criticidad Criticidad { get; set; }
        public string NombreUsuario { get; set; }
    }
}
