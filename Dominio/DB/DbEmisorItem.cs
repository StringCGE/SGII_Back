
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbEmisorItem : DbSQLServer2022
    {
        public DbEmisorItem() { }

        public async Task<int> CrearAsync(ClsEmisorItem EmisorItem)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO EmisorItem (emisor_id, emisorEstablecimiento_id, puntoEmision, dtReg, idPersReg, estado)
                                 VALUES (@emisor_id, @emisorEstablecimiento_id, @puntoEmision, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@emisor_id", EmisorItem.emisor?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@emisorEstablecimiento_id", EmisorItem.emisorEstablecimiento?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@puntoEmision", (object)EmisorItem.puntoEmision ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)EmisorItem.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)EmisorItem.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)EmisorItem.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        EmisorItem.id = Convert.ToInt32(result);
                        return (int)EmisorItem.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsEmisorItem EmisorItem)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE EmisorItem
                             SET emisor_id = @emisor_id, emisorEstablecimiento_id = @emisorEstablecimiento_id, puntoEmision = @puntoEmision, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@emisor_id", EmisorItem.emisor?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@emisorEstablecimiento_id", EmisorItem.emisorEstablecimiento?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@puntoEmision", (object)EmisorItem.puntoEmision ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)EmisorItem.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)EmisorItem.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)EmisorItem.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)EmisorItem.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM EmisorItem WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsEmisorItem>> ListarAsync(FetchDataEmisorItem fetchData)
        {
            List<ClsEmisorItem> EmisorItems = new List<ClsEmisorItem>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) emisor_id, emisorEstablecimiento_id, puntoEmision, id, dtReg, idPersReg, estado
                                  FROM EmisorItem
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
                            var EmisorItem = new ClsEmisorItem
                            {
                                emisor = new ClsEmisor { id = reader.GetInt32(reader.GetOrdinal("emisor_id")) },
                                //emisor_id = reader.GetInt32(reader.GetOrdinal("emisor_id")),
                                emisorEstablecimiento = new ClsEmisorEstablecimiento { id = reader.GetInt32(reader.GetOrdinal("emisorEstablecimiento_id")) },
                                //emisorEstablecimiento_id = reader.GetInt32(reader.GetOrdinal("emisorEstablecimiento_id")),
                                puntoEmision = reader.IsDBNull(reader.GetOrdinal("puntoEmision")) ? null : reader.GetString(reader.GetOrdinal("puntoEmision")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? 1 : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            EmisorItems.Add(EmisorItem);
                        }
                    }
                }
            }
            return EmisorItems;
        }

        public async Task<ClsEmisorItem> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT emisor_id, emisorEstablecimiento_id, puntoEmision, id, dtReg, idPersReg, estado
                                           FROM EmisorItem 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsEmisorItem
                        {
                                emisor = new ClsEmisor { id = reader.GetInt32(reader.GetOrdinal("emisor_id")) },
                                //emisor_id = reader.GetInt32(reader.GetOrdinal("emisor_id")),
                                emisorEstablecimiento = new ClsEmisorEstablecimiento { id = reader.GetInt32(reader.GetOrdinal("emisorEstablecimiento_id")) },
                                //emisorEstablecimiento_id = reader.GetInt32(reader.GetOrdinal("emisorEstablecimiento_id")),
                                puntoEmision = reader.IsDBNull(reader.GetOrdinal("puntoEmision")) ? null : reader.GetString(reader.GetOrdinal("puntoEmision")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? 1 : reader.GetInt32(reader.GetOrdinal("id")),
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
