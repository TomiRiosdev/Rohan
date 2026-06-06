using BLL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class ProductoDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int? CodigoSku { get; set; }
        public int? ContenidoPorVenta { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool Habilitado { get; set; }
        public int? IdTipoEnvase { get; set; }
        public int CantidadPorBulto { get; set; }

        //para la VISTA(DataGridView)
        public string CategoriaNombre { get; set; }
        public string UnidadMedidaNombre { get; set; }

        // Para la LÓGICA (Validar y Guardar)
        public Guid IdCategoria { get; set; }
        public Guid IdUnidadMedida { get; set; }

        public string TipoEnvaseNombre
        {
            get
            { //  Si el valor es cero o nulo por registros viejos, frena acá
                if (IdTipoEnvase <= 0)
                    return "Sin especificar";

                // Ahora sí llamamos a IsDefined con la total seguridad de que tiene un número válido
                return System.Enum.IsDefined(typeof(TipoEnvaseEnum), IdTipoEnvase)
                    ? ((TipoEnvaseEnum)IdTipoEnvase).ToString()
                    : "Desconocido";
            }
        }
    }
}
