using AutoMapper;
using BLL.MappingProfiles;
using DAO;
using DAO.Implementations.SQLServer;
using DAO.Interface;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class TipoProductoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        public TipoProductoService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Add(TipoProductoDTO tipoProductoDTO)
        {
            // Mapeo del DTO a la Entidad (Proveedor)
            var tipoProducto = _mapper.Map<TipoProducto>(tipoProductoDTO);
            // REGLA DE NEGOCIO: Asignación del ID y el estado inicial
            tipoProducto.IdTipoProducto = Guid.NewGuid();
            tipoProducto.Habilitado = true;
            // Persistencia (se llama al UoW, el UoW es el que guarda)
            _unitOfWork.TipoProductoRepository.Add(tipoProducto);
            _unitOfWork.SaveChanges();

        }
        public void Update(TipoProductoDTO tipoProductoDTO)
        {
            var tipoProducto = _mapper.Map<TipoProducto>(tipoProductoDTO);
            _unitOfWork.TipoProductoRepository.Update(tipoProducto);
            _unitOfWork.SaveChanges();
        }

        public void Remove(Guid id)
        {
            _unitOfWork.TipoProductoRepository.Remove(id);
            _unitOfWork.SaveChanges();
        }
        public TipoProductoDTO GetById(Guid id)
        {
            var tipoProducto = _unitOfWork.ProveedorRepository.GetById(id);
            return _mapper.Map<TipoProductoDTO>(tipoProducto);
        }
        public IEnumerable<TipoProductoDTO> GetAll()
        {
            var tipoProductoEntidades = _unitOfWork.TipoProductoRepository.GetAll();
            // Mapeo: Transforma la entidad de la DAO a DTO 
            var TipoProductoDto = _mapper.Map<IEnumerable<TipoProductoDTO>>(tipoProductoEntidades);
            // Devuelve la lista materializada (SOLUCIONA EL DataBinding)
            return TipoProductoDto.ToList();
        }
        public TipoProductoDTO GetByNombre(string name)
        {
            var TipoProductoDto = _unitOfWork.TipoProductoRepository.GetByNombre(name);
            return _mapper.Map<TipoProductoDTO>(TipoProductoDto);
        }
        public IEnumerable<TipoProductoDTO> GetDeshabilitados()
        {
            var tipoProductoEntidades = _unitOfWork.TipoProductoRepository.GetAll();
            // Mapeo: Transforma la entidad de la DAO a DTO 
            var TipoProductoDto = _mapper.Map<IEnumerable<TipoProductoDTO>>(tipoProductoEntidades);
            // Devuelve la lista materializada (SOLUCIONA EL DataBinding)
            return TipoProductoDto.ToList();
        }

    }
}
