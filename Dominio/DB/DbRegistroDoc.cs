
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbRegistroDoc : DbSQLServer2022
    {
        public DbRegistroDoc() { }

        public async Task<int> CrearAsync(ClsRegistroDoc RegistroDoc)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO RegistroDoc (secuencial, razonSocial, identificacion, fechaEmision, numeroGuiaRemision, codigoNumerico, verificador, denomComproModif, numComproModif, comproModif, dtReg, idPersReg, estado)
                                 VALUES (@secuencial, @razonSocial, @identificacion, @fechaEmision, @numeroGuiaRemision, @codigoNumerico, @verificador, @denomComproModif, @numComproModif, @comproModif, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@secuencial", (object)RegistroDoc.secuencial ?? DBNull.Value);
                        command.Parameters.AddWithValue("@razonSocial", (object)RegistroDoc.razonSocial ?? DBNull.Value);
                        command.Parameters.AddWithValue("@identificacion", (object)RegistroDoc.identificacion ?? DBNull.Value);
                        command.Parameters.AddWithValue("@fechaEmision", (object)RegistroDoc.fechaEmision ?? DBNull.Value);
                        command.Parameters.AddWithValue("@numeroGuiaRemision", (object)RegistroDoc.numeroGuiaRemision ?? DBNull.Value);
                        command.Parameters.AddWithValue("@codigoNumerico", (object)RegistroDoc.codigoNumerico ?? DBNull.Value);
                        command.Parameters.AddWithValue("@verificador", (object)RegistroDoc.verificador ?? DBNull.Value);
                        command.Parameters.AddWithValue("@denomComproModif", (object)RegistroDoc.denomComproModif ?? DBNull.Value);
                        command.Parameters.AddWithValue("@numComproModif", (object)RegistroDoc.numComproModif ?? DBNull.Value);
                        command.Parameters.AddWithValue("@comproModif", (object)RegistroDoc.comproModif ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)RegistroDoc.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)RegistroDoc.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)RegistroDoc.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        RegistroDoc.id = Convert.ToInt32(result);
                        return (int)RegistroDoc.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsRegistroDoc RegistroDoc)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE RegistroDoc
                             SET secuencial = @secuencial, razonSocial = @razonSocial, identificacion = @identificacion, fechaEmision = @fechaEmision, numeroGuiaRemision = @numeroGuiaRemision, codigoNumerico = @codigoNumerico, verificador = @verificador, denomComproModif = @denomComproModif, numComproModif = @numComproModif, comproModif = @comproModif, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@secuencial", (object)RegistroDoc.secuencial ?? DBNull.Value);
                    command.Parameters.AddWithValue("@razonSocial", (object)RegistroDoc.razonSocial ?? DBNull.Value);
                    command.Parameters.AddWithValue("@identificacion", (object)RegistroDoc.identificacion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@fechaEmision", (object)RegistroDoc.fechaEmision ?? DBNull.Value);
                    command.Parameters.AddWithValue("@numeroGuiaRemision", (object)RegistroDoc.numeroGuiaRemision ?? DBNull.Value);
                    command.Parameters.AddWithValue("@codigoNumerico", (object)RegistroDoc.codigoNumerico ?? DBNull.Value);
                    command.Parameters.AddWithValue("@verificador", (object)RegistroDoc.verificador ?? DBNull.Value);
                    command.Parameters.AddWithValue("@denomComproModif", (object)RegistroDoc.denomComproModif ?? DBNull.Value);
                    command.Parameters.AddWithValue("@numComproModif", (object)RegistroDoc.numComproModif ?? DBNull.Value);
                    command.Parameters.AddWithValue("@comproModif", (object)RegistroDoc.comproModif ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)RegistroDoc.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)RegistroDoc.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)RegistroDoc.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)RegistroDoc.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM RegistroDoc WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsRegistroDoc>> ListarAsync(FetchDataRegistroDoc fetchData)
        {
            List<ClsRegistroDoc> RegistroDocs = new List<ClsRegistroDoc>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) secuencial, razonSocial, identificacion, fechaEmision, numeroGuiaRemision, codigoNumerico, verificador, denomComproModif, numComproModif, comproModif, id, dtReg, idPersReg, estado
                                  FROM RegistroDoc
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
                            var RegistroDoc = new ClsRegistroDoc
                            {
                                secuencial = reader.IsDBNull(reader.GetOrdinal("secuencial")) ? null : reader.GetString(reader.GetOrdinal("secuencial")),
                                razonSocial = reader.IsDBNull(reader.GetOrdinal("razonSocial")) ? null : reader.GetString(reader.GetOrdinal("razonSocial")),
                                identificacion = reader.IsDBNull(reader.GetOrdinal("identificacion")) ? null : reader.GetString(reader.GetOrdinal("identificacion")),
                                fechaEmision = reader.IsDBNull(reader.GetOrdinal("fechaEmision")) ? null : reader.GetString(reader.GetOrdinal("fechaEmision")),
                                numeroGuiaRemision = reader.IsDBNull(reader.GetOrdinal("numeroGuiaRemision")) ? null : reader.GetString(reader.GetOrdinal("numeroGuiaRemision")),
                                codigoNumerico = reader.IsDBNull(reader.GetOrdinal("codigoNumerico")) ? null : reader.GetString(reader.GetOrdinal("codigoNumerico")),
                                verificador = reader.IsDBNull(reader.GetOrdinal("verificador")) ? null : reader.GetString(reader.GetOrdinal("verificador")),
                                denomComproModif = reader.IsDBNull(reader.GetOrdinal("denomComproModif")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("denomComproModif")),
                                numComproModif = reader.IsDBNull(reader.GetOrdinal("numComproModif")) ? null : reader.GetString(reader.GetOrdinal("numComproModif")),
                                comproModif = reader.IsDBNull(reader.GetOrdinal("comproModif")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("comproModif")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? 1 : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            RegistroDocs.Add(RegistroDoc);
                        }
                    }
                }
            }
            return RegistroDocs;
        }

        public async Task<ClsRegistroDoc> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT secuencial, razonSocial, identificacion, fechaEmision, numeroGuiaRemision, codigoNumerico, verificador, denomComproModif, numComproModif, comproModif, id, dtReg, idPersReg, estado
                                           FROM RegistroDoc 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsRegistroDoc
                        {
                                secuencial = reader.IsDBNull(reader.GetOrdinal("secuencial")) ? null : reader.GetString(reader.GetOrdinal("secuencial")),
                                razonSocial = reader.IsDBNull(reader.GetOrdinal("razonSocial")) ? null : reader.GetString(reader.GetOrdinal("razonSocial")),
                                identificacion = reader.IsDBNull(reader.GetOrdinal("identificacion")) ? null : reader.GetString(reader.GetOrdinal("identificacion")),
                                fechaEmision = reader.IsDBNull(reader.GetOrdinal("fechaEmision")) ? null : reader.GetString(reader.GetOrdinal("fechaEmision")),
                                numeroGuiaRemision = reader.IsDBNull(reader.GetOrdinal("numeroGuiaRemision")) ? null : reader.GetString(reader.GetOrdinal("numeroGuiaRemision")),
                                codigoNumerico = reader.IsDBNull(reader.GetOrdinal("codigoNumerico")) ? null : reader.GetString(reader.GetOrdinal("codigoNumerico")),
                                verificador = reader.IsDBNull(reader.GetOrdinal("verificador")) ? null : reader.GetString(reader.GetOrdinal("verificador")),
                                denomComproModif = reader.IsDBNull(reader.GetOrdinal("denomComproModif")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("denomComproModif")),
                                numComproModif = reader.IsDBNull(reader.GetOrdinal("numComproModif")) ? null : reader.GetString(reader.GetOrdinal("numComproModif")),
                                comproModif = reader.IsDBNull(reader.GetOrdinal("comproModif")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("comproModif")),
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
