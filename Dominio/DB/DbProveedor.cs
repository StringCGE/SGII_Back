
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbProveedor : DbSQLServer2022
    {
        public DbProveedor() { }

        public async Task<int> CrearAsync(ClsProveedor Proveedor)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO Proveedor (razonSocial, ruc, responsable_id, telefonoResponsable, direccionMatriz, telefono1, telefono2, email, dtReg, idPersReg, estado)
                                 VALUES (@razonSocial, @ruc, @responsable_id, @telefonoResponsable, @direccionMatriz, @telefono1, @telefono2, @email, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@razonSocial", (object)Proveedor.razonSocial ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ruc", (object)Proveedor.ruc ?? DBNull.Value);
                        command.Parameters.AddWithValue("@responsable_id", Proveedor.responsable?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@telefonoResponsable", (object)Proveedor.telefonoResponsable ?? DBNull.Value);
                        command.Parameters.AddWithValue("@direccionMatriz", (object)Proveedor.direccionMatriz ?? DBNull.Value);
                        command.Parameters.AddWithValue("@telefono1", (object)Proveedor.telefono1 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@telefono2", (object)Proveedor.telefono2 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@email", (object)Proveedor.email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)Proveedor.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)Proveedor.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)Proveedor.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        Proveedor.id = Convert.ToInt32(result);
                        return (int)Proveedor.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsProveedor Proveedor)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE Proveedor
                             SET razonSocial = @razonSocial, ruc = @ruc, responsable_id = @responsable_id, telefonoResponsable = @telefonoResponsable, direccionMatriz = @direccionMatriz, telefono1 = @telefono1, telefono2 = @telefono2, email = @email, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@razonSocial", (object)Proveedor.razonSocial ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ruc", (object)Proveedor.ruc ?? DBNull.Value);
                    command.Parameters.AddWithValue("@responsable_id", Proveedor.responsable?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefonoResponsable", (object)Proveedor.telefonoResponsable ?? DBNull.Value);
                    command.Parameters.AddWithValue("@direccionMatriz", (object)Proveedor.direccionMatriz ?? DBNull.Value);
                    command.Parameters.AddWithValue("@telefono1", (object)Proveedor.telefono1 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@telefono2", (object)Proveedor.telefono2 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@email", (object)Proveedor.email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)Proveedor.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)Proveedor.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)Proveedor.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)Proveedor.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM Proveedor WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsProveedor>> ListarAsync(FetchDataProveedor fetchData)
        {
            List<ClsProveedor> Proveedors = new List<ClsProveedor>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) razonSocial, ruc, responsable_id, telefonoResponsable, direccionMatriz, telefono1, telefono2, email, id, dtReg, idPersReg, estado
                                  FROM Proveedor
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
                            var Proveedor = new ClsProveedor
                            {
                                razonSocial = reader.IsDBNull(reader.GetOrdinal("razonSocial")) ? null : reader.GetString(reader.GetOrdinal("razonSocial")),
                                ruc = reader.IsDBNull(reader.GetOrdinal("ruc")) ? null : reader.GetString(reader.GetOrdinal("ruc")),
                                responsable = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("responsable_id")) },
                                //responsable_id = reader.GetInt32(reader.GetOrdinal("responsable_id")),
                                telefonoResponsable = reader.IsDBNull(reader.GetOrdinal("telefonoResponsable")) ? null : reader.GetString(reader.GetOrdinal("telefonoResponsable")),
                                direccionMatriz = reader.IsDBNull(reader.GetOrdinal("direccionMatriz")) ? null : reader.GetString(reader.GetOrdinal("direccionMatriz")),
                                telefono1 = reader.IsDBNull(reader.GetOrdinal("telefono1")) ? null : reader.GetString(reader.GetOrdinal("telefono1")),
                                telefono2 = reader.IsDBNull(reader.GetOrdinal("telefono2")) ? null : reader.GetString(reader.GetOrdinal("telefono2")),
                                email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            Proveedors.Add(Proveedor);
                        }
                    }
                }
            }
            return Proveedors;
        }

        public async Task<ClsProveedor> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT razonSocial, ruc, responsable_id, telefonoResponsable, direccionMatriz, telefono1, telefono2, email, id, dtReg, idPersReg, estado
                                           FROM Proveedor 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsProveedor
                        {
                                razonSocial = reader.IsDBNull(reader.GetOrdinal("razonSocial")) ? null : reader.GetString(reader.GetOrdinal("razonSocial")),
                                ruc = reader.IsDBNull(reader.GetOrdinal("ruc")) ? null : reader.GetString(reader.GetOrdinal("ruc")),
                                responsable = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("responsable_id")) },
                                //responsable_id = reader.GetInt32(reader.GetOrdinal("responsable_id")),
                                telefonoResponsable = reader.IsDBNull(reader.GetOrdinal("telefonoResponsable")) ? null : reader.GetString(reader.GetOrdinal("telefonoResponsable")),
                                direccionMatriz = reader.IsDBNull(reader.GetOrdinal("direccionMatriz")) ? null : reader.GetString(reader.GetOrdinal("direccionMatriz")),
                                telefono1 = reader.IsDBNull(reader.GetOrdinal("telefono1")) ? null : reader.GetString(reader.GetOrdinal("telefono1")),
                                telefono2 = reader.IsDBNull(reader.GetOrdinal("telefono2")) ? null : reader.GetString(reader.GetOrdinal("telefono2")),
                                email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
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
