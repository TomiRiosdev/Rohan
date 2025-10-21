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
    public class CategoriaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;
        public CategoriaService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void AddCategoriaProducto(CategoriaDTO categoriaProductoDto)
        {
            var categoriaProducto = _mapper.Map<CategoriaProducto>(categoriaProductoDto);

            categoriaProducto.IdCategoriaProdcuto = Guid.NewGuid(); 
            categoriaProducto.Habilitado = true; 

            _unitOfWork.CategoriaRepository.Add(categoriaProducto);
            _unitOfWork.SaveChanges();
        }
        public void UpdateCategoriaProducto(CategoriaDTO categoriaProductoDto)
        {
            var categoriaProducto = _mapper.Map<CategoriaProducto>(categoriaProductoDto);
            _unitOfWork.CategoriaRepository.Update(categoriaProducto);
            _unitOfWork.SaveChanges();
        }
        public void DeleteCategoriaProducto(Guid id)
        {
            _unitOfWork.CategoriaRepository.Remove(id);
            _unitOfWork.SaveChanges();
        }
        public CategoriaDTO GetCategoriaProductoById(Guid id)
        {
            var categoriaProducto = _unitOfWork.CategoriaRepository.GetById(id);
            return _mapper.Map<CategoriaDTO>(categoriaProducto);
        }
        public IEnumerable<CategoriaDTO> GetAllCategoriaProductos()
        {
            // var categoriaProductos = _unitOfWork.CategoriaRepository.GetAll();
            // **Convertir la Entidad de la DAO a DTO para devolver a la UI**
            // return _mapper.Map<IEnumerable<CategoriaDTO>>(categoriaProductos);
           
            var categoriaEntidades = _unitOfWork.CategoriaRepository.GetAll();

            // Mapeo: Transforma la entidad de la DAO a DTO (solo toma Id y Nombre)
            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDTO>>(categoriaEntidades);

            // Devuelve la lista materializada (SOLUCIONA EL DataBinding)
            return categoriasDto.ToList();
        }

    }
}
