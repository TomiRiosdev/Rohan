using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Service.DataAccess.Tools;
using Service.DomainModel.Composite;
using Service.DataAccess.Implementations.Adapters;
using Service.DateAccess.Interface;

namespace Service.DataAccess.Implementations
{
    internal class PatenteRepository : IGenericRepository<Patente>
    {
        public Patente GetById(Guid id)
        {
            string SelectByIdStatement = "SELECT IdPatente, DataKey, TipoAcceso FROM [dbo].[Patente] WHERE IdPatente = @IdPatente";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectByIdStatement,
                                                     CommandType.Text,
                                                     new SqlParameter[] { new SqlParameter("@IdPatente", id) }))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return PatenteAdapter.Current.Get(data);
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<Patente> GetAll()
        {
            List<Patente> patentes = new List<Patente>();
            string query = "SELECT IdPatente, DataKey, TipoAcceso FROM [dbo].[Patente]";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    patentes.Add(PatenteAdapter.Current.Get(data));
                }
            }
            return patentes;
        }

        public void Add(Patente obj)
        {
            string query = "INSERT INTO [dbo].[Patente] (IdPatente, DataKey, TipoAcceso) VALUES (@IdPatente, @DataKey, @TipoAcceso)";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdPatente", obj.Id),
                new SqlParameter("@DataKey", obj.DataKey),
                new SqlParameter("@TipoAcceso", (int)obj.TipoAcceso));
        }

        public void Update(Patente obj)
        {
            string query = "UPDATE [dbo].[Patente] SET DataKey = @DataKey, TipoAcceso = @TipoAcceso WHERE IdPatente = @IdPatente";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdPatente", obj.Id),
                new SqlParameter("@DataKey", obj.DataKey),
                new SqlParameter("@TipoAcceso", (int)obj.TipoAcceso));
        }

        public void Remove(Guid id)
        {
            // Nota: En la vida real, primero debes borrar las dependencias en FamiliaPatente y UsuarioPatente
            string query = "DELETE FROM [dbo].[Patente] WHERE IdPatente = @IdPatente";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text, new SqlParameter("@IdPatente", id));
        }
    }
}
