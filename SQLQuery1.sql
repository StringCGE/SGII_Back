use SGII_BackDB;

CREATE TABLE EstadoLaboral (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    nombre NVARCHAR(MAX) NULL
);

CREATE TABLE CondicionLaboral (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    nombre NVARCHAR(MAX) NULL
);

DROP TABLE TipoColab;
CREATE TABLE TipoColab (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME,
    idPersReg INT,
    estado INT,
    nombre NVARCHAR(255),
    detalle NVARCHAR(1000)
);



