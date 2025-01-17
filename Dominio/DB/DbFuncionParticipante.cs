using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbFuncionParticipante : DbSQLServer2022
    {
        public DbFuncionParticipante() { }

        public async Task<int> CrearAsync(ClsFuncionParticipante item)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO FuncionParticipante (dtReg, idPersReg, estado, nombre, detalle)
                             VALUES (@dtReg, @idPersReg, @estado, @nombre, @detalle);
                             SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@dtReg", item.dtReg);
                        command.Parameters.AddWithValue("@idPersReg", item.idPersReg);
                        command.Parameters.AddWithValue("@estado", item.estado);
                        command.Parameters.AddWithValue("@nombre", item.nombre);
                        command.Parameters.AddWithValue("@detalle", item.detalle);

                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        item.id = Convert.ToInt32(result);
                        return (int)item.id;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            
        }

        public async Task<bool> EditarAsync(ClsFuncionParticipante item)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE FuncionParticipante
                             SET dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado, 
                                 nombre = @nombre, detalle = @detalle
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", item.id);
                    command.Parameters.AddWithValue("@dtReg", item.dtReg);
                    command.Parameters.AddWithValue("@idPersReg", item.idPersReg);
                    command.Parameters.AddWithValue("@estado", item.estado);
                    command.Parameters.AddWithValue("@nombre", item.nombre);
                    command.Parameters.AddWithValue("@detalle", item.detalle);

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
                string query = @"DELETE FROM FuncionParticipante WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsFuncionParticipante>> ListarAsync(FetchDataFuncionParticipante fetchData)
        {
            List<ClsFuncionParticipante> items = new List<ClsFuncionParticipante>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre, detalle
                                  FROM FuncionParticipante
                                  WHERE dtReg < @offsetDT
                                  AND estado != 0"); ;
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
                            var item = new ClsFuncionParticipante
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.GetInt32(reader.GetOrdinal("estado")),
                                nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                detalle = reader.GetString(reader.GetOrdinal("detalle"))
                            };

                            items.Add(item);
                        }
                    }
                }
            }
            return items;
        }

        public async Task<ClsFuncionParticipante> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT id, dtReg, idPersReg, estado, nombre, detalle FROM FuncionParticipante WHERE id = @id  AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsFuncionParticipante
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            dtReg = reader.GetDateTime(reader.GetOrdinal("dtReg")),
                            idPersReg = reader.GetInt32(reader.GetOrdinal("idPersReg")),
                            estado = reader.GetInt32(reader.GetOrdinal("estado")),
                            nombre = reader.GetString(reader.GetOrdinal("nombre")),
                            detalle = reader.GetString(reader.GetOrdinal("detalle"))
                        };
                    }
                }
            }

            return null;
        }
    }
}
