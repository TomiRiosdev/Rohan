using BLL.Service;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class TipoProductoFacade
    {
        private readonly TipoProductoService _tipoProductoService;
        public TipoProductoFacade()
        {
            _tipoProductoService = new TipoProductoService();
        }

        public void Add(TipoProductoDTO tipoProducto)
        {
            _tipoProductoService.Add(tipoProducto);
        }
        public void Update(TipoProductoDTO tipoProducto)
        {
            _tipoProductoService.Update(tipoProducto);
        }
        public void Remove(Guid id)
        {
            _tipoProductoService.Remove(id);
        }
        public IEnumerable<TipoProductoDTO> GetAll()
        {
            return _tipoProductoService.GetAll();
        }
        public TipoProductoDTO GetById(Guid id)
        {
            return _tipoProductoService.GetById(id);
        }
        public IEnumerable<TipoProductoDTO> GetAllDesHabilitados()
        {
            return _tipoProductoService.GetDeshabilitados();
        }
        public void GetByNombre(string nombre)
        {
            _tipoProductoService.GetByNombre(nombre);
        }
    }
}
