using Service.DomainModel.Composite;
using System.Collections.Generic;

namespace Service.DateAccess.Interface
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {    
        Usuario GetByUserName(string username);
        Usuario GetByCredentials(string user, string password);
        Usuario GetByEmail(string email);
        void RecuperarContraseña(string email, string nuevaContraseña);
        List<Usuario> GetHabilitado();
        List<Usuario> GetDeshabilitado();
    }
}
