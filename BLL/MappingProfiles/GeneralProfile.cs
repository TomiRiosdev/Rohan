using AutoMapper;
using DAO.DomainModel;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AutoMapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            // Mapeo bidireccional (DAO Entity <-> DTO)
            CreateMap<Producto, ProductoDTO>()
                // Mapeo inverso: de DTO a Entidad para guardar
                .ReverseMap();

            CreateMap<CategoriaProducto, CategoriaDTO>().ReverseMap();
            CreateMap<Proveedore, ProveedorDTO>().ReverseMap();
            CreateMap<TipoProducto, TipoProductoDTO>().ReverseMap();
            CreateMap<UnidadMedidum, UnidadMedidaDTO>().ReverseMap();

        }
    }
}
