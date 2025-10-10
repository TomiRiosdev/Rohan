using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        // Métodos para exponer los repositorios específicos
        IProductoRepository ProductoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }

        // Método para guardar los cambios en la base de datos
        void SaveChanges();
    }
}
