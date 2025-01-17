using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{

    public class DbEstadoLaboral : DbSQLServer2022
    {
        public DbEstadoLaboral()
        {
            // Constructor, inicializa la conexión si es necesario
        }

        // Crear una nueva EstadoLaboral
        public async Task<int> CrearAsync(ClsEstadoLaboral EstadoLaboral)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO EstadoLaboral (dtReg, idPersReg, estado, nombre)
                                 VALUES (@dtReg, @idPersReg, @estado, @nombre);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@dtReg", (object)EstadoLaboral.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)EstadoLaboral.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)EstadoLaboral.estado ?? DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", (object)EstadoLaboral.nombre ?? DBNull.Value);

                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        EstadoLaboral.id = Convert.ToInt32(result);
                        return (int)EstadoLaboral.id;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        // Editar una EstadoLaboral
        public async Task<bool> EditarAsync(ClsEstadoLaboral EstadoLaboral)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE EstadoLaboral
                             SET dtReg = @dtReg, idPersReg = @idPersReg, 
                                 estado = @estado, nombre = @nombre
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", EstadoLaboral.id);
                    command.Parameters.AddWithValue("@dtReg", EstadoLaboral.dtReg);
                    command.Parameters.AddWithValue("@idPersReg", EstadoLaboral.idPersReg);
                    command.Parameters.AddWithValue("@estado", EstadoLaboral.estado);
                    command.Parameters.AddWithValue("@nombre", EstadoLaboral.nombre);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        // Eliminar una EstadoLaboral
        public async Task<bool> EliminarAsync(int id)
        {
            using (var connection = GetConnection())
            {
                string query = @"DELETE FROM EstadoLaboral WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        // Listar EstadoLaborales con filtro
        public async Task<List<ClsEstadoLaboral>> ListarAsync(FetchDataEstadoLaboral fetchData)
        {
            List<ClsEstadoLaboral> EstadoLaborales = new List<ClsEstadoLaboral>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre
                                  FROM EstadoLaboral
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
                            var EstadoLaboral = new ClsEstadoLaboral
                            {
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre"))
                            };

                            EstadoLaborales.Add(EstadoLaboral);
                        }
                    }
                }
            }
            return EstadoLaborales;
        }

        // Obtener EstadoLaboral por ID
        public async Task<ClsEstadoLaboral> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT id, dtReg, idPersReg, estado, nombre FROM EstadoLaboral WHERE id = @id AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsEstadoLaboral
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
