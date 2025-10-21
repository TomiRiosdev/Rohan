using AutoMapper;
using BLL.MappingProfiles;
using DAO;
using DAO.Implementations.SQLServer;
using DAO.Interface;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
   public class ProveedorService
   {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;
            
        public ProveedorService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void AddProveedor(ProveedorDTO proveedorDto)
        {
            // Mapeo del DTO a la Entidad (Proveedor)
            var proveedor = _mapper.Map<Proveedore>(proveedorDto);
            // REGLA DE NEGOCIO: Asignación del ID y el estado inicial
            proveedor.IdProveedor = Guid.NewGuid();
            proveedor.Habilitado = true;
            // Persistencia (se llama al UoW, el UoW es el que guarda)
            _unitOfWork.ProveedorRepository.Add(proveedor);
            _unitOfWork.SaveChanges();
           
        }
        public void UpdateProveedor(ProveedorDTO proveedorDto)
        {
            var proveedor = _mapper.Map<Proveedore>(proveedorDto);
            _unitOfWork.ProveedorRepository.Update(proveedor);
            _unitOfWork.SaveChanges();
        }

        public void RemoveProveedor(Guid id)
        {
            _unitOfWork.ProveedorRepository.Remove(id);
            _unitOfWork.SaveChanges();
        }
        public ProveedorDTO GetProveedorById(Guid id)
        {
            var proveedor = _unitOfWork.ProveedorRepository.GetById(id);
            return _mapper.Map<ProveedorDTO>(proveedor);
        }
        public IEnumerable<ProveedorDTO> GetAllProveedores()
        {
            var proveedorEntidades = _unitOfWork.ProveedorRepository.GetAll();
            // Mapeo: Transforma la entidad de la DAO a DTO 
            var proveedoresDto = _mapper.Map<IEnumerable<ProveedorDTO>>(proveedorEntidades);
            // Devuelve la lista materializada (SOLUCIONA EL DataBinding)
            return proveedoresDto.ToList();
        }
        public ProveedorDTO GetProveedorByNombre(string name)
        {
            var proveedor = _unitOfWork.ProveedorRepository.GetByNombre(name);
            return _mapper.Map<ProveedorDTO>(proveedor);
        }
        
        public IEnumerable<ProveedorDTO> GetProveedoresHabilitados()
        {
            var proveedorEntidades = _unitOfWork.ProveedorRepository.GetAll();
            // Mapeo: Transforma la entidad de la DAO a DTO 
            var proveedoresDto = _mapper.Map<IEnumerable<ProveedorDTO>>(proveedorEntidades);
            // Devuelve la lista materializada (SOLUCIONA EL DataBinding)
            return proveedoresDto.ToList();
        }

    }
}
