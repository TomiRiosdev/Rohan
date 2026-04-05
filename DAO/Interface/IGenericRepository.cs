using System;

namespace DAO.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Guid Add(T entity);
        void Update(T entity);
        void Remove(Guid id);
        T GetById(Guid id);
        IEnumerable<T> GetAll();

    }
}
