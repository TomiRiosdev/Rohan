using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public class UnidadMedidaDTO
    {
        public Guid IdUnidadMedida { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Habilitado { get; set; }
    }
}
