using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbNacionalidad : DbSQLServer2022
    {
        public DbNacionalidad()
        {
            // Constructor, inicializa la conexión si es necesario
        }

        // Crear una nueva nacionalidad
        public async Task<int> CrearAsync(ClsNacionalidad nacionalidad)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO Nacionalidad (dtReg, idPersReg, estado, nombre)
                                 VALUES (@dtReg, @idPersReg, @estado, @nombre);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@dtReg", (object)nacionalidad.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)nacionalidad.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)nacionalidad.estado ?? DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", (object)nacionalidad.nombre ?? DBNull.Value);

                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        nacionalidad.id = Convert.ToInt32(result);
                        return (int)nacionalidad.id;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        // Editar una nacionalidad
        public async Task<bool> EditarAsync(ClsNacionalidad nacionalidad)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE Nacionalidad
                             SET dtReg = @dtReg, idPersReg = @idPersReg, 
                                 estado = @estado, nombre = @nombre
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", nacionalidad.id);
                    command.Parameters.AddWithValue("@dtReg", nacionalidad.dtReg);
                    command.Parameters.AddWithValue("@idPersReg", nacionalidad.idPersReg);
                    command.Parameters.AddWithValue("@estado", nacionalidad.estado);
                    command.Parameters.AddWithValue("@nombre", nacionalidad.nombre);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        // Eliminar una nacionalidad
        public async Task<bool> EliminarAsync(int id)
        {
            using (var connection = GetConnection())
            {
                string query = @"DELETE FROM Nacionalidad WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        // Listar nacionalidades con filtro
        public async Task<List<ClsNacionalidad>> ListarAsync(FetchDataNacionalidad fetchData)
        {
            List<ClsNacionalidad> nacionalidades = new List<ClsNacionalidad>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre
                                  FROM Nacionalidad
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
                            var nacionalidad = new ClsNacionalidad
                            {
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre"))
                            };

                            nacionalidades.Add(nacionalidad);
                        }
                    }
                }
            }
            return nacionalidades;
        }

        // Obtener nacionalidad por ID
        public async Task<ClsNacionalidad> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT id, dtReg, idPersReg, estado, nombre FROM Nacionalidad WHERE id = @id AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsNacionalidad
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
