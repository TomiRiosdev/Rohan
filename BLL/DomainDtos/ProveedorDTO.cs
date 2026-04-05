using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class ProveedorDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Cuit { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string RazonSocial { get; set; }

    }
}
