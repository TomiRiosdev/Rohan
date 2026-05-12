using System;

namespace Service.DateAccess.Interface
{
    internal interface IAdapter<T> 
    {
        T Get(object[] values);
    }
}
