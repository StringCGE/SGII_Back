
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbRegistroFactura : DbSQLServer2022
    {
        public DbRegistroFactura() { }

        public async Task<int> CrearAsync(ClsRegistroFactura RegistroFactura)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO RegistroFactura (secuencial, razonSocial, identificacion, fechaEmision, numeroGuiaRemision, codigoNumerico, verificador, dtReg, idPersReg, estado)
                                 VALUES (@secuencial, @razonSocial, @identificacion, @fechaEmision, @numeroGuiaRemision, @codigoNumerico, @verificador, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@secuencial", (object)RegistroFactura.secuencial ?? DBNull.Value);
                        command.Parameters.AddWithValue("@razonSocial", (object)RegistroFactura.razonSocial ?? DBNull.Value);
                        command.Parameters.AddWithValue("@identificacion", (object)RegistroFactura.identificacion ?? DBNull.Value);
                        command.Parameters.AddWithValue("@fechaEmision", (object)RegistroFactura.fechaEmision ?? DBNull.Value);
                        command.Parameters.AddWithValue("@numeroGuiaRemision", (object)RegistroFactura.numeroGuiaRemision ?? DBNull.Value);
                        command.Parameters.AddWithValue("@codigoNumerico", (object)RegistroFactura.codigoNumerico ?? DBNull.Value);
                        command.Parameters.AddWithValue("@verificador", (object)RegistroFactura.verificador ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)RegistroFactura.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)RegistroFactura.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)RegistroFactura.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        RegistroFactura.id = Convert.ToInt32(result);
                        return (int)RegistroFactura.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsRegistroFactura RegistroFactura)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE RegistroFactura
                             SET secuencial = @secuencial, razonSocial = @razonSocial, identificacion = @identificacion, fechaEmision = @fechaEmision, numeroGuiaRemision = @numeroGuiaRemision, codigoNumerico = @codigoNumerico, verificador = @verificador, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@secuencial", (object)RegistroFactura.secuencial ?? DBNull.Value);
                    command.Parameters.AddWithValue("@razonSocial", (object)RegistroFactura.razonSocial ?? DBNull.Value);
                    command.Parameters.AddWithValue("@identificacion", (object)RegistroFactura.identificacion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@fechaEmision", (object)RegistroFactura.fechaEmision ?? DBNull.Value);
                    command.Parameters.AddWithValue("@numeroGuiaRemision", (object)RegistroFactura.numeroGuiaRemision ?? DBNull.Value);
                    command.Parameters.AddWithValue("@codigoNumerico", (object)RegistroFactura.codigoNumerico ?? DBNull.Value);
                    command.Parameters.AddWithValue("@verificador", (object)RegistroFactura.verificador ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)RegistroFactura.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)RegistroFactura.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)RegistroFactura.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)RegistroFactura.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM RegistroFactura WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsRegistroFactura>> ListarAsync(FetchDataRegistroFactura fetchData)
        {
            List<ClsRegistroFactura> RegistroFacturas = new List<ClsRegistroFactura>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) secuencial, razonSocial, identificacion, fechaEmision, numeroGuiaRemision, codigoNumerico, verificador, id, dtReg, idPersReg, estado
                                  FROM RegistroFactura
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
                            var RegistroFactura = new ClsRegistroFactura
                            {
                                secuencial = reader.IsDBNull(reader.GetOrdinal("secuencial")) ? null : reader.GetString(reader.GetOrdinal("secuencial")),
                                razonSocial = reader.IsDBNull(reader.GetOrdinal("razonSocial")) ? null : reader.GetString(reader.GetOrdinal("razonSocial")),
                                identificacion = reader.IsDBNull(reader.GetOrdinal("identificacion")) ? null : reader.GetString(reader.GetOrdinal("identificacion")),
                                fechaEmision = reader.IsDBNull(reader.GetOrdinal("fechaEmision")) ? null : reader.GetString(reader.GetOrdinal("fechaEmision")),
                                numeroGuiaRemision = reader.IsDBNull(reader.GetOrdinal("numeroGuiaRemision")) ? null : reader.GetString(reader.GetOrdinal("numeroGuiaRemision")),
                                codigoNumerico = reader.IsDBNull(reader.GetOrdinal("codigoNumerico")) ? null : reader.GetString(reader.GetOrdinal("codigoNumerico")),
                                verificador = reader.IsDBNull(reader.GetOrdinal("verificador")) ? null : reader.GetString(reader.GetOrdinal("verificador")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            RegistroFacturas.Add(RegistroFactura);
                        }
                    }
                }
            }
            return RegistroFacturas;
        }

        public async Task<ClsRegistroFactura> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT secuencial, razonSocial, identificacion, fechaEmision, numeroGuiaRemision, codigoNumerico, verificador, id, dtReg, idPersReg, estado
                                           FROM RegistroFactura 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsRegistroFactura
                        {
                                secuencial = reader.IsDBNull(reader.GetOrdinal("secuencial")) ? null : reader.GetString(reader.GetOrdinal("secuencial")),
                                razonSocial = reader.IsDBNull(reader.GetOrdinal("razonSocial")) ? null : reader.GetString(reader.GetOrdinal("razonSocial")),
                                identificacion = reader.IsDBNull(reader.GetOrdinal("identificacion")) ? null : reader.GetString(reader.GetOrdinal("identificacion")),
                                fechaEmision = reader.IsDBNull(reader.GetOrdinal("fechaEmision")) ? null : reader.GetString(reader.GetOrdinal("fechaEmision")),
                                numeroGuiaRemision = reader.IsDBNull(reader.GetOrdinal("numeroGuiaRemision")) ? null : reader.GetString(reader.GetOrdinal("numeroGuiaRemision")),
                                codigoNumerico = reader.IsDBNull(reader.GetOrdinal("codigoNumerico")) ? null : reader.GetString(reader.GetOrdinal("codigoNumerico")),
                                verificador = reader.IsDBNull(reader.GetOrdinal("verificador")) ? null : reader.GetString(reader.GetOrdinal("verificador")),
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
