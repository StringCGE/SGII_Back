using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbEstadoCivil : DbSQLServer2022
    {
        public DbEstadoCivil()
        {
            // Constructor, inicializa la conexión si es necesario
        }

        // Crear una nueva EstadoCivil
        public async Task<int> CrearAsync(ClsEstadoCivil EstadoCivil)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO EstadoCivil (dtReg, idPersReg, estado, nombre)
                                 VALUES (@dtReg, @idPersReg, @estado, @nombre);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@dtReg", (object)EstadoCivil.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)EstadoCivil.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)EstadoCivil.estado ?? DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", (object)EstadoCivil.nombre ?? DBNull.Value);

                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        EstadoCivil.id = Convert.ToInt32(result);
                        return (int)EstadoCivil.id;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        // Editar una EstadoCivil
        public async Task<bool> EditarAsync(ClsEstadoCivil EstadoCivil)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE EstadoCivil
                             SET dtReg = @dtReg, idPersReg = @idPersReg, 
                                 estado = @estado, nombre = @nombre
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", EstadoCivil.id);
                    command.Parameters.AddWithValue("@dtReg", EstadoCivil.dtReg);
                    command.Parameters.AddWithValue("@idPersReg", EstadoCivil.idPersReg);
                    command.Parameters.AddWithValue("@estado", EstadoCivil.estado);
                    command.Parameters.AddWithValue("@nombre", EstadoCivil.nombre);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        // Eliminar una EstadoCivil
        public async Task<bool> EliminarAsync(int id)
        {
            using (var connection = GetConnection())
            {
                string query = @"DELETE FROM EstadoCivil WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        // Listar EstadoCiviles con filtro
        public async Task<List<ClsEstadoCivil>> ListarAsync(FetchDataEstadoCivil fetchData)
        {
            List<ClsEstadoCivil> EstadoCiviles = new List<ClsEstadoCivil>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre
                                  FROM EstadoCivil
                                  WHERE dtReg < @offsetDT
                                  AND estado != 0");
                if (!string.IsNullOrWhiteSpace(fetchData.nombre)) queryBuilder.Append(" AND nombre LIKE @nombre");
                queryBuilder.Append(" ORDER BY dtReg DESC");

                string query = queryBuilder.ToString();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offsetDT", fetchData.offsetDT);
                    command.Parameters.AddWithValue("@take", fetchData.take);

                    if (!string.IsNullOrWhiteSpace(fetchData.nombre))
                    {
                        command.Parameters.AddWithValue("@nombre", "%" + fetchData.nombre + "%");
                    }

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var EstadoCivil = new ClsEstadoCivil
                            {
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre"))
                            };

                            EstadoCiviles.Add(EstadoCivil);
                        }
                    }
                }
            }
            return EstadoCiviles;
        }

        // Obtener EstadoCivil por ID
        public async Task<ClsEstadoCivil> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT id, dtReg, idPersReg, estado, nombre FROM EstadoCivil WHERE id = @id AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsEstadoCivil
                        {
                            id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                            dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                            idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                            estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre"))
                        };
                    }
                }
            }

            return null;
        }
    }

}
