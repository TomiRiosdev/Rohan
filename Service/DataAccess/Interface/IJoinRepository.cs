using Service.DomainModel.Composite;
using System.Collections.Generic;


namespace Service.DateAccess.Interface
{
    public interface IJoinRepository<T>
    {
        IList<Component> GetByObject(T parent);
    }
}
