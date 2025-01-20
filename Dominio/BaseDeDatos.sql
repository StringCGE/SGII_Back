CREATE TABLE DbObj (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,

);
CREATE TABLE Sexo (
    nombre NVARCHAR(255) NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,

);
CREATE TABLE Nacionalidad (
    nombre NVARCHAR(255) NULL,
    detalle NVARCHAR(255) NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,

);
CREATE TABLE EstadoCivil (
    nombre NVARCHAR(255) NULL,
    detalle NVARCHAR(255) NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,

);
CREATE TABLE Persona (
    nombre1 NVARCHAR(255) NULL,
    nombre2 NVARCHAR(255) NULL,
    apellido1 NVARCHAR(255) NULL,
    apellido2 NVARCHAR(255) NULL,
    fechaNacimiento DATETIME NULL,
    cedula NVARCHAR(255) NULL,
    sexo_id INT NULL,
    estadoCivil_id INT NULL,
    nacionalidad_id INT NULL,
    grupoSanguineo NVARCHAR(255) NULL,
    tipoSanguineo NVARCHAR(255) NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (sexo_id) REFERENCES sexo(id),
    FOREIGN KEY (estadoCivil_id) REFERENCES estadoCivil(id),
    FOREIGN KEY (nacionalidad_id) REFERENCES nacionalidad(id),
);
CREATE TABLE Proveedor (
    razonSocial NVARCHAR(255) NULL,
    ruc NVARCHAR(255) NULL,
    responsable_id INT NULL,
    telefonoResponsable NVARCHAR(255) NULL,
    direccionMatriz NVARCHAR(255) NULL,
    telefono1 NVARCHAR(255) NULL,
    telefono2 NVARCHAR(255) NULL,
    email NVARCHAR(255) NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (responsable_id) REFERENCES persona(id),
);
CREATE TABLE User (
    persona_id INT NULL,
    email NVARCHAR(255) NULL,
    urlFoto NVARCHAR(255) NULL,
    role NVARCHAR(255) NULL,
    password NVARCHAR(255) NULL,
    salt NVARCHAR(255) NULL,
    tempCode NVARCHAR(255) NULL,
    tempCodeCreateAt DATETIME NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (persona_id) REFERENCES persona(id),
);
