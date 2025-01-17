﻿use SGII_BackDB

CREATE TABLE Cargo (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME,                      -- dtReg (fecha y hora)
    idPersReg INT,                       -- idPersReg (entero, podría ser una referencia a una persona)
    estado INT,                          -- estado (entero)
    nombre NVARCHAR(255),                -- nombre (cadena de texto, ajusta el tamaño según sea necesario)
    detalle NVARCHAR(1000)               -- detalle (cadena de texto, ajusta el tamaño según sea necesario)
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
    estado NVARCHAR(50),
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


