using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public class ProductoDTO
    {
        public Guid IdProducto { get; set; }
        public string Nombre { get; set; } 
        public string Descripcion { get; set; } 
        public Guid IdCategoria { get; set; }
        public Guid IdTipoProducto { get; set; }
        public Guid IdUnidadMedida { get; set; }
        public Guid IdProveedor { get; set; }
        public bool Habilitado { get; set; }
    }
}
