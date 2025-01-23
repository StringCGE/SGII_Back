
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbEmisorEstablecimiento : DbSQLServer2022
    {
        public DbEmisorEstablecimiento() { }

        public async Task<int> CrearAsync(ClsEmisorEstablecimiento EmisorEstablecimiento)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO EmisorEstablecimiento (emisor_id, numero, nombre, direccion, puntosDeEmision, dtReg, idPersReg, estado)
                                 VALUES (@emisor_id, @numero, @nombre, @direccion, @puntosDeEmision, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@emisor_id", EmisorEstablecimiento.emisor?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@numero", (object)EmisorEstablecimiento.numero ?? DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", (object)EmisorEstablecimiento.nombre ?? DBNull.Value);
                        command.Parameters.AddWithValue("@direccion", (object)EmisorEstablecimiento.direccion ?? DBNull.Value);
                        command.Parameters.AddWithValue("@puntosDeEmision", (object)EmisorEstablecimiento.puntosDeEmision ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)EmisorEstablecimiento.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)EmisorEstablecimiento.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)EmisorEstablecimiento.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        EmisorEstablecimiento.id = Convert.ToInt32(result);
                        return (int)EmisorEstablecimiento.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsEmisorEstablecimiento EmisorEstablecimiento)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE EmisorEstablecimiento
                             SET emisor_id = @emisor_id, numero = @numero, nombre = @nombre, direccion = @direccion, puntosDeEmision = @puntosDeEmision, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@emisor_id", EmisorEstablecimiento.emisor?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@numero", (object)EmisorEstablecimiento.numero ?? DBNull.Value);
                    command.Parameters.AddWithValue("@nombre", (object)EmisorEstablecimiento.nombre ?? DBNull.Value);
                    command.Parameters.AddWithValue("@direccion", (object)EmisorEstablecimiento.direccion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@puntosDeEmision", (object)EmisorEstablecimiento.puntosDeEmision ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)EmisorEstablecimiento.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)EmisorEstablecimiento.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)EmisorEstablecimiento.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)EmisorEstablecimiento.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM EmisorEstablecimiento WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsEmisorEstablecimiento>> ListarAsync(FetchDataEmisorEstablecimiento fetchData)
        {
            List<ClsEmisorEstablecimiento> EmisorEstablecimientos = new List<ClsEmisorEstablecimiento>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) emisor_id, numero, nombre, direccion, puntosDeEmision, id, dtReg, idPersReg, estado
                                  FROM EmisorEstablecimiento
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
                            var EmisorEstablecimiento = new ClsEmisorEstablecimiento
                            {
                                emisor = new ClsEmisor { id = reader.GetInt32(reader.GetOrdinal("emisor_id")) },
                                //emisor_id = reader.GetInt32(reader.GetOrdinal("emisor_id")),
                                numero = reader.IsDBNull(reader.GetOrdinal("numero")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("numero")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre")),
                                direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? null : reader.GetString(reader.GetOrdinal("direccion")),
                                puntosDeEmision = reader.IsDBNull(reader.GetOrdinal("puntosDeEmision")) ? null : reader.GetString(reader.GetOrdinal("puntosDeEmision")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? 1 : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            EmisorEstablecimientos.Add(EmisorEstablecimiento);
                        }
                    }
                }
            }
            return EmisorEstablecimientos;
        }

        public async Task<ClsEmisorEstablecimiento> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT emisor_id, numero, nombre, direccion, puntosDeEmision, id, dtReg, idPersReg, estado
                                           FROM EmisorEstablecimiento 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsEmisorEstablecimiento
                        {
                                emisor = new ClsEmisor { id = reader.GetInt32(reader.GetOrdinal("emisor_id")) },
                                //emisor_id = reader.GetInt32(reader.GetOrdinal("emisor_id")),
                                numero = reader.IsDBNull(reader.GetOrdinal("numero")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("numero")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre")),
                                direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? null : reader.GetString(reader.GetOrdinal("direccion")),
                                puntosDeEmision = reader.IsDBNull(reader.GetOrdinal("puntosDeEmision")) ? null : reader.GetString(reader.GetOrdinal("puntosDeEmision")),
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
