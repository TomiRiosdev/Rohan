using Service.DateAccess.Implementations;
using Service.DateAccess.Interface;
using Service.DomainModel.Composite;
using System;
using System.Collections.Generic;

namespace Service.DataAccess.Implementations.Adapters
{
    internal class UsuarioAdapter : IAdapter<Usuario>
    {
        #region Singleton
        private readonly static UsuarioAdapter _instance = new UsuarioAdapter();

        public static UsuarioAdapter Current
        {
            get { return _instance; }
        }

        private UsuarioAdapter() { }
        #endregion

        public Usuario Get(object[] values)
        {
            Guid id = Guid.Parse(values[0].ToString());

            string username = values[1] == DBNull.Value ? string.Empty : values[1].ToString();

            string nombre = values[2] == DBNull.Value ? string.Empty : values[2].ToString();

            string password = values[3] == DBNull.Value ? string.Empty : values[3].ToString();

            string email = values[4] == DBNull.Value ? string.Empty : values[4].ToString();

            string telefono = values[5] == DBNull.Value ? string.Empty : values[5].ToString();

            bool habilitado = values[6] != DBNull.Value && Convert.ToBoolean(values[6]);

            DateTime dateTime = values[7] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(values[7]);

            Guid? idSucursal = values[8] == DBNull.Value ? (Guid?)null : Guid.Parse(values[8].ToString());

            Usuario usuario = new Usuario(id, username, nombre, email, password, telefono, dateTime, habilitado, idSucursal);

            if (usuario.Privilegios == null) usuario.Privilegios = new List<Component>();

            usuario.Privilegios.AddRange(new UsuarioFamiliaRepository().GetByObject(usuario));
            usuario.Privilegios.AddRange(new UsuarioPatenteRepository().GetByObject(usuario));

            return usuario;
        }
        
    }
}

