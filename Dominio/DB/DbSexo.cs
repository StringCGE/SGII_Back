using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbSexo : DbSQLServer2022
    {
        public DbSexo() { }

        public async Task<int> CrearAsync(ClsSexo Sexo)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO Sexo (dtReg, idPersReg, estado, nombre)
                                 VALUES (@dtReg, @idPersReg, @estado, @nombre);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@dtReg", (object)Sexo.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)Sexo.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)Sexo.estado ?? DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", (object)Sexo.nombre ?? DBNull.Value);

                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        Sexo.id = Convert.ToInt32(result);
                        return (int)Sexo.id;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsSexo Sexo)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE Sexo
                             SET dtReg = @dtReg, idPersReg = @idPersReg, 
                                 estado = @estado, nombre = @nombre
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Sexo.id);
                    command.Parameters.AddWithValue("@dtReg", Sexo.dtReg);
                    command.Parameters.AddWithValue("@idPersReg", Sexo.idPersReg);
                    command.Parameters.AddWithValue("@estado", Sexo.estado);
                    command.Parameters.AddWithValue("@nombre", Sexo.nombre);

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
                string query = @"DELETE FROM Sexo WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsSexo>> ListarAsync(FetchDataSexo fetchData)
        {
            List<ClsSexo> Sexoes = new List<ClsSexo>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre
                                  FROM Sexo
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
                            var Sexo = new ClsSexo
                            {
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre"))
                            };

                            Sexoes.Add(Sexo);
                        }
                    }
                }
            }
            return Sexoes;
        }

        public async Task<ClsSexo> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT id, dtReg, idPersReg, estado, nombre FROM Sexo WHERE id = @id AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsSexo
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
