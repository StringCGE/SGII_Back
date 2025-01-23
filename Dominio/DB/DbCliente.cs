
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbCliente : DbSQLServer2022
    {
        public DbCliente() { }

        public async Task<int> CrearAsync(ClsCliente Cliente)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO Cliente (persona_id, identificacion, tipoIdentificacion_id, telefono, dtReg, idPersReg, estado)
                                 VALUES (@persona_id, @identificacion, @tipoIdentificacion_id, @telefono, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@persona_id", Cliente.persona?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@identificacion", (object)Cliente.identificacion ?? DBNull.Value);
                        command.Parameters.AddWithValue("@tipoIdentificacion_id", Cliente.tipoIdentificacion?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@telefono", (object)Cliente.telefono ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)Cliente.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)Cliente.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)Cliente.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        Cliente.id = Convert.ToInt32(result);
                        return (int)Cliente.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsCliente Cliente)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE Cliente
                             SET persona_id = @persona_id, identificacion = @identificacion, tipoIdentificacion_id = @tipoIdentificacion_id, telefono = @telefono, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@persona_id", Cliente.persona?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@identificacion", (object)Cliente.identificacion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@tipoIdentificacion_id", Cliente.tipoIdentificacion?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefono", (object)Cliente.telefono ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)Cliente.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)Cliente.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)Cliente.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)Cliente.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM Cliente WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsCliente>> ListarAsync(FetchDataCliente fetchData)
        {
            List<ClsCliente> Clientes = new List<ClsCliente>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) 
                      Cliente.persona_id, Cliente.identificacion, Cliente.tipoIdentificacion_id, Cliente.telefono, 
                      Cliente.id, Cliente.dtReg, Cliente.idPersReg, Cliente.estado
                      FROM Cliente
                      LEFT JOIN Persona ON Cliente.persona_id = Persona.id
                      WHERE Cliente.dtReg < @offsetDT
                      AND Cliente.estado != 0");

                if (!string.IsNullOrWhiteSpace(fetchData.identificacion))
                    queryBuilder.Append(" AND Cliente.identificacion LIKE @identificacion");

                if (fetchData.persona != null && !string.IsNullOrWhiteSpace(fetchData.persona.nombre1))
                    queryBuilder.Append(" AND Persona.nombre1 LIKE @nombre1");

                queryBuilder.Append(" ORDER BY Cliente.dtReg DESC");

                string query = queryBuilder.ToString();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offsetDT", fetchData.offsetDT);
                    command.Parameters.AddWithValue("@take", fetchData.take);

                    if (!string.IsNullOrWhiteSpace(fetchData.identificacion))
                    {
                        command.Parameters.AddWithValue("@identificacion", "%" + fetchData.identificacion + "%");
                    }
                    if (fetchData.persona != null && !string.IsNullOrWhiteSpace(fetchData.persona.nombre1)) if (!string.IsNullOrWhiteSpace(fetchData.persona.nombre1))
                    {
                        command.Parameters.AddWithValue("@nombre1", "%" + fetchData.persona.nombre1 + "%");
                    }
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Cliente = new ClsCliente
                            {
                                persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
                                //persona_id = reader.GetInt32(reader.GetOrdinal("persona_id")),
                                identificacion = reader.IsDBNull(reader.GetOrdinal("identificacion")) ? null : reader.GetString(reader.GetOrdinal("identificacion")),
                                tipoIdentificacion = new ClsTipoIdentificacion { id = reader.GetInt32(reader.GetOrdinal("tipoIdentificacion_id")) },
                                //tipoIdentificacion_id = reader.GetInt32(reader.GetOrdinal("tipoIdentificacion_id")),
                                telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? null : reader.GetString(reader.GetOrdinal("telefono")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? 1 : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            Clientes.Add(Cliente);
                        }
                    }
                }
            }
            return Clientes;
        }

        public async Task<ClsCliente> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT persona_id, identificacion, tipoIdentificacion_id, telefono, id, dtReg, idPersReg, estado
                                           FROM Cliente 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsCliente
                        {
                                persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
                                //persona_id = reader.GetInt32(reader.GetOrdinal("persona_id")),
                                identificacion = reader.IsDBNull(reader.GetOrdinal("identificacion")) ? null : reader.GetString(reader.GetOrdinal("identificacion")),
                                tipoIdentificacion = new ClsTipoIdentificacion { id = reader.GetInt32(reader.GetOrdinal("tipoIdentificacion_id")) },
                                //tipoIdentificacion_id = reader.GetInt32(reader.GetOrdinal("tipoIdentificacion_id")),
                                telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? null : reader.GetString(reader.GetOrdinal("telefono")),
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
