using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DB
{
    public class DbParticipante : DbSQLServer2022
    {
        public DbParticipante() { }

        // Crear Participante y asociar funciones
        /*public async Task<int> CrearAsync(ClsParticipante participante)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    // Insertar Participante
                    string query = @"INSERT INTO Participante (dtReg, idPersReg, estado, persona_id, detalle, horasTrabajadas)
                                 VALUES (@dtReg, @idPersReg, @estado, @persona_id, @detalle, @horasTrabajadas);
                                 SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@dtReg", (object)participante.dtReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@idPersReg", (object)participante.idPersReg ?? DBNull.Value);
                        command.Parameters.AddWithValue("@estado", (object)participante.estado ?? DBNull.Value);
                        command.Parameters.AddWithValue("@persona_id", (object)participante.persona?.idApi ?? DBNull.Value);
                        command.Parameters.AddWithValue("@detalle", (object)participante.detalle ?? DBNull.Value);
                        command.Parameters.AddWithValue("@horasTrabajadas", (object)participante.horasTrabajadas ?? DBNull.Value);

                        connection.Open();
                        var result = await command.ExecuteScalarAsync();
                        participante.id = Convert.ToInt32(result);

                        // Insertar funciones asociadas
                        if (participante.funciones != null && participante.funciones.Any())
                        {
                            foreach (var funcion in participante.funciones)
                            {
                                string funcionQuery = @"INSERT INTO Participante_Funcion (participante_id, funcion_id)
                                                    VALUES (@participante_id, @funcion_id)";

                                using (var funcionCommand = new SqlCommand(funcionQuery, connection))
                                {
                                    funcionCommand.Parameters.AddWithValue("@participante_id", participante.id);
                                    funcionCommand.Parameters.AddWithValue("@funcion_id", funcion.idApi);
                                    await funcionCommand.ExecuteNonQueryAsync();
                                }
                            }
                        }

                        return (int)participante.id;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }*/


        public async Task<int> CrearAsync(ClsParticipante participante)
        {
            int nuevoParticipanteId = -1;
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand("InsertarParticipanteConFunciones", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@dtReg", participante.dtReg ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", participante.idPersReg ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@estado", participante.estado ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@persona_id", participante.persona?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@detalle", participante.detalle ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@horasTrabajadas", participante.horasTrabajadas ?? (object)DBNull.Value);

                    DataTable funcionesTable = new DataTable();
                    funcionesTable.Columns.Add("funcion_id", typeof(int));

                    foreach (var funcion in participante.funciones)
                    {
                        funcionesTable.Rows.Add(funcion.idApi);
                    }

                    SqlParameter tvpParam = new SqlParameter("@FuncionesData", SqlDbType.Structured)
                    {
                        TypeName = "TVP_FuncionesIdList",
                        Value = funcionesTable
                    };
                    command.Parameters.Add(tvpParam);
                    SqlParameter outputIdParam = new SqlParameter("@NuevoParticipanteId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    command.ExecuteNonQuery();
                    nuevoParticipanteId = (int)outputIdParam.Value;
                }
            }
            return nuevoParticipanteId;
        }

        public async Task<bool> EditarAsync(ClsParticipante participante)
        {
            bool exito = false;
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand("ActualizarParticipanteConFunciones", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros del participante
                    command.Parameters.AddWithValue("@id", participante.idApi);
                    command.Parameters.AddWithValue("@dtReg", participante.dtReg ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idPersReg", participante.idPersReg ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@estado", participante.estado ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@persona_id", participante.persona?.idApi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@detalle", participante.detalle ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@horasTrabajadas", participante.horasTrabajadas ?? (object)DBNull.Value);

                    // Crear y agregar la lista de funciones a un DataTable
                    DataTable funcionesTable = new DataTable();
                    funcionesTable.Columns.Add("funcion_id", typeof(int));

                    foreach (var funcion in participante.funciones)
                    {
                        funcionesTable.Rows.Add(funcion.idApi);
                    }

                    SqlParameter tvpParam = new SqlParameter("@FuncionesData", SqlDbType.Structured)
                    {
                        TypeName = "TVP_FuncionesIdList",
                        Value = funcionesTable
                    };
                    command.Parameters.Add(tvpParam);

                    // Ejecutar el procedimiento almacenado
                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    // Si se afectaron filas, significa que la actualización fue exitosa
                    exito = rowsAffected > 0;
                }
            }
            return exito;
        }

        /*
        // Editar Participante y sus funciones asociadas
        public async Task<bool> EditarAsync(ClsParticipante participante)
        {
            using (var connection = GetConnection())
            {
                string query = @"UPDATE Participante
                             SET dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado, 
                                 persona_id = @persona_id, detalle = @detalle, horasTrabajadas = @horasTrabajadas
                             WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", participante.idApi);
                    command.Parameters.AddWithValue("@dtReg", participante.dtReg);
                    command.Parameters.AddWithValue("@idPersReg", participante.idPersReg);
                    command.Parameters.AddWithValue("@estado", participante.estado);
                    command.Parameters.AddWithValue("@persona_id", participante.persona?.idApi ?? 0);
                    command.Parameters.AddWithValue("@detalle", participante.detalle);
                    command.Parameters.AddWithValue("@horasTrabajadas", participante.horasTrabajadas);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Eliminar funciones existentes
                    string deleteFuncionesQuery = @"DELETE FROM Participante_Funcion WHERE participante_id = @id";
                    using (var deleteCommand = new SqlCommand(deleteFuncionesQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@id", participante.id);
                        await deleteCommand.ExecuteNonQueryAsync();
                    }

                    // Insertar funciones asociadas
                    if (participante.funciones != null && participante.funciones.Any())
                    {
                        foreach (var funcion in participante.funciones)
                        {
                            string insertFuncionQuery = @"INSERT INTO Participante_Funcion (participante_id, funcion_id)
                                                    VALUES (@participante_id, @funcion_id)";
                            using (var insertCommand = new SqlCommand(insertFuncionQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@participante_id", participante.idApi);
                                insertCommand.Parameters.AddWithValue("@funcion_id", funcion.idApi);
                                await insertCommand.ExecuteNonQueryAsync();
                            }
                        }
                    }

                    return rowsAffected > 0;
                }
            }
        }*/

        // Eliminar Participante y sus funciones asociadas
        public async Task<bool> EliminarAsync(int id)
        {
            using (var connection = GetConnection())
            {
                // Eliminar funciones asociadas
                string deleteFuncionesQuery = @"DELETE FROM Participante_Funcion WHERE participante_id = @id";
                using (var deleteFuncionesCommand = new SqlCommand(deleteFuncionesQuery, connection))
                {
                    deleteFuncionesCommand.Parameters.AddWithValue("@id", id);
                    await deleteFuncionesCommand.ExecuteNonQueryAsync();
                }

                // Eliminar Participante
                string deleteParticipanteQuery = @"DELETE FROM Participante WHERE id = @id";
                using (var command = new SqlCommand(deleteParticipanteQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<ClsParticipante>> ListarAsync(FetchDataParticipante fetchData)
        {
            List<ClsParticipante> participantes = new List<ClsParticipante>();

            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand("ListarParticipantesConFunciones", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros para el procedimiento almacenado
                    command.Parameters.AddWithValue("@offsetDT", fetchData.offsetDT);
                    command.Parameters.AddWithValue("@take", fetchData.take);
                    
                    // Parámetros opcionales
                    command.Parameters.AddWithValue("@estado", fetchData.estado ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombre1", fetchData.persona?.nombre1 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombre2", fetchData.persona?.nombre2 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@apellido1", fetchData.persona?.apellido1 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@apellido2", fetchData.persona?.apellido2 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fechaNacimiento", fetchData.persona?.fechaNacimiento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@cedula", fetchData.persona?.cedula ?? (object)DBNull.Value);

                    // Filtro opcional por id de participante
                    command.Parameters.AddWithValue("@participanteId", fetchData.idApi ?? (object)DBNull.Value);

                    // Crear los DataTables para los parámetros de tipo
                    var funcionesDataTable = new DataTable();
                    funcionesDataTable.Columns.Add("funcion_id", typeof(int));
                    foreach (var funcion in fetchData.funciones ?? new List<ClsFuncionParticipante>())
                    {
                        funcionesDataTable.Rows.Add(funcion.id);
                    }

                    var sexoDataTable = new DataTable();
                    sexoDataTable.Columns.Add("sexo_id", typeof(int));
                    var nacionalidadDataTable = new DataTable();
                    nacionalidadDataTable.Columns.Add("nacionalidad_id", typeof(int));
                    var estadoCivilDataTable = new DataTable();
                    estadoCivilDataTable.Columns.Add("estadoCivil_id", typeof(int));
                    //if (fetchData.persona != null)
                    //{
                    //    foreach (var sexo in fetchData.persona.sexo ?? new List<ClsSexo>())
                    //    {
                    //        sexoDataTable.Rows.Add(sexo.idApi);
                    //    }


                    //    foreach (var nacionalidad in fetchData.persona.nacionalidad ?? new List<ClsNacionalidad>())
                    //    {
                    //        nacionalidadDataTable.Rows.Add(nacionalidad.idApi);
                    //    }


                    //    foreach (var estadoCivil in fetchData.persona.estadoCivil ?? new List<ClsEstadoCivil>())
                    //    {
                    //        estadoCivilDataTable.Rows.Add(estadoCivil.idApi);
                    //    }
                    //}
                    

                    // Agregar los parámetros de las tablas temporales
                    command.Parameters.Add(new SqlParameter("@FuncionesData", SqlDbType.Structured)
                    {
                        TypeName = "TVP_FuncionesIdList",
                        Value = funcionesDataTable
                    });

                    command.Parameters.Add(new SqlParameter("@SexoData", SqlDbType.Structured)
                    {
                        TypeName = "TVP_SexoIdList",
                        Value = sexoDataTable
                    });

                    command.Parameters.Add(new SqlParameter("@NacionalidadData", SqlDbType.Structured)
                    {
                        TypeName = "TVP_NacionalidadIdList",
                        Value = nacionalidadDataTable
                    });

                    command.Parameters.Add(new SqlParameter("@EstadoCivilData", SqlDbType.Structured)
                    {
                        TypeName = "TVP_EstadoCivilIdList",
                        Value = estadoCivilDataTable
                    });

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var participantesDict = new Dictionary<int, ClsParticipante>();

                        // Leer los participantes con las funciones
                        while (await reader.ReadAsync())
                        {
                            int participanteId = reader.GetInt32(reader.GetOrdinal("participante_id"));

                            // Si el participante no existe en el diccionario, crear un nuevo objeto
                            if (!participantesDict.ContainsKey(participanteId))
                            {
                                var participante = new ClsParticipante
                                {
                                    id = participanteId,
                                    dtReg = reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                    idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                    estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                                    persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
                                    detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
                                    horasTrabajadas = reader.IsDBNull(reader.GetOrdinal("horasTrabajadas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("horasTrabajadas")),
                                    funciones = new List<ClsFuncionParticipante>() // Inicializa la lista de funciones
                                };

                                participantesDict[participanteId] = participante;
                            }

                            // Ahora añadir las funciones al participante correspondiente
                            int? funcionId = reader.IsDBNull(reader.GetOrdinal("funcion_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("funcion_id"));
                            if (funcionId.HasValue)
                            {
                                // Solo añadir la función si existe (es decir, no es nula)
                                participantesDict[participanteId].funciones.Add(new ClsFuncionParticipante
                                {
                                    id = funcionId.Value
                                });
                            }
                        }

                        // Convertir el diccionario a una lista para retornar
                        participantes = participantesDict.Values.ToList();
                    }
                }
            }

            return participantes;
        }

        public async Task<ClsParticipante?> ObtenerPorIdAsync(int participanteId)
        {
            var participantes = await ListarAsync(new FetchDataParticipante
            {
                idApi = participanteId,
                offsetDT = DateTime.UtcNow,
                take = 1
            });
            return participantes.FirstOrDefault();
        }

        // Listar Participantes con funciones asociadas
        public async Task<List<ClsParticipante>> ListarAsync1(FetchDataParticipante fetchData)
        {
            List<ClsParticipante> participantes = new List<ClsParticipante>();
            using (var connection = GetConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"SELECT TOP (@take) p.id, p.dtReg, p.idPersReg, p.estado, p.persona_id, p.detalle, p.horasTrabajadas
                                  FROM Participante p
                                  WHERE p.dtReg < @offsetDT
                                  AND p.estado != 0");

                queryBuilder.Append(" ORDER BY p.dtReg DESC");

                //queryBuilder.Append(@"SELECT * FROM Participante");

                string query = queryBuilder.ToString();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offsetDT", fetchData.offsetDT);
                    command.Parameters.AddWithValue("@take", fetchData.take);
                    // Abrimos la conexión y ejecutamos la primera consulta
                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Leemos los participantes
                        while (await reader.ReadAsync())
                        {
                            var participante = new ClsParticipante
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                                persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
                                detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
                                horasTrabajadas = reader.IsDBNull(reader.GetOrdinal("horasTrabajadas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("horasTrabajadas"))
                            };

                            // Agregar el participante a la lista
                            participantes.Add(participante);
                        }
                    }
                }

                //using (var command = new SqlCommand(query, connection))
                //{
                //    //command.Parameters.AddWithValue("@offsetDT", fetchData.offsetDT);
                //    //command.Parameters.AddWithValue("@take", fetchData.take);

                //    connection.Open();
                //    using (var reader = await command.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            var participante = new ClsParticipante
                //            {
                //                id = reader.GetInt32(reader.GetOrdinal("id")),
                //                dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
                //                idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
                //                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
                //                persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
                //                detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
                //                horasTrabajadas = reader.IsDBNull(reader.GetOrdinal("horasTrabajadas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("horasTrabajadas"))
                //            };

                //            // Obtener las funciones asociadas
                //            var funcionesQuery = @"SELECT f.id, f.nombre, f.detalle 
                //                               FROM FuncionParticipante f
                //                               INNER JOIN Participante_Funcion pf ON f.id = pf.funcion_id
                //                               WHERE pf.participante_id = @id";
                //            using (var funcionCommand = new SqlCommand(funcionesQuery, connection))
                //            {
                //                funcionCommand.Parameters.AddWithValue("@id", participante.id);
                //                using (var funcionReader = await funcionCommand.ExecuteReaderAsync())
                //                {
                //                    participante.funciones = new List<ClsFuncionParticipante>();
                //                    while (await funcionReader.ReadAsync())
                //                    {
                //                        participante.funciones.Add(new ClsFuncionParticipante
                //                        {
                //                            id = funcionReader.GetInt32(funcionReader.GetOrdinal("id")),
                //                            nombre = funcionReader.GetString(funcionReader.GetOrdinal("nombre")),
                //                            detalle = funcionReader.IsDBNull(funcionReader.GetOrdinal("detalle")) ? null : funcionReader.GetString(funcionReader.GetOrdinal("detalle"))
                //                        });
                //                    }
                //                }
                //            }

                //            participantes.Add(participante);
                //        }
                //    }
                //}
            }

            foreach (var participante in participantes)
            {
                using (var connection = GetConnection())
                {
                    var funcionesQuery = @"SELECT f.id, f.nombre, f.detalle 
                           FROM FuncionParticipante f
                           INNER JOIN Participante_Funcion pf ON f.id = pf.funcion_id
                           WHERE pf.participante_id = @id";

                    using (var funcionCommand = new SqlCommand(funcionesQuery, connection))
                    {
                        funcionCommand.Parameters.AddWithValue("@id", participante.id);

                        // Abrimos la conexión nuevamente para la segunda consulta
                        connection.Open();

                        using (var funcionReader = await funcionCommand.ExecuteReaderAsync())
                        {
                            participante.funciones = new List<ClsFuncionParticipante>();
                            while (await funcionReader.ReadAsync())
                            {
                                participante.funciones.Add(new ClsFuncionParticipante
                                {
                                    id = funcionReader.GetInt32(funcionReader.GetOrdinal("id")),
                                    nombre = funcionReader.GetString(funcionReader.GetOrdinal("nombre")),
                                    detalle = funcionReader.IsDBNull(funcionReader.GetOrdinal("detalle")) ? null : funcionReader.GetString(funcionReader.GetOrdinal("detalle"))
                                });
                            }
                        }
                    }
                }
            }
            return participantes;
        }

        //// Obtener Participante por ID con funciones asociadas
        //public async Task<ClsParticipante> ObtenerPorIdAsync(int id)
        //{
        //    using (var connection = GetConnection())
        //    {
        //        var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, persona_id, detalle, horasTrabajadas 
        //                                   FROM Participante 
        //                                   WHERE id = @id
        //                                   AND estado != 0", connection);
        //        command.Parameters.AddWithValue("@id", id);

        //        await connection.OpenAsync();
        //        using (var reader = await command.ExecuteReaderAsync())
        //        {
        //            if (await reader.ReadAsync())
        //            {
        //                var participante = new ClsParticipante
        //                {
        //                    id = reader.GetInt32(reader.GetOrdinal("id")),
        //                    dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
        //                    idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
        //                    estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
        //                    persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
        //                    detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
        //                    horasTrabajadas = reader.IsDBNull(reader.GetOrdinal("horasTrabajadas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("horasTrabajadas"))
        //                };

        //                // Obtener las funciones asociadas
        //                var funcionesQuery = @"SELECT f.id, f.nombre, f.detalle 
        //                                   FROM FuncionParticipante f
        //                                   INNER JOIN Participante_Funcion pf ON f.id = pf.funcion_id
        //                                   WHERE pf.participante_id = @id";
        //                using (var funcionCommand = new SqlCommand(funcionesQuery, connection))
        //                {
        //                    funcionCommand.Parameters.AddWithValue("@id", participante.id);
        //                    using (var funcionReader = await funcionCommand.ExecuteReaderAsync())
        //                    {
        //                        participante.funciones = new List<ClsFuncionParticipante>();
        //                        while (await funcionReader.ReadAsync())
        //                        {
        //                            participante.funciones.Add(new ClsFuncionParticipante
        //                            {
        //                                id = funcionReader.GetInt32(funcionReader.GetOrdinal("id")),
        //                                nombre = funcionReader.GetString(funcionReader.GetOrdinal("nombre")),
        //                                detalle = funcionReader.IsDBNull(funcionReader.GetOrdinal("detalle")) ? null : funcionReader.GetString(funcionReader.GetOrdinal("detalle"))
        //                            });
        //                        }
        //                    }
        //                }

        //                return participante;
        //            }
        //            return null;
        //        }
        //    }
        //}
    }


    //public class DbParticipante : DbSQLServer2022
    //{
    //    public DbParticipante() { }

    //    public async Task<int> CrearAsync(ClsParticipante participante)
    //    {
    //        try
    //        {
    //            using (var connection = GetConnection())
    //            {
    //                string query = @"INSERT INTO Participante (dtReg, idPersReg, estado, persona_id, detalle, horasTrabajadas)
    //                             VALUES (@dtReg, @idPersReg, @estado, @persona_id, @detalle, @horasTrabajadas);
    //                             SELECT SCOPE_IDENTITY();";

    //                using (var command = new SqlCommand(query, connection))
    //                {
    //                    command.Parameters.AddWithValue("@dtReg", (object)participante.dtReg ?? DBNull.Value);
    //                    command.Parameters.AddWithValue("@idPersReg", (object)participante.idPersReg ?? DBNull.Value);
    //                    command.Parameters.AddWithValue("@estado", (object)participante.estado ?? DBNull.Value);
    //                    command.Parameters.AddWithValue("@persona_id", (object)participante.persona?.idApi ?? DBNull.Value);
    //                    command.Parameters.AddWithValue("@detalle", (object)participante.detalle ?? DBNull.Value);
    //                    command.Parameters.AddWithValue("@horasTrabajadas", (object)participante.horasTrabajadas ?? DBNull.Value);

    //                    connection.Open();
    //                    var result = await command.ExecuteScalarAsync();
    //                    participante.id = Convert.ToInt32(result);
    //                    return (int)participante.id;
    //                }
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            return -1;
    //        }
    //    }

    //    public async Task<bool> EditarAsync(ClsParticipante participante)
    //    {
    //        using (var connection = GetConnection())
    //        {
    //            string query = @"UPDATE Participante
    //                         SET dtReg = @dtReg, idPersReg = @idPersReg, estado = @estado, 
    //                             persona_id = @persona_id, detalle = @detalle, horasTrabajadas = @horasTrabajadas
    //                         WHERE id = @id";

    //            using (var command = new SqlCommand(query, connection))
    //            {
    //                command.Parameters.AddWithValue("@id", participante.id);
    //                command.Parameters.AddWithValue("@dtReg", participante.dtReg);
    //                command.Parameters.AddWithValue("@idPersReg", participante.idPersReg);
    //                command.Parameters.AddWithValue("@estado", participante.estado);
    //                command.Parameters.AddWithValue("@persona_id", participante.persona?.idApi ?? 0);
    //                command.Parameters.AddWithValue("@detalle", participante.detalle);
    //                command.Parameters.AddWithValue("@horasTrabajadas", participante.horasTrabajadas);

    //                connection.Open();
    //                int rowsAffected = await command.ExecuteNonQueryAsync();
    //                return rowsAffected > 0;
    //            }
    //        }
    //    }

    //    public async Task<bool> EliminarAsync(int id)
    //    {
    //        using (var connection = GetConnection())
    //        {
    //            string query = @"DELETE FROM Participante WHERE id = @id";

    //            using (var command = new SqlCommand(query, connection))
    //            {
    //                command.Parameters.AddWithValue("@id", id);

    //                connection.Open();
    //                int rowsAffected = await command.ExecuteNonQueryAsync();
    //                return rowsAffected > 0;
    //            }
    //        }
    //    }

    //    public async Task<List<ClsParticipante>> ListarAsync(FetchDataParticipante fetchData)
    //    {
    //        List<ClsParticipante> participantes = new List<ClsParticipante>();
    //        using (var connection = GetConnection())
    //        {
    //            StringBuilder queryBuilder = new StringBuilder();
    //            queryBuilder.Append(@"SELECT TOP (@take) id, dtReg, idPersReg, estado, persona_id, detalle, horasTrabajadas
    //                              FROM Participante
    //                              WHERE dtReg < @offsetDT
    //                              AND estado != 0");

    //            queryBuilder.Append(" ORDER BY dtReg DESC");

    //            string query = queryBuilder.ToString();

    //            using (var command = new SqlCommand(query, connection))
    //            {
    //                command.Parameters.AddWithValue("@offsetDT", fetchData.offsetDT);
    //                command.Parameters.AddWithValue("@take", fetchData.take);

    //                connection.Open();
    //                using (var reader = await command.ExecuteReaderAsync())
    //                {
    //                    while (await reader.ReadAsync())
    //                    {
    //                        var participante = new ClsParticipante
    //                        {
    //                            id = reader.GetInt32(reader.GetOrdinal("id")),
    //                            dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
    //                            idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
    //                            estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
    //                            persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
    //                            detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
    //                            horasTrabajadas = reader.IsDBNull(reader.GetOrdinal("horasTrabajadas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("horasTrabajadas"))
    //                        };
    //                        participantes.Add(participante);
    //                    }
    //                }
    //            }
    //        }
    //        return participantes;
    //    }

    //    public async Task<ClsParticipante> ObtenerPorIdAsync(int id)
    //    {
    //        using (var connection = GetConnection())
    //        {
    //            var command = new SqlCommand(@"SELECT id, dtReg, idPersReg, estado, persona_id, detalle, horasTrabajadas 
    //                                       FROM Participante 
    //                                       WHERE id = @id
    //                                       AND estado != 0", connection);
    //            command.Parameters.AddWithValue("@id", id);

    //            await connection.OpenAsync();
    //            using (var reader = await command.ExecuteReaderAsync())
    //            {
    //                if (await reader.ReadAsync())
    //                {
    //                    return new ClsParticipante
    //                    {
    //                        id = reader.GetInt32(reader.GetOrdinal("id")),
    //                        dtReg = reader.IsDBNull(reader.GetOrdinal("dtReg")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtReg")),
    //                        idPersReg = reader.IsDBNull(reader.GetOrdinal("idPersReg")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idPersReg")),
    //                        estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado")),
    //                        persona = new ClsPersona { id = reader.GetInt32(reader.GetOrdinal("persona_id")) },
    //                        detalle = reader.IsDBNull(reader.GetOrdinal("detalle")) ? null : reader.GetString(reader.GetOrdinal("detalle")),
    //                        horasTrabajadas = reader.IsDBNull(reader.GetOrdinal("horasTrabajadas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("horasTrabajadas"))
    //                    };
    //                }
    //                return null;
    //            }
    //        }
    //    }
    //}

}
