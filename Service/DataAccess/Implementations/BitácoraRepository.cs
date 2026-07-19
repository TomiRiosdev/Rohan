using Service.DataAccess.Tools;
using Service.DomainModel.Logging;
using Service.Facade;
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

            Guid nuevoId = Guid.NewGuid();

            string query = @"INSERT INTO Bitacora (IdBitacora,Fecha, IdUsuario, Mensaje, Criticidad, NombreUsuario, IdSucursal) 
                     VALUES (@IdBitacora,@Fecha, @IdUsuario, @Mensaje, @Criticidad, @NombreUsuario, @IdSucursal)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdBitacora", nuevoId),
                new SqlParameter("@Fecha", DateTime.Now),
                new SqlParameter("@IdUsuario", log.IdUsuario.HasValue ? (object)log.IdUsuario.Value : DBNull.Value),
                new SqlParameter("@Mensaje", log.Mensaje),
                new SqlParameter("@Criticidad", log.Criticidad.ToString()),
                new SqlParameter("@NombreUsuario", (object)log.NombreUsuario ??"Sistema"),
                new SqlParameter("@IdSucursal", (object)log.IdSucursal ?? DBNull.Value)
            };

            SqlHelper.ExecuteNonQuery(query, CommandType.Text, parametros);
        }

        public List<Bitácora> Listar(Guid? idSucursal = null)
        {
            List<Bitácora> lista = new List<Bitácora>();

            string query = @"SELECT IdBitacora, Fecha, IdUsuario, NombreUsuario, Mensaje, Criticidad, IdSucursal 
                     FROM Bitacora 
                     WHERE (@IdSucursal IS NULL OR IdSucursal = @IdSucursal)
                     ORDER BY Fecha DESC";

            // 1. Definimos los parámetros correctamente
            SqlParameter[] parametros = new SqlParameter[]
            {
        // Si idSucursal es null, pasamos DBNull.Value, de lo contrario pasamos el Guid
        new SqlParameter("@IdSucursal", (object)idSucursal ?? DBNull.Value)
            };

            // 2. PASAMOS LOS PARÁMETROS AL EJECUTAR
            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, parametros))
            {
                while (reader.Read())
                {
                    Bitácora log = new Bitácora();

                    // Mapeo (Asegúrate de que IdBitacora sea Guid en tu clase Bitácora)
                    log.IdBitacora = Guid.Parse(reader["IdBitacora"].ToString());
                    log.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    log.Mensaje = reader["Mensaje"].ToString();
                    log.Criticidad = (Criticidad)Enum.Parse(typeof(Criticidad), reader["Criticidad"].ToString());

                    // Mapeo de Sucursal (Opcional: Si quieres tenerlo en tu modelo de objeto)
                    if (reader["IdSucursal"] != DBNull.Value)
                        log.IdSucursal = Guid.Parse(reader["IdSucursal"].ToString());

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

