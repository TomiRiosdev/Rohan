using Service.DataAccess.Tools;
using Service.DomainModel.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Service.DateAccess.Implementations
{
    public class BitácoraRepository
    {
        public void Insertar(Bitácora log)
        {  
            string query = @"INSERT INTO Bitacora (IdUsuario, Mensaje, Criticidad) 
                             VALUES (@IdUsuario, @Mensaje, @Criticidad)";

            SqlParameter[] parametros = new SqlParameter[]
            {
               
                new SqlParameter("@IdUsuario", log.IdUsuario.HasValue ? (object)log.IdUsuario.Value : DBNull.Value),
                new SqlParameter("@Mensaje", log.Mensaje),         
                new SqlParameter("@Criticidad", log.Criticidad.ToString())
            };

            SqlHelper.ExecuteNonQuery(query, CommandType.Text, parametros);
        }

        public List<Bitácora> ListarTodos()
        {
            List<Bitácora> lista = new List<Bitácora>();

            string query = @"SELECT b.IdBitacora, b.Fecha, b.IdUsuario, u.Nombre AS NombreUsuario, b.Mensaje, b.Criticidad 
                     FROM Bitacora b
                     LEFT JOIN Usuario u ON b.IdUsuario = u.IdUsuario
                     ORDER BY b.Fecha DESC"; 

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    Bitácora log = new Bitácora();
                    log.IdBitacora = Convert.ToInt32(reader["IdBitacora"]);
                    log.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    log.Mensaje = reader["Mensaje"].ToString();

                    log.Criticidad = (Criticidad)Enum.Parse(typeof(Criticidad), reader["Criticidad"].ToString());
                  
                    if (reader["IdUsuario"] != DBNull.Value)
                    {
                        log.IdUsuario = Guid.Parse(reader["IdUsuario"].ToString());
                        log.NombreUsuario = reader["NombreUsuario"].ToString();
                    }
                    else
                    {
                        log.NombreUsuario = "Sistema"; 
                    }

                    lista.Add(log);
                }
            }

            return lista;
        }
    }
}

