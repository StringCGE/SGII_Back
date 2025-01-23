
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbItemFacturaNotaCredito : DbSQLServer2022
    {
        public DbItemFacturaNotaCredito() { }

        public async Task<int> CrearAsync(ClsItemFacturaNotaCredito ItemFacturaNotaCredito)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO ItemFacturaNotaCredito (facturaNotaCredito_id, cantidad, producto_id, precioUnitario, total, tipoTransac, dtReg, idPersReg, estado)
                                 VALUES (@facturaNotaCredito_id, @cantidad, @producto_id, @precioUnitario, @total, @tipoTransac, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@facturaNotaCredito_id", ItemFacturaNotaCredito.facturaNotaCredito?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@cantidad", (object)ItemFacturaNotaCredito.cantidad ?? DBNull.Value);
                        command.Parameters.AddWithValue("@producto_id", ItemFacturaNotaCredito.producto?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@precioUnitario", (object)ItemFacturaNotaCredito.precioUnitario ?? DBNull.Value);
                        command.Parameters.AddWithValue("@total", (object)ItemFacturaNotaCredito.total ?? DBNull.Value);
                        command.Parameters.AddWithValue("@tipoTransac", (object)ItemFacturaNotaCredito.tipoTransac ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)ItemFacturaNotaCredito.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)ItemFacturaNotaCredito.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)ItemFacturaNotaCredito.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        ItemFacturaNotaCredito.id = Convert.ToInt32(result);
                        return (int)ItemFacturaNotaCredito.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsItemFacturaNotaCredito ItemFacturaNotaCredito)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE ItemFacturaNotaCredito
                             SET facturaNotaCredito_id = @facturaNotaCredito_id, cantidad = @cantidad, producto_id = @producto_id, precioUnitario = @precioUnitario, total = @total, tipoTransac = @tipoTransac, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@facturaNotaCredito_id", ItemFacturaNotaCredito.facturaNotaCredito?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@cantidad", (object)ItemFacturaNotaCredito.cantidad ?? DBNull.Value);
                    command.Parameters.AddWithValue("@producto_id", ItemFacturaNotaCredito.producto?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@precioUnitario", (object)ItemFacturaNotaCredito.precioUnitario ?? DBNull.Value);
                    command.Parameters.AddWithValue("@total", (object)ItemFacturaNotaCredito.total ?? DBNull.Value);
                    command.Parameters.AddWithValue("@tipoTransac", (object)ItemFacturaNotaCredito.tipoTransac ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)ItemFacturaNotaCredito.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)ItemFacturaNotaCredito.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)ItemFacturaNotaCredito.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)ItemFacturaNotaCredito.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM ItemFacturaNotaCredito WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsItemFacturaNotaCredito>> ListarAsync(FetchDataItemFacturaNotaCredito fetchData)
        {
            List<ClsItemFacturaNotaCredito> ItemFacturaNotaCreditos = new List<ClsItemFacturaNotaCredito>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) facturaNotaCredito_id, cantidad, producto_id, precioUnitario, total, tipoTransac, id, dtReg, idPersReg, estado
                                  FROM ItemFacturaNotaCredito
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
                            var ItemFacturaNotaCredito = new ClsItemFacturaNotaCredito
                            {
                                facturaNotaCredito = new ClsFacturaNotaCredito { id = reader.GetInt32(reader.GetOrdinal("facturaNotaCredito_id")) },
                                //facturaNotaCredito_id = reader.GetInt32(reader.GetOrdinal("facturaNotaCredito_id")),
                                cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidad")),
                                producto = new ClsProducto { id = reader.GetInt32(reader.GetOrdinal("producto_id")) },
                                //producto_id = reader.GetInt32(reader.GetOrdinal("producto_id")),
                                precioUnitario = reader.IsDBNull(reader.GetOrdinal("precioUnitario")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("precioUnitario")),
                                total = reader.IsDBNull(reader.GetOrdinal("total")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("total")),
                                tipoTransac = reader.IsDBNull(reader.GetOrdinal("tipoTransac")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("tipoTransac")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            ItemFacturaNotaCreditos.Add(ItemFacturaNotaCredito);
                        }
                    }
                }
            }
            return ItemFacturaNotaCreditos;
        }

        public async Task<ClsItemFacturaNotaCredito> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT facturaNotaCredito_id, cantidad, producto_id, precioUnitario, total, tipoTransac, id, dtReg, idPersReg, estado
                                           FROM ItemFacturaNotaCredito 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsItemFacturaNotaCredito
                        {
                                facturaNotaCredito = new ClsFacturaNotaCredito { id = reader.GetInt32(reader.GetOrdinal("facturaNotaCredito_id")) },
                                //facturaNotaCredito_id = reader.GetInt32(reader.GetOrdinal("facturaNotaCredito_id")),
                                cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidad")),
                                producto = new ClsProducto { id = reader.GetInt32(reader.GetOrdinal("producto_id")) },
                                //producto_id = reader.GetInt32(reader.GetOrdinal("producto_id")),
                                precioUnitario = reader.IsDBNull(reader.GetOrdinal("precioUnitario")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("precioUnitario")),
                                total = reader.IsDBNull(reader.GetOrdinal("total")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("total")),
                                tipoTransac = reader.IsDBNull(reader.GetOrdinal("tipoTransac")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("tipoTransac")),
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
