
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbItemFactura : DbSQLServer2022
    {
        public DbItemFactura() { }

        public async Task<int> CrearAsync(ClsItemFactura ItemFactura)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO ItemFactura (cantidad, producto_id, precioUnitario, total, dtReg, idPersReg, estado)
                                 VALUES (@cantidad, @producto_id, @precioUnitario, @total, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cantidad", (object)ItemFactura.cantidad ?? DBNull.Value);
                        command.Parameters.AddWithValue("@producto_id", ItemFactura.producto?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@precioUnitario", (object)ItemFactura.precioUnitario ?? DBNull.Value);
                        command.Parameters.AddWithValue("@total", (object)ItemFactura.total ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)ItemFactura.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)ItemFactura.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)ItemFactura.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        ItemFactura.id = Convert.ToInt32(result);
                        return (int)ItemFactura.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsItemFactura ItemFactura)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE ItemFactura
                             SET cantidad = @cantidad, producto_id = @producto_id, precioUnitario = @precioUnitario, total = @total, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cantidad", (object)ItemFactura.cantidad ?? DBNull.Value);
                    command.Parameters.AddWithValue("@producto_id", ItemFactura.producto?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@precioUnitario", (object)ItemFactura.precioUnitario ?? DBNull.Value);
                    command.Parameters.AddWithValue("@total", (object)ItemFactura.total ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)ItemFactura.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)ItemFactura.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)ItemFactura.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)ItemFactura.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM ItemFactura WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsItemFactura>> ListarAsync(FetchDataItemFactura fetchData)
        {
            List<ClsItemFactura> ItemFacturas = new List<ClsItemFactura>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) cantidad, producto_id, precioUnitario, total, id, dtReg, idPersReg, estado
                                  FROM ItemFactura
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
                            var ItemFactura = new ClsItemFactura
                            {
                                cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidad")),
                                producto = new ClsProducto { id = reader.GetInt32(reader.GetOrdinal("producto_id")) },
                                //producto_id = reader.GetInt32(reader.GetOrdinal("producto_id")),
//No soportado
//No soportado
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? 1: reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            ItemFacturas.Add(ItemFactura);
                        }
                    }
                }
            }
            return ItemFacturas;
        }

        public async Task<ClsItemFactura> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT cantidad, producto_id, precioUnitario, total, id, dtReg, idPersReg, estado
                                           FROM ItemFactura 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsItemFactura
                        {
                                cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidad")),
                                producto = new ClsProducto { id = reader.GetInt32(reader.GetOrdinal("producto_id")) },
                                //producto_id = reader.GetInt32(reader.GetOrdinal("producto_id")),
//No soportado
//No soportado
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
