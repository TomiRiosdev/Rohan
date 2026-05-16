using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Service.DomainModel.Composite;
using Service.DataAccess.Implementations.Adapters;
using Service.DataAccess.Tools;
using Service.DateAccess.Interface;

namespace Service.DateAccess.Implementations
{
   public class UsuarioRepository : IUsuarioRepository
   {
        // Este es el método que usará tu LoginService
        public Usuario GetByUserName(string username)
        {
            string query = "SELECT * FROM Usuario WHERE Nombre = @Username";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, new SqlParameter("@Username", username)))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }

        public Usuario GetById(Guid id)
        {
            string query = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, new SqlParameter("@IdUsuario", id)))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }

        public void Add(Usuario obj)
        {

            obj.IdUsuario = Guid.NewGuid();
            string commandText = "INSERT INTO Usuario (IdUsuario,Username, Nombre, Password, Email, Telefono, Habilitado,Fecha , IdSucursal) VALUES (@IdUsuario, @Username, @Nombre, @Password, @Email, @Telefono, @Habilitado, @Fecha, @IdSucursal)";
            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario),
                new SqlParameter("@Username", obj.Username),
                new SqlParameter("@Nombre", obj.Nombre),
                new SqlParameter("@Password", obj.Password),
                new SqlParameter("@Email", obj.Email),
                new SqlParameter("@Telefono", obj.Telefono),
                new SqlParameter("@Habilitado", obj.Habilitado),
                new SqlParameter("@Fecha", obj.Fecha),
                new SqlParameter("@IdSucursal", (object)obj.IdSucursal ?? DBNull.Value)
            );
        }

        public void Update(Usuario obj)
        {
            string query = "UPDATE Usuario SET Username = @Username, Nombre = @Nombre, Password = @Password, Email = @Email, Telefono = @Telefono, Habilitado = @Habilitado, Fecha = @Fecha, IdSucursal = @IdSucursal WHERE IdUsuario = @IdUsuario";
            int filasAfectadas = SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario),
                new SqlParameter("@Username", obj.Username),
                new SqlParameter("@Nombre", obj.Nombre),
                new SqlParameter("@Password", obj.Password),
                new SqlParameter("@Email", obj.Email),
                new SqlParameter("@Telefono", obj.Telefono),
                new SqlParameter("@Habilitado", obj.Habilitado),
                new SqlParameter("@Fecha", obj.Fecha),
                new SqlParameter("@IdSucursal", (object)obj.IdSucursal ?? DBNull.Value)

             );
            if (filasAfectadas == 0)
            {
                throw new Exception("No se encontró el usuario en la base de datos para actualizar.");
            }
        }

        public void Remove(Guid id)
        {
            string query = "UPDATE Usuario SET Habilitado = 0 WHERE IdUsuario = @IdUsuario";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text, new SqlParameter("@IdUsuario", id));
        }

        public IEnumerable<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string query = "SELECT * FROM Usuario";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    usuarios.Add(UsuarioAdapter.Current.Get(data));
                }
            }
            return usuarios;
        }

        public Usuario GetByCredentials(string user, string password)
        {
            string commandText = "SELECT * FROM Usuario WHERE Username = @Username AND Password = @Password";

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(commandText, CommandType.Text,
                new SqlParameter("@Username", user),
                new SqlParameter("@Password", password)))
            {
                if (dataReader.Read())
                {
                    object[] data = new object[dataReader.FieldCount];
                    dataReader.GetValues(data);
                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }

        public Usuario GetByEmail(string email)
        {
            string query = "SELECT * FROM Usuario WHERE Email = @Email";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, new SqlParameter("@Email", email)))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }

        public void RecuperarContraseña(string email, string nuevaContraseña)
        {
            string query = "UPDATE Usuario SET Password = @Password WHERE Email = @Email";
            int filasAfectadas = SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@Password", nuevaContraseña),
                new SqlParameter("@Email", email)
            );
            if (filasAfectadas == 0)
            {
                throw new Exception("No se encontró ningún usuario con ese email para recuperar la contraseña.");
            }
        }

        public List<Usuario> GetHabilitado()
        {
            if (GetAll() is List<Usuario> usuarios)
            {
                return usuarios.FindAll(u => u.Habilitado);
            }
            return new List<Usuario>();
        }

        public List<Usuario> GetDeshabilitado()
        {
            if (GetAll() is List<Usuario> usuarios)
            {
                return usuarios.FindAll(u => !u.Habilitado);
            }
            return new List<Usuario>();
        }

        public int ContarAdministradoresActivos()
        {
            // Contamos cuántos usuarios habilitados tienen asignada la familia 'Administrador'
            string query = @"SELECT COUNT(DISTINCT u.IdUsuario) 
                     FROM [dbo].[Usuario] u
                     INNER JOIN [dbo].[UsuarioFamilia] uf ON u.IdUsuario = uf.IdUsuario
                     INNER JOIN [dbo].[Familia] f ON uf.IdFamilia = f.IdFamilia
                     WHERE f.Nombre = 'Administrador' AND u.Habilitado = 1";

            // Usamos el ExecuteScalar de tu SqlHelper que devuelve un Object y lo casteamos
            return (int)SqlHelper.ExecuteScalar(query, CommandType.Text);
        }
   }
}
