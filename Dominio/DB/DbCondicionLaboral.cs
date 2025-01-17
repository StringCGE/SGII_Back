using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbCondicionLaboral : DbSQLServer2022
    {
        public DbCondicionLaboral()
        {
            // Constructor, inicializa la conexión si es necesario
        }

        // Crear una nueva CondicionLaboral
        public async Task<int> CrearAsync(ClsCondicionLaboral CondicionLaboral)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO CondicionLaboral (dtReg, idPersReg, estado, nombre)
                                 VALUES (@dtReg, @idPersReg, @estado, @nombre);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@dtReg", (object)CondicionLaboral.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)CondicionLaboral.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)CondicionLaboral.estado ?? DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", (object)CondicionLaboral.nombre ?? DBNull.Value);

                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        CondicionLaboral.id = Convert.ToInt32(result);
                        return (int)CondicionLaboral.id;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        // Editar una CondicionLaboral
        public async Task<bool> EditarAsync(ClsCondicionLaboral CondicionLaboral)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE CondicionLaboral
                             SET dtReg = @dtReg, idPersReg = @idPersReg, 
                                 estado = @estado, nombre = @nombre
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", CondicionLaboral.id);
                    command.Parameters.AddWithValue("@dtReg", CondicionLaboral.dtReg);
                    command.Parameters.AddWithValue("@idPersReg", CondicionLaboral.idPersReg);
                    command.Parameters.AddWithValue("@estado", CondicionLaboral.estado);
                    command.Parameters.AddWithValue("@nombre", CondicionLaboral.nombre);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        // Eliminar una CondicionLaboral
        public async Task<bool> EliminarAsync(int id)
        {
            using (var connection = GetConnection())
            {
                string query = @"DELETE FROM CondicionLaboral WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        // Listar CondicionLaborales con filtro
        public async Task<List<ClsCondicionLaboral>> ListarAsync(FetchDataCondicionLaboral fetchData)
        {
            List<ClsCondicionLaboral> CondicionLaborales = new List<ClsCondicionLaboral>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre
                                  FROM CondicionLaboral
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
                            var CondicionLaboral = new ClsCondicionLaboral
                            {
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre"))
                            };

                            CondicionLaborales.Add(CondicionLaboral);
                        }
                    }
                }
            }
            return CondicionLaborales;
        }

        // Obtener CondicionLaboral por ID
        public async Task<ClsCondicionLaboral> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT id, dtReg, idPersReg, estado, nombre FROM CondicionLaboral WHERE id = @id AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsCondicionLaboral
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
