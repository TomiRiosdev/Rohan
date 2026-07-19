using Service.DataAccess.Implementations.Adapters;
using Service.DataAccess.Tools;
using Service.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Service.DateAccess.Implementations
{
    public class PermisosRepository
    {
        // Método para cargar los privilegios (Familias y Patentes) de un usuario en su mochila
        public void CargarPrivilegios(Usuario usuario)
        {
          
            usuario.Privilegios = new List<Component>();

            // 1. CARGAR PATENTES ASIGNADAS DIRECTAMENTE AL USUARIO
            string queryPatentes = "SELECT p.IdPatente, p.DataKey, p.TipoAcceso FROM [dbo].[Patente] p INNER JOIN [dbo].[UsuarioPatente] up ON p.IdPatente = up.IdPatente WHERE up.IdUsuario = @IdUsuario";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(queryPatentes, CommandType.Text, new SqlParameter("@IdUsuario", usuario.IdUsuario)))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    usuario.Privilegios.Add(PatenteAdapter.Current.Get(data));
                }
            }

            // 2. CARGAR LAS FAMILIAS (ROLES) DEL USUARIO
            string queryFamilias = "SELECT f.IdFamilia, f.Nombre FROM [dbo].[Familia] f INNER JOIN [dbo].[UsuarioFamilia] uf ON f.IdFamilia = uf.IdFamilia WHERE uf.IdUsuario = @IdUsuario";

            using (SqlDataReader readerFamilias = SqlHelper.ExecuteReader(queryFamilias, CommandType.Text, new SqlParameter("@IdUsuario", usuario.IdUsuario)))
            {
                while (readerFamilias.Read())
                {
                    object[] dataFam = new object[readerFamilias.FieldCount];
                    readerFamilias.GetValues(dataFam);
                    var familia = FamiliaAdapter.Current.Get(dataFam);

                    // 3. POR CADA FAMILIA, BUSCAMOS QUÉ PATENTES TIENE ADENTRO
                    string queryPatFamilia = "SELECT p.IdPatente, p.DataKey, p.TipoAcceso FROM [dbo].[Patente] p INNER JOIN [dbo].[FamiliaPatente] fp ON p.IdPatente = fp.IdPatente WHERE fp.IdFamilia = @IdFamilia";

                    using (SqlDataReader readerPatFam = SqlHelper.ExecuteReader(queryPatFamilia, CommandType.Text, new SqlParameter("@IdFamilia", familia.Id)))
                    {
                        while (readerPatFam.Read())
                        {
                            object[] dataPat = new object[readerPatFam.FieldCount];
                            readerPatFam.GetValues(dataPat);

                            // 1. Instanciamos la patente
                            var nuevaPatente = PatenteAdapter.Current.Get(dataPat);

                            // 2. Usamos el método propio de la clase Familia en lugar de tocar la lista Hijos directamente
                            familia.Add(nuevaPatente);
                        }
                    }

                    // Finalmente, guardamos la familia (ya llena) en la mochila del usuario
                    usuario.Privilegios.Add(familia);
                }
            }
        }

        // Métodos para obtener todas las Familias y Patentes disponibles en el sistema (para mostrar en el formulario de edición)
        public List<Familia> GetAllFamilias()
        {
            var lista = new List<Familia>();
            string query = "SELECT IdFamilia, Nombre FROM [dbo].[Familia]";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    // Usamos tu adaptador para convertir la fila de SQL a objeto Familia
                    var familia = (Familia)FamiliaAdapter.Current.Get(data);
                    lista.Add(familia);
                }
            }
            return lista;
        }

        // Método para obtener todas las Patentes disponibles en el sistema (para mostrar en el formulario de edición)
        public List<Patente> GetAllPatentes()
        {
            var lista = new List<Patente>();
            // Incluimos TipoAcceso porque tu adaptador de Patente seguramente lo espera
            string query = "SELECT IdPatente, DataKey, TipoAcceso FROM [dbo].[Patente]";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    // Usamos tu adaptador para convertir la fila de SQL a objeto Patente
                    var patente = (Patente)PatenteAdapter.Current.Get(data);
                    lista.Add(patente);
                }
            }
            return lista;
        }

        public bool UsuarioTieneFamilia(Guid idUsuario, Guid idFamilia)
        {
            string query = "SELECT COUNT(*) FROM [dbo].[UsuarioFamilia] WHERE IdUsuario = @IdUsuario AND IdFamilia = @IdFamilia";
            int count = (int)SqlHelper.ExecuteScalar(query, CommandType.Text,
                new SqlParameter("@IdUsuario", idUsuario),
                new SqlParameter("@IdFamilia", idFamilia));
            return count > 0;
        }

        public void AgregarUsuarioFamilia(Guid idUsuario, Guid idFamilia)
        {
            string query = "INSERT INTO [dbo].[UsuarioFamilia] (IdUsuario, IdFamilia) VALUES (@IdUsuario, @IdFamilia)";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdUsuario", idUsuario),
                new SqlParameter("@IdFamilia", idFamilia));
        }

        public void EliminarUsuarioFamilia(Guid idUsuario, Guid idFamilia)
        {
            string query = "DELETE FROM [dbo].[UsuarioFamilia] WHERE IdUsuario = @IdUsuario AND IdFamilia = @IdFamilia";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdUsuario", idUsuario),
                new SqlParameter("@IdFamilia", idFamilia));
        }

        public DataTable GetUsuarioFamilia()
        {
            string query = @"SELECT u.IdUsuario, f.IdFamilia, u.Nombre as [Usuario], f.Nombre as [Permiso]
                     FROM Usuario u
                     INNER JOIN UsuarioFamilia uf ON u.IdUsuario = uf.IdUsuario
                     INNER JOIN Familia f ON uf.IdFamilia = f.IdFamilia
                     WHERE u.Habilitado = 1";

            return SqlHelper.ExecuteDataTable(query, CommandType.Text);
        }
        // Solo trae los datos. No arma el objeto, solo devuelve las filas.
        public List<object[]> GetPatentesByUsuario(Guid idUsuario)
        {
            string query = "SELECT p.IdPatente, p.DataKey, p.TipoAcceso FROM [dbo].[Patente] p INNER JOIN [dbo].[UsuarioPatente] up ON p.IdPatente = up.IdPatente WHERE up.IdUsuario = @IdUsuario";
            return ExecuteQuery(query, new SqlParameter("@IdUsuario", idUsuario));
        }

        public List<object[]> GetFamiliasByUsuario(Guid idUsuario)
        {
            string query = "SELECT f.IdFamilia, f.Nombre FROM [dbo].[Familia] f INNER JOIN [dbo].[UsuarioFamilia] uf ON f.IdFamilia = uf.IdFamilia WHERE uf.IdUsuario = @IdUsuario";
            return ExecuteQuery(query, new SqlParameter("@IdUsuario", idUsuario));
        }

        public List<object[]> GetPatentesByFamilia(Guid idFamilia)
        {
            string query = "SELECT p.IdPatente, p.DataKey, p.TipoAcceso FROM [dbo].[Patente] p INNER JOIN [dbo].[FamiliaPatente] fp ON p.IdPatente = fp.IdPatente WHERE fp.IdFamilia = @IdFamilia";
            return ExecuteQuery(query, new SqlParameter("@IdFamilia", idFamilia));
        }
        // Método privado para evitar repetir el ciclo del SqlDataReader
        private List<object[]> ExecuteQuery(string query, SqlParameter param)
        {
            var resultados = new List<object[]>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, param))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    resultados.Add(data);
                }
            }
            return resultados;
        }
    }
}
