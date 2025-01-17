using Dominio;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbColaborador : DbSQLServer2022
    {
        public DbColaborador() { }

        public async Task<int> CrearAsync(ClsColaborador Colaborador)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO Colaborador (dtReg, idPersReg, estado, persona_id, inicioAsignacion, finAsignacion, tipoColab_id, cargo_id, estadoLaboral_id, condicionLaboral_id, sueldo, horasTrabajo, contrato)
                                 VALUES (@dtReg, @idPersReg, @estado, @persona_id, @inicioAsignacion, @finAsignacion, @tipoColab_id, @cargo_id, @estadoLaboral_id, @condicionLaboral_id, @sueldo, @horasTrabajo, @contrato);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@dtReg", (object)Colaborador.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)Colaborador.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)Colaborador.estado ?? DBNull.Value);
                        command.Parameters.AddWithValue("@persona_id", (object)Colaborador.persona?.idApi ?? DBNull.Value);
                        command.Parameters.AddWithValue("@inicioAsignacion", (object)Colaborador.inicioAsignacion ?? DBNull.Value);
                        command.Parameters.AddWithValue("@finAsignacion", (object)Colaborador.finAsignacion ?? DBNull.Value);
                        command.Parameters.AddWithValue("@tipoColab_id", (object)Colaborador.tipoColab?.idApi ?? DBNull.Value);
                        command.Parameters.AddWithValue("@cargo_id", (object)Colaborador.cargo?.idApi ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estadoLaboral_id", (object)Colaborador.estadoLaboral?.idApi ?? DBNull.Value);
                        command.Parameters.AddWithValue("@condicionLaboral_id", (object)Colaborador.condicionLaboral?.idApi ?? DBNull.Value);
                        command.Parameters.AddWithValue("@sueldo", (object)Colaborador.sueldo ?? DBNull.Value);
                        command.Parameters.AddWithValue("@horasTrabajo", (object)Colaborador.horasTrabajo ?? DBNull.Value);
                        command.Parameters.AddWithValue("@contrato", (object)Colaborador.contrato ?? DBNull.Value);

                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        Colaborador.id = Convert.ToInt32(result);
                        return (int)Colaborador.id;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsColaborador Colaborador)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE Colaborador
                             SET dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado, 
                                 persona_id = @persona_id,
                                 inicioAsignacion = @inicioAsignacion, finAsignacion = @finAsignacion, 
                                 tipoColab_id = @tipoColab_id, cargo_id = @cargo_id, 
                                 estadoLaboral_id = @estadoLaboral_id, condicionLaboral_id = @condicionLaboral_id, 
                                 sueldo = @sueldo, horasTrabajo = @horasTrabajo, contrato = @contrato
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Colaborador.id);
                    command.Parameters.AddWithValue("@dtReg", Colaborador.dtReg);
                    command.Parameters.AddWithValue("@idPersReg", Colaborador.idPersReg);
                    command.Parameters.AddWithValue("@estado", Colaborador.estado);
                    command.Parameters.AddWithValue("@persona_id", Colaborador.persona?.idApi??0);
                    command.Parameters.AddWithValue("@inicioAsignacion", Colaborador.inicioAsignacion);
                    command.Parameters.AddWithValue("@finAsignacion", Colaborador.finAsignacion);
                    command.Parameters.AddWithValue("@tipoColab_id", Colaborador.tipoColab?.idApi ?? 0);
                    command.Parameters.AddWithValue("@cargo_id", Colaborador.cargo?.idApi ?? 0);
                    command.Parameters.AddWithValue("@estadoLaboral_id", Colaborador.estadoLaboral?.idApi ?? 0);
                    command.Parameters.AddWithValue("@condicionLaboral_id", Colaborador.condicionLaboral?.idApi ?? 0);
                    command.Parameters.AddWithValue("@sueldo", Colaborador.sueldo);
                    command.Parameters.AddWithValue("@horasTrabajo", Colaborador.horasTrabajo);
                    command.Parameters.AddWithValue("@contrato", Colaborador.contrato);

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
                string query = @"DELETE FROM Colaborador WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsColaborador>> ListarAsync(FetchDataColaborador fetchData)
        {
            List<ClsColaborador> Colaboradores = new List<ClsColaborador>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, persona_id, inicioAsignacion, finAsignacion, tipoColab_id, cargo_id, estadoLaboral_id, condicionLaboral_id, sueldo, horasTrabajo, contrato
                                  FROM Colaborador
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
                            var Colaborador = new ClsColaborador
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                                persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
                                //persona_id = reader.IsDBNull(reader.GetOrdinal("persona_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("persona_id")),
                                inicioAsignacion = reader.IsDBNull(reader.GetOrdinal("inicioAsignacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("inicioAsignacion")),
                                finAsignacion = reader.IsDBNull(reader.GetOrdinal("finAsignacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("finAsignacion")),
                                tipoColab = new ClsTipoColab { id = reader.GetInt32(reader.GetOrdinal("tipoColab_id")) },
                                //tipoColab_id = reader.IsDBNull(reader.GetOrdinal("tipoColab_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("tipoColab_id")),
                                cargo = new ClsCargo { id = reader.GetInt32(reader.GetOrdinal("cargo_id")) },
                                //cargo_id = reader.IsDBNull(reader.GetOrdinal("cargo_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cargo_id")),
                                estadoLaboral = new ClsEstadoLaboral { id = reader.GetInt32(reader.GetOrdinal("estadoLaboral_id")) },
                                //estadoLaboral_id = reader.IsDBNull(reader.GetOrdinal("estadoLaboral_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estadoLaboral_id")),
                                condicionLaboral = new ClsCondicionLaboral { id = reader.GetInt32(reader.GetOrdinal("condicionLaboral_id")) },
                                //condicionLaboral_id = reader.IsDBNull(reader.GetOrdinal("condicionLaboral_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("condicionLaboral_id")),
                                sueldo = reader.IsDBNull(reader.GetOrdinal("sueldo")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("sueldo")),
                                horasTrabajo = reader.IsDBNull(reader.GetOrdinal("horasTrabajo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("horasTrabajo")),
                                contrato = reader.IsDBNull(reader.GetOrdinal("contrato")) ? null : reader.GetString(reader.GetOrdinal("contrato"))
                            };
                            Colaboradores.Add(Colaborador);

                        }
                    }
                }
            }
            return Colaboradores;
        }

        public async Task<ClsColaborador> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, persona_id, inicioAsignacion, finAsignacion, tipoColab_id, cargo_id, estadoLaboral_id, condicionLaboral_id, sueldo, horasTrabajo, contrato 
                                           FROM Colaborador 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsColaborador
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                            idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                            estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
                            //persona_id = reader.IsDBNull(reader.GetOrdinal("persona_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("persona_id")),
                            inicioAsignacion = reader.IsDBNull(reader.GetOrdinal("inicioAsignacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("inicioAsignacion")),
                            finAsignacion = reader.IsDBNull(reader.GetOrdinal("finAsignacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("finAsignacion")),
                            tipoColab = new ClsTipoColab { id = reader.GetInt32(reader.GetOrdinal("tipoColab_id")) },
                            //tipoColab_id = reader.IsDBNull(reader.GetOrdinal("tipoColab_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("tipoColab_id")),
                            cargo = new ClsCargo { id = reader.GetInt32(reader.GetOrdinal("cargo_id")) },
                            //cargo_id = reader.IsDBNull(reader.GetOrdinal("cargo_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cargo_id")),
                            estadoLaboral = new ClsEstadoLaboral { id = reader.GetInt32(reader.GetOrdinal("estadoLaboral_id")) },
                            //estadoLaboral_id = reader.IsDBNull(reader.GetOrdinal("estadoLaboral_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estadoLaboral_id")),
                            condicionLaboral = new ClsCondicionLaboral { id = reader.GetInt32(reader.GetOrdinal("condicionLaboral_id")) },
                            //condicionLaboral_id = reader.IsDBNull(reader.GetOrdinal("condicionLaboral_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("condicionLaboral_id")),
                            sueldo = reader.IsDBNull(reader.GetOrdinal("sueldo")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("sueldo")),
                            horasTrabajo = reader.IsDBNull(reader.GetOrdinal("horasTrabajo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("horasTrabajo")),
                            contrato = reader.IsDBNull(reader.GetOrdinal("contrato")) ? null : reader.GetString(reader.GetOrdinal("contrato"))
                        };
                    }
                    return null;
                }
            }
        }
    }
}
