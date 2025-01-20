

use SGII_BackDB;

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