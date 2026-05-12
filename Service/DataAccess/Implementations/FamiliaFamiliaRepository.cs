using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Service.DateAccess.Implementations;
using Service.DateAccess.Interface;
using Service.DataAccess.Tools;
using Service.DomainModel.Composite;

namespace Service.DataAccess.Implementations
{
    internal class FamiliaFamiliaRepository : IJoinRepository<Familia>
    {
        public IList<Component> GetByObject(Familia obj)
        {
            List<Component> familias = new List<Component>();

            string query = "SELECT IdFamiliaHijo FROM FamiliaFamilia WHERE IdFamiliaPadre = @IdFamiliaPadre";
            SqlParameter param = new SqlParameter("@IdFamiliaPadre", obj.Id);
            
            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, param))
            {
                while (reader.Read())
                {                    
                    Guid idFamilia = reader.GetGuid(0);
                    familias.Add(new FamiliaRepository().GetById(idFamilia));
                }
            }

            return familias;
        }

        IList<Component> IJoinRepository<Familia>.GetByObject(Familia parent)
        {
            throw new NotImplementedException();
        }
    }
}
