
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbPersona : DbSQLServer2022
    {
        public DbPersona() { }

        public async Task<int> CrearAsync(ClsPersona Persona)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO Persona (nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo, dtReg, idPersReg, estado)
                                 VALUES (@nombre1, @nombre2, @apellido1, @apellido2, @fechaNacimiento, @cedula, @sexo_id, @estadoCivil_id, @nacionalidad_id, @grupoSanguineo, @tipoSanguineo, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre1", (object)Persona.nombre1 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@nombre2", (object)Persona.nombre2 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@apellido1", (object)Persona.apellido1 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@apellido2", (object)Persona.apellido2 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@fechaNacimiento", (object)Persona.fechaNacimiento ?? DBNull.Value);
                        command.Parameters.AddWithValue("@cedula", (object)Persona.cedula ?? DBNull.Value);
                        command.Parameters.AddWithValue("@sexo_id", Persona.sexo?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@estadoCivil_id", Persona.estadoCivil?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@nacionalidad_id", Persona.nacionalidad?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@grupoSanguineo", (object)Persona.grupoSanguineo ?? DBNull.Value);
                        command.Parameters.AddWithValue("@tipoSanguineo", (object)Persona.tipoSanguineo ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)Persona.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)Persona.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)Persona.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        Persona.id = Convert.ToInt32(result);
                        return (int)Persona.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsPersona Persona)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE Persona
                             SET nombre1 = @nombre1, nombre2 = @nombre2, apellido1 = @apellido1, apellido2 = @apellido2, fechaNacimiento = @fechaNacimiento, cedula = @cedula, sexo_id = @sexo_id, estadoCivil_id = @estadoCivil_id, nacionalidad_id = @nacionalidad_id, grupoSanguineo = @grupoSanguineo, tipoSanguineo = @tipoSanguineo, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre1", (object)Persona.nombre1 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@nombre2", (object)Persona.nombre2 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@apellido1", (object)Persona.apellido1 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@apellido2", (object)Persona.apellido2 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@fechaNacimiento", (object)Persona.fechaNacimiento ?? DBNull.Value);
                    command.Parameters.AddWithValue("@cedula", (object)Persona.cedula ?? DBNull.Value);
                    command.Parameters.AddWithValue("@sexo", Persona.sexo?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@estadoCivil", Persona.estadoCivil?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nacionalidad", Persona.nacionalidad?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@grupoSanguineo", (object)Persona.grupoSanguineo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@tipoSanguineo", (object)Persona.tipoSanguineo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)Persona.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)Persona.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)Persona.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM Persona WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsPersona>> ListarAsync(FetchDataPersona fetchData)
        {
            List<ClsPersona> Personas = new List<ClsPersona>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo, id, dtReg, idPersReg, estado
                                  FROM Persona
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
                            var Persona = new ClsPersona
                            {
                                nombre1 = reader.IsDBNull(reader.GetOrdinal("nombre1")) ? null : reader.GetString(reader.GetOrdinal("nombre1")),
                                nombre2 = reader.IsDBNull(reader.GetOrdinal("nombre2")) ? null : reader.GetString(reader.GetOrdinal("nombre2")),
                                apellido1 = reader.IsDBNull(reader.GetOrdinal("apellido1")) ? null : reader.GetString(reader.GetOrdinal("apellido1")),
                                apellido2 = reader.IsDBNull(reader.GetOrdinal("apellido2")) ? null : reader.GetString(reader.GetOrdinal("apellido2")),
                                fechaNacimiento = reader.IsDBNull(reader.GetOrdinal("fechaNacimiento")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaNacimiento")),
                                cedula = reader.IsDBNull(reader.GetOrdinal("cedula")) ? null : reader.GetString(reader.GetOrdinal("cedula")),
                                sexo = new ClsSexo { id = reader.GetInt32(reader.GetOrdinal("sexo_id")) },
                                //sexo_id = reader.GetInt32(reader.GetOrdinal("sexo_id")),
                                estadoCivil = new ClsEstadoCivil { id = reader.GetInt32(reader.GetOrdinal("estadoCivil_id")) },
                                //estadoCivil_id = reader.GetInt32(reader.GetOrdinal("estadoCivil_id")),
                                nacionalidad = new ClsNacionalidad { id = reader.GetInt32(reader.GetOrdinal("nacionalidad_id")) },
                                //nacionalidad_id = reader.GetInt32(reader.GetOrdinal("nacionalidad_id")),
                                grupoSanguineo = reader.IsDBNull(reader.GetOrdinal("grupoSanguineo")) ? null : reader.GetString(reader.GetOrdinal("grupoSanguineo")),
                                tipoSanguineo = reader.IsDBNull(reader.GetOrdinal("tipoSanguineo")) ? null : reader.GetString(reader.GetOrdinal("tipoSanguineo")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            Personas.Add(Persona);
                        }
                    }
                }
            }
            return Personas;
        }

        public async Task<ClsPersona> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT TOP (@take) nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo, id, dtReg, idPersReg, estado
                                           FROM Persona 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsPersona
                        {
                                nombre1 = reader.IsDBNull(reader.GetOrdinal("nombre1")) ? null : reader.GetString(reader.GetOrdinal("nombre1")),
                                nombre2 = reader.IsDBNull(reader.GetOrdinal("nombre2")) ? null : reader.GetString(reader.GetOrdinal("nombre2")),
                                apellido1 = reader.IsDBNull(reader.GetOrdinal("apellido1")) ? null : reader.GetString(reader.GetOrdinal("apellido1")),
                                apellido2 = reader.IsDBNull(reader.GetOrdinal("apellido2")) ? null : reader.GetString(reader.GetOrdinal("apellido2")),
                                fechaNacimiento = reader.IsDBNull(reader.GetOrdinal("fechaNacimiento")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaNacimiento")),
                                cedula = reader.IsDBNull(reader.GetOrdinal("cedula")) ? null : reader.GetString(reader.GetOrdinal("cedula")),
                                sexo = new ClsSexo { id = reader.GetInt32(reader.GetOrdinal("sexo_id")) },
                                //sexo_id = reader.GetInt32(reader.GetOrdinal("sexo_id")),
                                estadoCivil = new ClsEstadoCivil { id = reader.GetInt32(reader.GetOrdinal("estadoCivil_id")) },
                                //estadoCivil_id = reader.GetInt32(reader.GetOrdinal("estadoCivil_id")),
                                nacionalidad = new ClsNacionalidad { id = reader.GetInt32(reader.GetOrdinal("nacionalidad_id")) },
                                //nacionalidad_id = reader.GetInt32(reader.GetOrdinal("nacionalidad_id")),
                                grupoSanguineo = reader.IsDBNull(reader.GetOrdinal("grupoSanguineo")) ? null : reader.GetString(reader.GetOrdinal("grupoSanguineo")),
                                tipoSanguineo = reader.IsDBNull(reader.GetOrdinal("tipoSanguineo")) ? null : reader.GetString(reader.GetOrdinal("tipoSanguineo")),
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
