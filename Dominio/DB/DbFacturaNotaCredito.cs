
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbFacturaNotaCredito : DbSQLServer2022
    {
        public DbFacturaNotaCredito() { }


        public async Task<int> CrearAsync(ClsFacturaNotaCredito factura)
        {
            int result = -1;
            string errorMessage = "";
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"RegistrarFactura";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Crear DataTable para ItemFactura
                        var itemFacturaTable = new DataTable();
                        itemFacturaTable.Columns.Add("cantidad", typeof(int));
                        itemFacturaTable.Columns.Add("producto_id", typeof(int));
                        itemFacturaTable.Columns.Add("precioUnitario", typeof(double));
                        itemFacturaTable.Columns.Add("total", typeof(double));
                        itemFacturaTable.Columns.Add("dtReg", typeof(DateTime));
                        itemFacturaTable.Columns.Add("idPersReg", typeof(int));
                        itemFacturaTable.Columns.Add("estado", typeof(int));

                        foreach (var item in factura.lItem)
                        {
                            itemFacturaTable.Rows.Add(
                                item.cantidad,
                                item.producto?.idApi,
                                item.precioUnitario,
                                item.total,
                                item.dtReg ?? DateTime.Now,
                                item.idPersReg,
                                item.estado
                            );
                        }

                        command.Parameters.AddWithValue("@denomComproModif", factura.registroFactura?.denomComproModif ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@numComproModif", factura.registroFactura?.numComproModif ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@comproModif", factura.registroFactura?.comproModif ?? (object)DBNull.Value);

                        // Agregar parámetros al comando
                        command.Parameters.AddWithValue("@emisor_id", factura.emisor?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@registroFactura_id", factura.registroFactura?.id ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@cliente_id", factura.cliente?.idApi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@claveAcceso", factura.claveAcceso ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@esFactura", factura.esFactura);
                        command.Parameters.AddWithValue("@autorizacion", factura.autorizacion ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@subtotalPrevio", factura.subtotalPrevio ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@subtotal0", factura.subtotal0 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@descuento", factura.descuento ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@subtotal", factura.subtotal ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@iva", factura.iva ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@total", factura.total ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@pagoEfectivo", factura.pagoEfectivo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@pagoTarjetaDebCred", factura.pagoTarjetaDebCred ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@pagoOtraForma", factura.pagoOtraForma ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@pagoOtraFormaDetalle", factura.pagoOtraFormaDetalle ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@secuencial", factura.registroFactura?.secuencial ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@razonSocial", factura.registroFactura?.razonSocial ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@identificacion", factura.registroFactura?.identificacion ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@fechaEmision", factura.registroFactura?.fechaEmision ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@numeroGuiaRemision", factura.registroFactura?.numeroGuiaRemision ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@codigoNumerico", factura.registroFactura?.codigoNumerico ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@verificador", factura.registroFactura?.verificador ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", factura.dtReg ?? DateTime.Now);
                        command.Parameters.AddWithValue("@idPersReg", factura.idPersReg ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@estado", factura.estado ?? (object)DBNull.Value);

                        // Parámetro para tabla de elementos
                        var itemParam = command.Parameters.AddWithValue("@ItemFacturaTemp", itemFacturaTable);
                        itemParam.SqlDbType = SqlDbType.Structured;

                        // Parámetros de salida
                        var resultParam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(resultParam);

                        var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 4000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(errorMessageParam);

                        connection.Open();
                        command.ExecuteNonQuery();
                        result = (int)resultParam.Value;
                        errorMessage = errorMessageParam.Value as string;
                        if (result == 1)
                        {
                            result = 1;
                        }
                        else
                        {
                            result = -1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result = -1;
            }
            return result;
        }

        public async Task<int> CrearAsync2(ClsFacturaNotaCredito FacturaNotaCredito)
        {
            int result = -1;
            string errorMessage = "";
            try
            {
                using (var connection = GetConnection())
                {
                    string query = @"RegistrarFactura";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de FacturaNotaCredito
                        command.Parameters.AddWithValue("@emisor_id", 1);
                        command.Parameters.AddWithValue("@registroFactura_id", 2);
                        command.Parameters.AddWithValue("@cliente_id", 3);
                        command.Parameters.AddWithValue("@claveAcceso", "ABC123");
                        command.Parameters.AddWithValue("@esFactura", true);
                        command.Parameters.AddWithValue("@autorizacion", "XYZ456");
                        command.Parameters.AddWithValue("@subtotalPrevio", 100);
                        command.Parameters.AddWithValue("@subtotal0", 0);
                        command.Parameters.AddWithValue("@descuento", 10);
                        command.Parameters.AddWithValue("@subtotal", 90);
                        command.Parameters.AddWithValue("@iva", 12);
                        command.Parameters.AddWithValue("@total", 102);
                        command.Parameters.AddWithValue("@pagoEfectivo", 50);
                        command.Parameters.AddWithValue("@pagoTarjetaDebCred", 52);
                        command.Parameters.AddWithValue("@pagoOtraForma", 0);
                        command.Parameters.AddWithValue("@pagoOtraFormaDetalle", DBNull.Value);
                        command.Parameters.AddWithValue("@dtReg", DateTime.Now);
                        command.Parameters.AddWithValue("@idPersReg", 1);
                        command.Parameters.AddWithValue("@estado", 1);

                        // Crear DataTable para ítems de factura
                        DataTable itemFacturaTable = new DataTable();
                        itemFacturaTable.Columns.Add("cantidad", typeof(int));
                        itemFacturaTable.Columns.Add("producto_id", typeof(int));
                        itemFacturaTable.Columns.Add("precioUnitario", typeof(decimal));
                        itemFacturaTable.Columns.Add("total", typeof(decimal));
                        itemFacturaTable.Columns.Add("dtReg", typeof(DateTime));
                        itemFacturaTable.Columns.Add("idPersReg", typeof(int));
                        itemFacturaTable.Columns.Add("estado", typeof(string));

                        // Agregar datos de ejemplo
                        itemFacturaTable.Rows.Add(2, 101, 20.00m, 40.00m, DateTime.Now, 1, "Activo");
                        itemFacturaTable.Rows.Add(1, 102, 50.00m, 50.00m, DateTime.Now, 1, "Activo");

                        // Agregar parámetro de tipo tabla
                        SqlParameter itemFacturaParam = command.Parameters.AddWithValue("@ItemFacturaTemp", itemFacturaTable);
                        itemFacturaParam.SqlDbType = SqlDbType.Structured;

                        // Agregar parámetros de salida
                        SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(resultParam);

                        SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(errorMessageParam);

                        connection.Open();
                        command.ExecuteNonQuery();
                        result = (int)resultParam.Value;
                        errorMessage = errorMessageParam.Value as string;
                        if (result == 1)
                        {
                            result = 1;
                        }
                        else
                        {
                            result = -1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result = - 1;
            }
            return result;
        }

        public async Task<bool> EditarAsync(ClsFacturaNotaCredito FacturaNotaCredito)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE FacturaNotaCredito
                             SET emisor_id = @emisor_id, registroFactura_id = @registroFactura_id, cliente_id = @cliente_id, claveAcceso = @claveAcceso, esFactura = @esFactura, autorizacion = @autorizacion, subtotalPrevio = @subtotalPrevio, subtotal0 = @subtotal0, descuento = @descuento, subtotal = @subtotal, iva = @iva, total = @total, pagoEfectivo = @pagoEfectivo, pagoTarjetaDebCred = @pagoTarjetaDebCred, pagoOtraForma = @pagoOtraForma, pagoOtraFormaDetalle = @pagoOtraFormaDetalle, dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@emisor_id", FacturaNotaCredito.emisor?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@registroFactura_id", FacturaNotaCredito.registroFactura?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@cliente_id", FacturaNotaCredito.cliente?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@claveAcceso", (object)FacturaNotaCredito.claveAcceso ?? DBNull.Value);
                    command.Parameters.AddWithValue("@esFactura", (object)FacturaNotaCredito.esFactura ?? DBNull.Value);
                    command.Parameters.AddWithValue("@autorizacion", (object)FacturaNotaCredito.autorizacion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@subtotalPrevio", (object)FacturaNotaCredito.subtotalPrevio ?? DBNull.Value);
                    command.Parameters.AddWithValue("@subtotal0", (object)FacturaNotaCredito.subtotal0 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@descuento", (object)FacturaNotaCredito.descuento ?? DBNull.Value);
                    command.Parameters.AddWithValue("@subtotal", (object)FacturaNotaCredito.subtotal ?? DBNull.Value);
                    command.Parameters.AddWithValue("@iva", (object)FacturaNotaCredito.iva ?? DBNull.Value);
                    command.Parameters.AddWithValue("@total", (object)FacturaNotaCredito.total ?? DBNull.Value);
                    command.Parameters.AddWithValue("@pagoEfectivo", (object)FacturaNotaCredito.pagoEfectivo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@pagoTarjetaDebCred", (object)FacturaNotaCredito.pagoTarjetaDebCred ?? DBNull.Value);
                    command.Parameters.AddWithValue("@pagoOtraForma", (object)FacturaNotaCredito.pagoOtraForma ?? DBNull.Value);
                    command.Parameters.AddWithValue("@pagoOtraFormaDetalle", (object)FacturaNotaCredito.pagoOtraFormaDetalle ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", (object)FacturaNotaCredito.id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@dtReg", (object)FacturaNotaCredito.dtReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", (object)FacturaNotaCredito.idPersReg ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado", (object)FacturaNotaCredito.estado ?? DBNull.Value);
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
                string query = @"DELETE FROM FacturaNotaCredito WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsFacturaNotaCredito>> ListarAsync(FetchDataFacturaNotaCredito fetchData)
        {
            List<ClsFacturaNotaCredito> FacturaNotaCreditos = new List<ClsFacturaNotaCredito>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo
                queryBuilder.Append(@"SELECT TOP (@take) emisor_id, registroFactura_id, cliente_id, claveAcceso, esFactura, autorizacion, subtotalPrevio, subtotal0, descuento, subtotal, iva, total, pagoEfectivo, pagoTarjetaDebCred, pagoOtraForma, pagoOtraFormaDetalle, id, dtReg, idPersReg, estado
                                  FROM FacturaNotaCredito
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
                            var FacturaNotaCredito = new ClsFacturaNotaCredito
                            {
                                emisor = new ClsEmisorItem { id = reader.GetInt32(reader.GetOrdinal("emisor_id")) },
                                //emisor_id = reader.GetInt32(reader.GetOrdinal("emisor_id")),
                                registroFactura = new ClsRegistroDoc { id = reader.GetInt32(reader.GetOrdinal("registroFactura_id")) },
                                //registroFactura_id = reader.GetInt32(reader.GetOrdinal("registroFactura_id")),
                                cliente = new ClsCliente { id = reader.GetInt32(reader.GetOrdinal("cliente_id")) },
                                //cliente_id = reader.GetInt32(reader.GetOrdinal("cliente_id")),
                                claveAcceso = reader.IsDBNull(reader.GetOrdinal("claveAcceso")) ? null : reader.GetString(reader.GetOrdinal("claveAcceso")),
//no soportado
                                autorizacion = reader.IsDBNull(reader.GetOrdinal("autorizacion")) ? null : reader.GetString(reader.GetOrdinal("autorizacion")),
                                subtotalPrevio = reader.IsDBNull(reader.GetOrdinal("subtotalPrevio")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("subtotalPrevio")),
                                subtotal0 = reader.IsDBNull(reader.GetOrdinal("subtotal0")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("subtotal0")),
                                descuento = reader.IsDBNull(reader.GetOrdinal("descuento")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("descuento")),
                                subtotal = reader.IsDBNull(reader.GetOrdinal("subtotal")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("subtotal")),
                                iva = reader.IsDBNull(reader.GetOrdinal("iva")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("iva")),
                                total = reader.IsDBNull(reader.GetOrdinal("total")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("total")),
                                pagoEfectivo = reader.IsDBNull(reader.GetOrdinal("pagoEfectivo")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("pagoEfectivo")),
                                pagoTarjetaDebCred = reader.IsDBNull(reader.GetOrdinal("pagoTarjetaDebCred")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("pagoTarjetaDebCred")),
                                pagoOtraForma = reader.IsDBNull(reader.GetOrdinal("pagoOtraForma")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("pagoOtraForma")),
                                pagoOtraFormaDetalle = reader.IsDBNull(reader.GetOrdinal("pagoOtraFormaDetalle")) ? null : reader.GetString(reader.GetOrdinal("pagoOtraFormaDetalle")),
                                id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                            };

                            FacturaNotaCreditos.Add(FacturaNotaCredito);
                        }
                    }
                }
            }
            return FacturaNotaCreditos;
        }

        public async Task<ClsFacturaNotaCredito> ObtenerPorIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                
                //var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, nombre1, nombre2, apellido1, apellido2, fechaNacimiento, cedula, sexo_id, estadoCivil_id, nacionalidad_id, grupoSanguineo, tipoSanguineo 
                var command = new SqlCommand(@"SELECT emisor_id, registroFactura_id, cliente_id, claveAcceso, esFactura, autorizacion, subtotalPrevio, subtotal0, descuento, subtotal, iva, total, pagoEfectivo, pagoTarjetaDebCred, pagoOtraForma, pagoOtraFormaDetalle, id, dtReg, idPersReg, estado
                                           FROM FacturaNotaCredito 
                                           WHERE id = @id
                                           AND estado != 0", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ClsFacturaNotaCredito
                        {
                                emisor = new ClsEmisorItem { id = reader.GetInt32(reader.GetOrdinal("emisor_id")) },
                                //emisor_id = reader.GetInt32(reader.GetOrdinal("emisor_id")),
                                registroFactura = new ClsRegistroDoc { id = reader.GetInt32(reader.GetOrdinal("registroFactura_id")) },
                                //registroFactura_id = reader.GetInt32(reader.GetOrdinal("registroFactura_id")),
                                cliente = new ClsCliente { id = reader.GetInt32(reader.GetOrdinal("cliente_id")) },
                                //cliente_id = reader.GetInt32(reader.GetOrdinal("cliente_id")),
                                claveAcceso = reader.IsDBNull(reader.GetOrdinal("claveAcceso")) ? null : reader.GetString(reader.GetOrdinal("claveAcceso")),
//no soportado
                                autorizacion = reader.IsDBNull(reader.GetOrdinal("autorizacion")) ? null : reader.GetString(reader.GetOrdinal("autorizacion")),
                                subtotalPrevio = reader.IsDBNull(reader.GetOrdinal("subtotalPrevio")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("subtotalPrevio")),
                                subtotal0 = reader.IsDBNull(reader.GetOrdinal("subtotal0")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("subtotal0")),
                                descuento = reader.IsDBNull(reader.GetOrdinal("descuento")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("descuento")),
                                subtotal = reader.IsDBNull(reader.GetOrdinal("subtotal")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("subtotal")),
                                iva = reader.IsDBNull(reader.GetOrdinal("iva")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("iva")),
                                total = reader.IsDBNull(reader.GetOrdinal("total")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("total")),
                                pagoEfectivo = reader.IsDBNull(reader.GetOrdinal("pagoEfectivo")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("pagoEfectivo")),
                                pagoTarjetaDebCred = reader.IsDBNull(reader.GetOrdinal("pagoTarjetaDebCred")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("pagoTarjetaDebCred")),
                                pagoOtraForma = reader.IsDBNull(reader.GetOrdinal("pagoOtraForma")) ? (int?)null : reader.GetDouble(reader.GetOrdinal("pagoOtraForma")),
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
