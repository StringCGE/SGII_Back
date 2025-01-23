
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbTipoIdentificacion : DbSQLServer2022
    {
        public DbTipoIdentificacion() { }

        public async Task<int> CrearAsync(ClsTipoIdentificacion TipoIdentificacion)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO TipoIdentificacion (nombre, detalle, pais_id, dtReg, idPersReg, estado)
                                 VALUES (@nombre, @detalle, @pais_id, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", (object)TipoIdentificacion.nombre ?? DBNull.Value);
                        command.Parameters.AddWithValue("@detalle", (object)TipoIdentificacion.detalle ?? DBNull.Value);
                        command.Parameters.AddWithValue("@pais_id", TipoIdentificacion.pais?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)TipoIdentificacion.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)TipoIdentificacion.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)TipoIdentificacion.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        TipoIdentificacion.id = Convert.ToInt32(result);
                        return (int)TipoIdentificacion.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsTipoIdentificacion TipoIdentificacion)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE TipoIdentificacion
                             SET nombre = @nombre, detalle = @detalle, pais_id = @pais_id, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", (object)TipoIdentificacion.nombre ?? DBNull.Value);
                    command.Parameters.AddWithValue("@detalle", (object)TipoIdentificacion.detalle ?? DBNull.Value);
                    command.Parameters.AddWithValue("@pais_id", TipoIdentificacion.pais?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)TipoIdentificacion.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)TipoIdentificacion.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)TipoIdentificacion.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)TipoIdentificacion.estado ?? DBNull.Value);
                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using (var connection = GetConnection())
            {
                string query = @"DELETE FROM TipoIdentificacion WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsTipoIdentificacion>> ListarAsync(FetchDataTipoIdentificacion fetchData)
        {
            List<ClsTipoIdentificacion> TipoIdentificacions = new List<ClsTipoIdentificacion>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) nombre, detalle, pais_id, id, dtReg, idPersReg, estado
                                  FROM TipoIdentificacion
                                  WHERE dtReg < @offsetDT
                                  AND estado != 0");
                //if (!string.IsNullOrWhiteSpace(fetchData.nombre1)) queryBuilder.Append(" AND (nombre1 LIKE @nombre)");
                queryBuilder.Append(" ORDER BY dtReg DESC");

                string query = queryBuilder.ToString();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offsetDT", fetchData.offsetDT);
                    command.Parameters.AddWithValue("@take", fetchData.take);

                    /*if (!string.IsNullOrWhiteSpace(fetchData.nombre1))
                    {
                        command.Parameters.AddWithValue("@nombre1", "%" + fetchData.nombre1 + "%");
                    }*/

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var TipoIdentificacion = new ClsTipoIdentificacion
                            {
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre")),
                                detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
                                pais = new ClsNacionalidad { id = reader.GetInt32(reader.GetOrdinal("pais_id")) },
                                //pais_id = reader.GetInt32(reader.GetOrdinal("pais_id")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            TipoIdentificacions.Add(TipoIdentificacion);
                        }
                    }
                }
            }
            return TipoIdentificacions;
        }

        public async Task<ClsTipoIdentificacion> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT nombre, detalle, pais_id, id, dtReg, idPersReg, estado
                                           FROM TipoIdentificacion 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsTipoIdentificacion
                        {
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre")),
                                detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
                                pais = new ClsNacionalidad { id = reader.GetInt32(reader.GetOrdinal("pais_id")) },
                                //pais_id = reader.GetInt32(reader.GetOrdinal("pais_id")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                        };
                    }
                }
            }
            return null;
        }
    }

}
