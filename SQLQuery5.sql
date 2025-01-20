use SGII_BackDB;

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
    idApi INT NULL,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (responsable_id) REFERENCES persona(id),
);

