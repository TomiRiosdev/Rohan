using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class SucursalDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public int? CodigoPostal { get; set; }
        public int? Telefono { get; set; }
        public Guid IdTipoSucursal { get; set; }
        public string TipoSucursalNombre { get; set; } // Para mostrar en la grilla
        public string Localidad { get; set; }
        public bool Habilitado { get; set; }
    }
}
