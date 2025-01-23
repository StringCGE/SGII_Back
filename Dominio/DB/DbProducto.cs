
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbProducto : DbSQLServer2022
    {
        public DbProducto() { }

        public async Task<int> CrearAsync(ClsProducto Producto)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO Producto (proveedor_id, nombre, detalle, precio, cantidad, dtReg, idPersReg, estado)
                                 VALUES (@proveedor_id, @nombre, @detalle, @precio, @cantidad, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@proveedor_id", Producto.proveedor?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", (object)Producto.nombre ?? DBNull.Value);
                        command.Parameters.AddWithValue("@detalle", (object)Producto.detalle ?? DBNull.Value);
                        command.Parameters.AddWithValue("@precio", (object)Producto.precio ?? DBNull.Value);
                        command.Parameters.AddWithValue("@cantidad", (object)Producto.cantidad ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)Producto.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)Producto.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)Producto.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        Producto.id = Convert.ToInt32(result);
                        return (int)Producto.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsProducto Producto)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE Producto
                             SET proveedor_id = @proveedor_id, nombre = @nombre, detalle = @detalle, precio = @precio, cantidad = @cantidad, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@proveedor_id", Producto.proveedor?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombre", (object)Producto.nombre ?? DBNull.Value);
                    command.Parameters.AddWithValue("@detalle", (object)Producto.detalle ?? DBNull.Value);
                    command.Parameters.AddWithValue("@precio", (object)Producto.precio ?? DBNull.Value);
                    command.Parameters.AddWithValue("@cantidad", (object)Producto.cantidad ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)Producto.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)Producto.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)Producto.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)Producto.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM Producto WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsProducto>> ListarAsync(FetchDataProducto fetchData)
        {
            List<ClsProducto> Productos = new List<ClsProducto>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) proveedor_id, nombre, detalle, precio, cantidad, id, dtReg, idPersReg, estado
                                  FROM Producto
                                  WHERE dtReg < @offsetDT
                                  AND estado != 0");
                if (!string.IsNullOrWhiteSpace(fetchData.nombre)) queryBuilder.Append(" AND (nombre LIKE @nombre)");
                queryBuilder.Append(" ORDER BY dtReg DESC");

                string query = queryBuilder.ToString();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offsetDT", fetchData.offsetDT);
                    command.Parameters.AddWithValue("@take", fetchData.take);

                    if (!string.IsNullOrWhiteSpace(fetchData.nombre))command.Parameters.AddWithValue("@nombre", "%" + fetchData.nombre + "%");
                    
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Producto = new ClsProducto
                            {
                                proveedor = new ClsEmisor { id = reader.GetInt32(reader.GetOrdinal("proveedor_id")) },
                                //proveedor_id = reader.GetInt32(reader.GetOrdinal("proveedor_id")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre")),
                                detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
                                precio = reader.IsDBNull(reader.GetOrdinal("precio")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("precio")),
                                cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidad")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? 1: reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            Productos.Add(Producto);
                        }
                    }
                }
            }
            return Productos;
        }



        public async Task<List<ClsProducto>> GetAll()
        {
            List<ClsProducto> Productos = new List<ClsProducto>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"SELECT proveedor_id, nombre, detalle, precio, cantidad, id, dtReg, idPersReg, estado
                                  FROM Producto
                                  WHERE estado != 0");
                queryBuilder.Append(" ORDER BY dtReg DESC");

                string query = queryBuilder.ToString();

                using (var command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Producto = new ClsProducto
                            {
                                proveedor = new ClsEmisor { id = reader.GetInt32(reader.GetOrdinal("proveedor_id")) },
                                //proveedor_id = reader.GetInt32(reader.GetOrdinal("proveedor_id")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre")),
                                detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
                                precio = reader.IsDBNull(reader.GetOrdinal("precio")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("precio")),
                                cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidad")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? 1 : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            Productos.Add(Producto);
                        }
                    }
                }
            }
            return Productos;
        }


        public async Task<ClsProducto> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT proveedor_id, nombre, detalle, precio, cantidad, id, dtReg, idPersReg, estado
                                           FROM Producto 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsProducto
                        {
                                proveedor = new ClsEmisor { id = reader.GetInt32(reader.GetOrdinal("proveedor_id")) },
                                //proveedor_id = reader.GetInt32(reader.GetOrdinal("proveedor_id")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre")),
                                detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
                                precio = reader.IsDBNull(reader.GetOrdinal("precio")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("precio")),
                                cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidad")),
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
