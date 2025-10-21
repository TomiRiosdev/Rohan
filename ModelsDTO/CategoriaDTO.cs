using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public class CategoriaDTO
    {
        public Guid IdCategoriaProdcuto { get; set; }
        public string Nombre { get; set; }
        public bool Habilitado { get; set; }
    }
}
