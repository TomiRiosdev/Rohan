using System;
using System.Collections.Generic;

namespace Service.DateAccess.Interface
{
    public interface IGenericRepository<T>
    {
        void Add(T obj);
        void Update(T obj);
        void Remove(Guid id);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
    }
}
