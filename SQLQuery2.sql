use SGII_BackDB;

Drop Table RegistroDoc;

CREATE TABLE RegistroDoc (
    secuencial NVARCHAR(255) NULL,
    razonSocial NVARCHAR(255) NULL,
    identificacion NVARCHAR(255) NULL,
    fechaEmision NVARCHAR(255) NULL,
    numeroGuiaRemision NVARCHAR(255) NULL,
    codigoNumerico NVARCHAR(255) NULL,
    verificador NVARCHAR(255) NULL,
    denomComproModif INT NULL,
    numComproModif NVARCHAR(255) NULL,
    comproModif INT NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,

);
Drop Table FacturaNotaCredito;
CREATE TABLE FacturaNotaCredito (
    emisor_id INT NULL,
    registroFactura_id INT NULL,
    cliente_id INT NULL,
    claveAcceso NVARCHAR(255) NULL,
    esFactura BIT,
    autorizacion NVARCHAR(255) NULL,
    subtotalPrevio FLOAT NULL,
    subtotal0 FLOAT NULL,
    descuento FLOAT NULL,
    subtotal FLOAT NULL,
    iva FLOAT NULL,
    total FLOAT NULL,
    pagoEfectivo FLOAT NULL,
    pagoTarjetaDebCred FLOAT NULL,
    pagoOtraForma FLOAT NULL,
    pagoOtraFormaDetalle NVARCHAR(255) NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (emisor_id) REFERENCES emisorItem(id),
    FOREIGN KEY (registroFactura_id) REFERENCES registroDoc(id),
    FOREIGN KEY (cliente_id) REFERENCES cliente(id),
);
Drop Table ItemFacturaNotaCredito;
CREATE TABLE ItemFacturaNotaCredito (
    facturaNotaCredito_id INT NULL,
    cantidad INT NULL,
    producto_id INT NULL,
    precioUnitario FLOAT NULL,
    total FLOAT NULL,
    tipoTransac INT NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (facturaNotaCredito_id) REFERENCES facturaNotaCredito(id),
    FOREIGN KEY (producto_id) REFERENCES producto(id),
);