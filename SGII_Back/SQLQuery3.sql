use SGII_BackDB

CREATE TABLE Cargo (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME,
    idPersReg INT,
    estado INT,
    nombre NVARCHAR(255),
    detalle NVARCHAR(1000)
);

CREATE TABLE Participante (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    persona_id INT NULL,
    detalle NVARCHAR(255) NULL,
    horasTrabajadas INT NULL,
    FOREIGN KEY (persona_id) REFERENCES Persona(id)
);

CREATE TABLE FuncionParticipante (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME,
    idPersReg INT,
    estado INT,
    nombre NVARCHAR(255),
    detalle NVARCHAR(1000)
);

CREATE TABLE Participante_Funcion (
    id INT IDENTITY(1,1) PRIMARY KEY,
    participante_id INT NOT NULL,
    funcion_id INT NOT NULL,
    FOREIGN KEY (participante_id) REFERENCES Participante(id) ON DELETE CASCADE,
    FOREIGN KEY (funcion_id) REFERENCES FuncionParticipante(id)
);

CREATE TABLE CondicionLaboral (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    nombre NVARCHAR(MAX) NULL
);

CREATE TABLE EstadoCivil (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    nombre NVARCHAR(MAX) NULL
);

CREATE TABLE EstadoLaboral (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    nombre NVARCHAR(MAX) NULL
);

CREATE TABLE TipoColab (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME,
    idPersReg INT,
    estado INT,
    nombre NVARCHAR(255),
    detalle NVARCHAR(1000)
);

CREATE TABLE Nacionalidad (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    nombre NVARCHAR(MAX) NULL
);

CREATE TABLE Sexo (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    nombre NVARCHAR(MAX) NULL
);

CREATE TABLE Persona (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    nombre1 NVARCHAR(50) NULL,
    nombre2 NVARCHAR(50) NULL,
    apellido1 NVARCHAR(50) NULL,
    apellido2 NVARCHAR(50) NULL,
    fechaNacimiento DATE NULL,
    cedula NVARCHAR(20) NULL,
    sexo_id INT NOT NULL,
    estadoCivil_id INT NOT NULL,
    nacionalidad_id INT NOT NULL,
    grupoSanguineo NVARCHAR(10) NULL,
    tipoSanguineo NVARCHAR(10) NULL,
    CONSTRAINT FK_Persona_Sexo FOREIGN KEY (sexo_id) REFERENCES Sexo(id),
    CONSTRAINT FK_Persona_EstadoCivil FOREIGN KEY (estadoCivil_id) REFERENCES EstadoCivil(id),
    CONSTRAINT FK_Persona_Nacionalidad FOREIGN KEY (nacionalidad_id) REFERENCES Nacionalidad(id)
);

CREATE TABLE Colaborador (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME,
    idPersReg INT,
    estado INT,
    persona_id INT,
    inicioAsignacion DATETIME,
    finAsignacion DATETIME,
    tipoColab_id INT,
    cargo_id INT,
    estadoLaboral_id INT,
    condicionLaboral_id INT,
    sueldo FLOAT,
    horasTrabajo INT,
    contrato NVARCHAR(100),

    -- Claves Foráneas
    CONSTRAINT FK_Persona FOREIGN KEY (persona_id) REFERENCES Persona(id),
    CONSTRAINT FK_TipoColab FOREIGN KEY (tipoColab_id) REFERENCES TipoColab(id),
    CONSTRAINT FK_Cargo FOREIGN KEY (cargo_id) REFERENCES Cargo(id),
    CONSTRAINT FK_EstadoLaboral FOREIGN KEY (estadoLaboral_id) REFERENCES EstadoLaboral(id),
    CONSTRAINT FK_CondicionLaboral FOREIGN KEY (condicionLaboral_id) REFERENCES CondicionLaboral(id)
);



CREATE TABLE MyUser (
    persona_id INT NULL,
    email NVARCHAR(255) NULL,
    urlFoto NVARCHAR(255) NULL,
    role NVARCHAR(255) NULL,
    password NVARCHAR(255) NULL,
    salt NVARCHAR(255) NULL,
    tempCode NVARCHAR(255) NULL,
    tempCodeCreateAt DATETIME NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    idApi INT NULL,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (persona_id) REFERENCES persona(id),
);


CREATE PROCEDURE InsertarParticipanteConFunciones
    @dtReg DATETIME,
    @idPersReg INT,
    @estado INT,
    @persona_id INT,
    @detalle NVARCHAR(255),
    @horasTrabajadas INT,
    @FuncionesData TVP_FuncionesIdList READONLY,
    @NuevoParticipanteId INT OUTPUT  -- Parámetro de salida para devolver el ID
AS
BEGIN
    SET NOCOUNT ON;

    -- Comenzamos la transacción para asegurar atomicidad
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar el nuevo participante en la tabla Participante
        INSERT INTO Participante (dtReg, idPersReg, estado, persona_id, detalle, horasTrabajadas)
        VALUES (@dtReg, @idPersReg, @estado, @persona_id, @detalle, @horasTrabajadas);

        -- Obtener el ID del nuevo participante insertado
        SET @NuevoParticipanteId = SCOPE_IDENTITY();

        -- Declarar una variable para la función ID
        DECLARE @FuncionId INT;

        -- Utilizar un cursor para insertar las funciones asociadas al participante
        DECLARE func_cursor CURSOR FOR
        SELECT funcion_id
        FROM @FuncionesData;

        OPEN func_cursor;
        FETCH NEXT FROM func_cursor INTO @FuncionId;

        -- Insertar cada función en la tabla Participante_Funcion
        WHILE @@FETCH_STATUS = 0
        BEGIN
            INSERT INTO Participante_Funcion (participante_id, funcion_id)
            VALUES (@NuevoParticipanteId, @FuncionId);

            FETCH NEXT FROM func_cursor INTO @FuncionId;
        END

        CLOSE func_cursor;
        DEALLOCATE func_cursor;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, deshacer la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END


CREATE TYPE TVP_FuncionesIdList AS TABLE (
    funcion_id INT
);

CREATE TYPE TVP_SexoIdList AS TABLE (
    sexo_id INT
);
CREATE TYPE TVP_NacionalidadIdList AS TABLE (
    nacionalidad_id INT
);
CREATE TYPE TVP_EstadoCivilIdList AS TABLE (
    estadoCivil_id INT
);

CREATE PROCEDURE ListarParticipantesConFunciones
    @offsetDT DATETIME,
    @take INT,
    @estado INT = NULL,
    @nombre1 NVARCHAR(100) = NULL,
    @nombre2 NVARCHAR(100) = NULL,
    @apellido1 NVARCHAR(100) = NULL,
    @apellido2 NVARCHAR(100) = NULL,
    @fechaNacimiento DATE = NULL,
    @cedula NVARCHAR(20) = NULL,
    @FuncionesData TVP_FuncionesIdList READONLY, -- Parámetro para la lista de IDs de funciones
    @SexoData TVP_SexoIdList READONLY, -- Parámetro para la lista de IDs de sexos
    @NacionalidadData TVP_NacionalidadIdList READONLY, -- Parámetro para la lista de IDs de nacionalidades
    @EstadoCivilData TVP_EstadoCivilIdList READONLY -- Parámetro para la lista de IDs de estados civiles
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    -- Tabla temporal para almacenar los participantes
    SELECT TOP (@take) 
        p.id,
        p.dtReg,
        p.idPersReg,
        p.estado,
        p.persona_id,
        p.detalle,
        p.horasTrabajadas
    INTO #Participantes
    FROM Participante p
    WHERE p.dtReg < @offsetDT
    AND p.estado != 0
    AND (@estado IS NULL OR p.estado = @estado);

    -- Filtrar por persona (aplicar LIKE en los campos que sean no nulos)
    IF @nombre1 IS NOT NULL
    BEGIN
        UPDATE #Participantes
        SET persona_id = p.id
        FROM Participante p
        JOIN Persona per ON p.persona_id = per.id
        WHERE per.nombre1 LIKE '%' + @nombre1 + '%';
    END

    IF @nombre2 IS NOT NULL
    BEGIN
        UPDATE #Participantes
        SET persona_id = p.id
        FROM Participante p
        JOIN Persona per ON p.persona_id = per.id
        WHERE per.nombre2 LIKE '%' + @nombre2 + '%';
    END

    IF @apellido1 IS NOT NULL
    BEGIN
        UPDATE #Participantes
        SET persona_id = p.id
        FROM Participante p
        JOIN Persona per ON p.persona_id = per.id
        WHERE per.apellido1 LIKE '%' + @apellido1 + '%';
    END

    IF @apellido2 IS NOT NULL
    BEGIN
        UPDATE #Participantes
        SET persona_id = p.id
        FROM Participante p
        JOIN Persona per ON p.persona_id = per.id
        WHERE per.apellido2 LIKE '%' + @apellido2 + '%';
    END

    IF @cedula IS NOT NULL
    BEGIN
        UPDATE #Participantes
        SET persona_id = p.id
        FROM Participante p
        JOIN Persona per ON p.persona_id = per.id
        WHERE per.cedula LIKE '%' + @cedula + '%';
    END

    IF @fechaNacimiento IS NOT NULL
    BEGIN
        UPDATE #Participantes
        SET persona_id = p.id
        FROM Participante p
        JOIN Persona per ON p.persona_id = per.id
        WHERE per.fechaNacimiento = @fechaNacimiento;
    END

    -- Filtrar por sexo
    IF EXISTS (SELECT 1 FROM @SexoData)
    BEGIN
        UPDATE #Participantes
        SET persona_id = p.id
        FROM Participante p
        JOIN Persona per ON p.persona_id = per.id
        JOIN Sexo s ON per.sexo_id = s.id
        WHERE s.id IN (SELECT sexo_id FROM @SexoData);
    END

    -- Filtrar por nacionalidad
    IF EXISTS (SELECT 1 FROM @NacionalidadData)
    BEGIN
        UPDATE #Participantes
        SET persona_id = p.id
        FROM Participante p
        JOIN Persona per ON p.persona_id = per.id
        JOIN Nacionalidad n ON per.nacionalidad_id = n.id
        WHERE n.id IN (SELECT nacionalidad_id FROM @NacionalidadData);
    END

    -- Filtrar por estado civil
    IF EXISTS (SELECT 1 FROM @EstadoCivilData)
    BEGIN
        UPDATE #Participantes
        SET persona_id = p.id
        FROM Participante p
        JOIN Persona per ON p.persona_id = per.id
        JOIN EstadoCivil ec ON per.estadoCivil_id = ec.id
        WHERE ec.id IN (SELECT estadoCivil_id FROM @EstadoCivilData);
    END

    -- Filtrar por funciones utilizando TVP_FuncionesIdList
    IF EXISTS (SELECT 1 FROM @FuncionesData)
    BEGIN
        UPDATE #Participantes
        SET persona_id = p.id
        FROM Participante p
        JOIN Persona per ON p.persona_id = per.id
        JOIN Participante_Funcion pf ON p.id = pf.participante_id
        WHERE pf.funcion_id IN (SELECT funcion_id FROM @FuncionesData);
    END

    -- Obtener las funciones asociadas a los participantes
    SELECT
        pf.participante_id AS participante_id,
        f.id AS funcion_id
    FROM Participante_Funcion pf
    INNER JOIN FuncionParticipante f ON f.id = pf.funcion_id
    WHERE pf.participante_id IN (SELECT id FROM #Participantes);

    -- Confirmar la transacción
    COMMIT TRANSACTION;

    -- Limpiar la tabla temporal
    DROP TABLE #Participantes;
END



EXEC ListarParticipantesConFunciones
    @offsetDT = '2024-12-30',
    @take = 10;


CREATE PROCEDURE ActualizarParticipanteConFunciones
    @id INT, -- ID del participante a editar
    @dtReg DATETIME,
    @idPersReg INT,
    @estado INT,
    @persona_id INT,
    @detalle NVARCHAR(255),
    @horasTrabajadas INT,
    @FuncionesData TVP_FuncionesIdList READONLY -- Nueva lista de funciones
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Actualizar los datos del participante
        UPDATE Participante
        SET
            dtReg = @dtReg,
            idPersReg = @idPersReg,
            estado = @estado,
            persona_id = @persona_id,
            detalle = @detalle,
            horasTrabajadas = @horasTrabajadas
        WHERE id = @id;

        -- Eliminar las funciones asociadas al participante (si existen)
        DELETE FROM Participante_Funcion WHERE participante_id = @id;

        -- Insertar las nuevas funciones asociadas al participante
        DECLARE @FuncionId INT;

        DECLARE func_cursor CURSOR FOR
        SELECT funcion_id
        FROM @FuncionesData;

        OPEN func_cursor;
        FETCH NEXT FROM func_cursor INTO @FuncionId;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            INSERT INTO Participante_Funcion (participante_id, funcion_id)
            VALUES (@id, @FuncionId);

            FETCH NEXT FROM func_cursor INTO @FuncionId;
        END

        CLOSE func_cursor;
        DEALLOCATE func_cursor;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, deshacer la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END


