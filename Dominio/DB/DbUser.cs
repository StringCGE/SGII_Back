
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbUser : DbSQLServer2022
    {
        public DbUser() { }

        public async Task<int> CrearAsync(ClsUser User)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO MyUser (persona_id, email, urlFoto, role, password, salt, tempCode, tempCodeCreateAt, dtReg, idPersReg, estado)
                                 VALUES (@persona_id, @email, @urlFoto, @role, @password, @salt, @tempCode, @tempCodeCreateAt, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@persona_id", User.persona?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@email", (object)User.email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@urlFoto", (object)User.urlFoto ?? DBNull.Value);
                        command.Parameters.AddWithValue("@role", (object)User.role ?? DBNull.Value);
                        command.Parameters.AddWithValue("@password", (object)User.password ?? DBNull.Value);
                        command.Parameters.AddWithValue("@salt", (object)User.salt ?? DBNull.Value);
                        command.Parameters.AddWithValue("@tempCode", (object)User.tempCode ?? DBNull.Value);
                        command.Parameters.AddWithValue("@tempCodeCreateAt", (object)User.tempCodeCreateAt ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)User.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)User.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)User.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        User.id = Convert.ToInt32(result);
                        return (int)User.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public async Task<int> SumarIntento(ClsUser User)//Un procedimiento almacenado queda mejor que esto
        {
            if (User.id is null)
            {
                return - 1;
            }
            int intentos = await ObtenerIntentosPorIdAsync(User.id ?? 0);
            intentos += 1;
            using (var connection = GetConnection())
            {
                string query = @"UPDATE MyUser
                             SET intentos = @intentos
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", User.id);
                    command.Parameters.AddWithValue("@intentos", intentos);
                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return intentos;
                }
            }
            return intentos;
        }
        public async Task<int> ResetIntento(ClsUser User)//Un procedimiento almacenado queda mejor que esto
        {
            if (User.id is null)
            {
                return -1;
            }
            int intentos = 0;
            using (var connection = GetConnection())
            {
                string query = @"UPDATE MyUser
                             SET intentos = @intentos
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", User.id);
                    command.Parameters.AddWithValue("@intentos", intentos);
                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return intentos;
                }
            }
        }
        public async Task<bool> EditarAsync(ClsUser User)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE MyUser
                             SET persona_id = @persona_id, email = @email, urlFoto = @urlFoto, role = @role, password = @password, salt = @salt, tempCode = @tempCode, tempCodeCreateAt = @tempCodeCreateAt, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@persona_id", User.persona?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email", (object)User.email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@urlFoto", (object)User.urlFoto ?? DBNull.Value);
                    command.Parameters.AddWithValue("@role", (object)User.role ?? DBNull.Value);
                    command.Parameters.AddWithValue("@password", (object)User.password ?? DBNull.Value);
                    command.Parameters.AddWithValue("@salt", (object)User.salt ?? DBNull.Value);
                    command.Parameters.AddWithValue("@tempCode", (object)User.tempCode ?? DBNull.Value);
                    command.Parameters.AddWithValue("@tempCodeCreateAt", (object)User.tempCodeCreateAt ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)User.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)User.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)User.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)User.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM MyUser WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsUser>> ListarAsync(FetchDataUser fetchData)
        {
            List<ClsUser> Users = new List<ClsUser>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) persona_id, email, urlFoto, role, password, salt, tempCode, tempCodeCreateAt, id, dtReg, idPersReg, estado, intentos
                                  FROM MyUser
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
                            var User = new ClsUser
                            {
                                persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
                                intentos = reader.IsDBNull(reader.GetOrdinal("intentos")) ? 0 : reader.GetInt32(reader.GetOrdinal("intentos")),
                                email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                                urlFoto = reader.IsDBNull(reader.GetOrdinal("urlFoto")) ? null : reader.GetString(reader.GetOrdinal("urlFoto")),
                                role = reader.IsDBNull(reader.GetOrdinal("role")) ? null : reader.GetString(reader.GetOrdinal("role")),
                                password = reader.IsDBNull(reader.GetOrdinal("password")) ? null : reader.GetString(reader.GetOrdinal("password")),
                                salt = reader.IsDBNull(reader.GetOrdinal("salt")) ? null : reader.GetString(reader.GetOrdinal("salt")),
                                tempCode = reader.IsDBNull(reader.GetOrdinal("tempCode")) ? null : reader.GetString(reader.GetOrdinal("tempCode")),
                                tempCodeCreateAt = reader.IsDBNull(reader.GetOrdinal("tempCodeCreateAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("tempCodeCreateAt")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? 1 : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            Users.Add(User);
                        }
                    }
                }
            }
            return Users;
        }
        public async Task<int> ObtenerIntentosPorIdAsync(int id)
        {
            int intentos = -1;
            using (var connection = GetConnection())
            {
                var command = new SqlCommand(@"SELECT intentos
                                           FROM MyUser 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        intentos = reader.IsDBNull(reader.GetOrdinal("intentos")) ? 0 : reader.GetInt32(reader.GetOrdinal("intentos"));
                    }
                }
            }
            return intentos;
        }
        public async Task<ClsUser> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT persona_id, email, urlFoto, role, password, salt, tempCode, tempCodeCreateAt, id, dtReg, idPersReg, estado, intentos
                                           FROM MyUser 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsUser
                        {
                                persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
                                intentos =  reader.IsDBNull(reader.GetOrdinal("intentos")) ? 0 : reader.GetInt32(reader.GetOrdinal("intentos")),
                                email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                                urlFoto = reader.IsDBNull(reader.GetOrdinal("urlFoto")) ? null : reader.GetString(reader.GetOrdinal("urlFoto")),
                                role = reader.IsDBNull(reader.GetOrdinal("role")) ? null : reader.GetString(reader.GetOrdinal("role")),
                                password = reader.IsDBNull(reader.GetOrdinal("password")) ? null : reader.GetString(reader.GetOrdinal("password")),
                                salt = reader.IsDBNull(reader.GetOrdinal("salt")) ? null : reader.GetString(reader.GetOrdinal("salt")),
                                tempCode = reader.IsDBNull(reader.GetOrdinal("tempCode")) ? null : reader.GetString(reader.GetOrdinal("tempCode")),
                                tempCodeCreateAt = reader.IsDBNull(reader.GetOrdinal("tempCodeCreateAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("tempCodeCreateAt")),
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

        public async Task<ClsUser?> ObtenerPorEmailAsync(string email)
        {
            using (var connection = GetConnection())
            {

                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT persona_id, email, urlFoto, role, password, salt, tempCode, tempCodeCreateAt, id, dtReg, idPersReg, estado, intentos
                                           FROM MyUser 
                                           WHERE email = @email
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@email", email);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsUser
                        {
                            persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
                            intentos = reader.IsDBNull(reader.GetOrdinal("intentos")) ? 0 : reader.GetInt32(reader.GetOrdinal("intentos")),
                            email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                            urlFoto = reader.IsDBNull(reader.GetOrdinal("urlFoto")) ? null : reader.GetString(reader.GetOrdinal("urlFoto")),
                            role = reader.IsDBNull(reader.GetOrdinal("role")) ? null : reader.GetString(reader.GetOrdinal("role")),
                            password = reader.IsDBNull(reader.GetOrdinal("password")) ? null : reader.GetString(reader.GetOrdinal("password")),
                            salt = reader.IsDBNull(reader.GetOrdinal("salt")) ? null : reader.GetString(reader.GetOrdinal("salt")),
                            tempCode = reader.IsDBNull(reader.GetOrdinal("tempCode")) ? null : reader.GetString(reader.GetOrdinal("tempCode")),
                            tempCodeCreateAt = reader.IsDBNull(reader.GetOrdinal("tempCodeCreateAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("tempCodeCreateAt")),
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
