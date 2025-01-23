
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbFactura : DbSQLServer2022
    {
        public DbFactura() { }

        public async Task<int> CrearAsync(ClsFactura Factura)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"INSERT INTO Factura (emisor_id, cliente_id, registroFactura_id, claveAcceso, autorizacion, subtotalPrevio, subtotal0, descuento, subtotal, iva, total, pagoEfectivo, pagoTarjetaDebCred, pagoOtraForma, pagoOtraFormaDetalle, dtReg, idPersReg, estado)
                                 VALUES (@emisor_id, @cliente_id, @registroFactura_id, @claveAcceso, @autorizacion, @subtotalPrevio, @subtotal0, @descuento, @subtotal, @iva, @total, @pagoEfectivo, @pagoTarjetaDebCred, @pagoOtraForma, @pagoOtraFormaDetalle, @dtReg, @idPersReg, @estado);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@emisor_id", Factura.emisor?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@cliente_id", Factura.cliente?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@registroFactura_id", Factura.registroFactura?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@claveAcceso", (object)Factura.claveAcceso ?? DBNull.Value);
                        command.Parameters.AddWithValue("@autorizacion", (object)Factura.autorizacion ?? DBNull.Value);
                        command.Parameters.AddWithValue("@subtotalPrevio", (object)Factura.subtotalPrevio ?? DBNull.Value);
                        command.Parameters.AddWithValue("@subtotal0", (object)Factura.subtotal0 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@descuento", (object)Factura.descuento ?? DBNull.Value);
                        command.Parameters.AddWithValue("@subtotal", (object)Factura.subtotal ?? DBNull.Value);
                        command.Parameters.AddWithValue("@iva", (object)Factura.iva ?? DBNull.Value);
                        command.Parameters.AddWithValue("@total", (object)Factura.total ?? DBNull.Value);
                        command.Parameters.AddWithValue("@pagoEfectivo", (object)Factura.pagoEfectivo ?? DBNull.Value);
                        command.Parameters.AddWithValue("@pagoTarjetaDebCred", (object)Factura.pagoTarjetaDebCred ?? DBNull.Value);
                        command.Parameters.AddWithValue("@pagoOtraForma", (object)Factura.pagoOtraForma ?? DBNull.Value);
                        command.Parameters.AddWithValue("@pagoOtraFormaDetalle", (object)Factura.pagoOtraFormaDetalle ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", (object)Factura.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)Factura.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)Factura.estado ?? DBNull.Value);
                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        Factura.id = Convert.ToInt32(result);
                        return (int)Factura.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(ClsFactura Factura)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE Factura
                             SET emisor_id = @emisor_id, cliente_id = @cliente_id, registroFactura_id = @registroFactura_id, claveAcceso = @claveAcceso, autorizacion = @autorizacion, subtotalPrevio = @subtotalPrevio, subtotal0 = @subtotal0, descuento = @descuento, subtotal = @subtotal, iva = @iva, total = @total, pagoEfectivo = @pagoEfectivo, pagoTarjetaDebCred = @pagoTarjetaDebCred, pagoOtraForma = @pagoOtraForma, pagoOtraFormaDetalle = @pagoOtraFormaDetalle, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@emisor_id", Factura.emisor?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@cliente_id", Factura.cliente?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@registroFactura_id", Factura.registroFactura?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@claveAcceso", (object)Factura.claveAcceso ?? DBNull.Value);
                    command.Parameters.AddWithValue("@autorizacion", (object)Factura.autorizacion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@subtotalPrevio", (object)Factura.subtotalPrevio ?? DBNull.Value);
                    command.Parameters.AddWithValue("@subtotal0", (object)Factura.subtotal0 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@descuento", (object)Factura.descuento ?? DBNull.Value);
                    command.Parameters.AddWithValue("@subtotal", (object)Factura.subtotal ?? DBNull.Value);
                    command.Parameters.AddWithValue("@iva", (object)Factura.iva ?? DBNull.Value);
                    command.Parameters.AddWithValue("@total", (object)Factura.total ?? DBNull.Value);
                    command.Parameters.AddWithValue("@pagoEfectivo", (object)Factura.pagoEfectivo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@pagoTarjetaDebCred", (object)Factura.pagoTarjetaDebCred ?? DBNull.Value);
                    command.Parameters.AddWithValue("@pagoOtraForma", (object)Factura.pagoOtraForma ?? DBNull.Value);
                    command.Parameters.AddWithValue("@pagoOtraFormaDetalle", (object)Factura.pagoOtraFormaDetalle ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)Factura.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)Factura.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)Factura.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)Factura.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM Factura WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsFactura>> ListarAsync(FetchDataFactura fetchData)
        {
            List<ClsFactura> Facturas = new List<ClsFactura>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) emisor_id, cliente_id, registroFactura_id, claveAcceso, autorizacion, subtotalPrevio, subtotal0, descuento, subtotal, iva, total, pagoEfectivo, pagoTarjetaDebCred, pagoOtraForma, pagoOtraFormaDetalle, id, dtReg, idPersReg, estado
                                  FROM Factura
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
                            var Factura = new ClsFactura
                            {
                                emisor = new ClsEmisorItem { id = reader.GetInt32(reader.GetOrdinal("emisor_id")) },
                                //emisor_id = reader.GetInt32(reader.GetOrdinal("emisor_id")),
                                cliente = new ClsCliente { id = reader.GetInt32(reader.GetOrdinal("cliente_id")) },
                                //cliente_id = reader.GetInt32(reader.GetOrdinal("cliente_id")),
                                registroFactura = new ClsRegistroFactura { id = reader.GetInt32(reader.GetOrdinal("registroFactura_id")) },
                                //registroFactura_id = reader.GetInt32(reader.GetOrdinal("registroFactura_id")),
                                claveAcceso = reader.IsDBNull(reader.GetOrdinal("claveAcceso")) ? null : reader.GetString(reader.GetOrdinal("claveAcceso")),
                                autorizacion = reader.IsDBNull(reader.GetOrdinal("autorizacion")) ? null : reader.GetString(reader.GetOrdinal("autorizacion")),
//No soportado
//No soportado
//No soportado
//No soportado
//No soportado
//No soportado
//No soportado
//No soportado
//No soportado
                                pagoOtraFormaDetalle = reader.IsDBNull(reader.GetOrdinal("pagoOtraFormaDetalle")) ? null : reader.GetString(reader.GetOrdinal("pagoOtraFormaDetalle")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            Facturas.Add(Factura);
                        }
                    }
                }
            }
            return Facturas;
        }

        public async Task<ClsFactura> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT emisor_id, cliente_id, registroFactura_id, claveAcceso, autorizacion, subtotalPrevio, subtotal0, descuento, subtotal, iva, total, pagoEfectivo, pagoTarjetaDebCred, pagoOtraForma, pagoOtraFormaDetalle, id, dtReg, idPersReg, estado
                                           FROM Factura 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsFactura
                        {
                                emisor = new ClsEmisorItem { id = reader.GetInt32(reader.GetOrdinal("emisor_id")) },
                                //emisor_id = reader.GetInt32(reader.GetOrdinal("emisor_id")),
                                cliente = new ClsCliente { id = reader.GetInt32(reader.GetOrdinal("cliente_id")) },
                                //cliente_id = reader.GetInt32(reader.GetOrdinal("cliente_id")),
                                registroFactura = new ClsRegistroFactura { id = reader.GetInt32(reader.GetOrdinal("registroFactura_id")) },
                                //registroFactura_id = reader.GetInt32(reader.GetOrdinal("registroFactura_id")),
                                claveAcceso = reader.IsDBNull(reader.GetOrdinal("claveAcceso")) ? null : reader.GetString(reader.GetOrdinal("claveAcceso")),
                                autorizacion = reader.IsDBNull(reader.GetOrdinal("autorizacion")) ? null : reader.GetString(reader.GetOrdinal("autorizacion")),
//No soportado
//No soportado
//No soportado
//No soportado
//No soportado
//No soportado
//No soportado
//No soportado
//No soportado
                                pagoOtraFormaDetalle = reader.IsDBNull(reader.GetOrdinal("pagoOtraFormaDetalle")) ? null : reader.GetString(reader.GetOrdinal("pagoOtraFormaDetalle")),
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
