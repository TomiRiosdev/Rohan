using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractDTOs.DomainDtos
{
    public class ProductoDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int? CodigoSku { get; set; }
        public int? ContenidoPorVenta { get; set; }
        public string Descripcion { get; set; }

        //para la VISTA(DataGridView)
        public string CategoriaNombre { get; set; }
        public string UnidadMedidaNombre { get; set; }

        // Para la LÓGICA (Validar y Guardar)
        public Guid IdCategoria { get; set; }
        public Guid IdUnidadMedida { get; set; }
    }
}
