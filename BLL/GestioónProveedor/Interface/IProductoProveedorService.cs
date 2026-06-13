using BLL.DomainDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónProveedor.Interface
{
    public interface IProductoProveedorService
    {
        // CRUD y Altas/Bajas Atómicas
        void VincularProductoAProveedor(ProductoProveedorDTO dto);
        void DesvincularProductoDeProveedor(Guid idProducto, Guid idProveedor);

        // Consultas de listado cruzado para alimentar las Grillas Gemelas de la UI
        IEnumerable<ProductoProveedorDTO> ListarProductosPorProveedor(Guid idProveedor);
        IEnumerable<ProductoProveedorDTO> ListarProveedoresPorProducto(Guid idProducto);
    }
}
