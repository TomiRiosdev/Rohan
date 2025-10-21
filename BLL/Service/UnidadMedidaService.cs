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
    public class UnidadMedidaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        public UnidadMedidaService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Add(UnidadMedidaDTO UnidadMedidaDto)
        {
            // Mapeo del DTO a la Entidad (Proveedor)
            var unidadMedida = _mapper.Map<UnidadMedida>(UnidadMedidaDto);
            // REGLA DE NEGOCIO: Asignación del ID y el estado inicial
            unidadMedida.IdUnidadMedida = Guid.NewGuid();
            unidadMedida.Habilitado = true;
            // Persistencia (se llama al UoW, el UoW es el que guarda)
            _unitOfWork.UnidadMedidaRepository.Add(unidadMedida);
            _unitOfWork.SaveChanges();

        }
        public void Update(UnidadMedidaDTO unidadMedidaDTO)
        {
            var unidadmedida = _mapper.Map<UnidadMedida>(unidadMedidaDTO);
            _unitOfWork.UnidadMedidaRepository.Update(unidadmedida);
            _unitOfWork.SaveChanges();
        }
        public void Remove(Guid id)
        {
            _unitOfWork.UnidadMedidaRepository.Remove(id);
            _unitOfWork.SaveChanges();
        }
        public UnidadMedidaDTO GetById(Guid id)
        {
            var unidad = _unitOfWork.UnidadMedidaRepository.GetById(id);
            return _mapper.Map<UnidadMedidaDTO>(unidad);
        }
        public IEnumerable<UnidadMedidaDTO> GetAll()
        {
            var unidadMedidantidades = _unitOfWork.UnidadMedidaRepository.GetAll();
            // Mapeo: Transforma la entidad de la DAO a DTO 
            var unidadmedidaDto = _mapper.Map<IEnumerable<UnidadMedidaDTO>>(unidadMedidantidades);
            // Devuelve la lista materializada (SOLUCIONA EL DataBinding)
            return unidadmedidaDto.ToList();
        }
        public UnidadMedidaDTO GetByNombre(string name)
        {
            var unidad = _unitOfWork.UnidadMedidaRepository.GetByNombre(name);
            return _mapper.Map<UnidadMedidaDTO>(unidad);
        }
        public IEnumerable<UnidadMedidaDTO> GetDeshabilitados()
        {
            var UnidadEntidades = _unitOfWork.UnidadMedidaRepository.GetAllDesHabilitados();
            // Mapeo: Transforma la entidad de la DAO a DTO 
            var UnidadDto = _mapper.Map<IEnumerable<UnidadMedidaDTO>>(UnidadEntidades);
            // Devuelve la lista materializada (SOLUCIONA EL DataBinding)
            return UnidadDto.ToList();
        }
    }
}
