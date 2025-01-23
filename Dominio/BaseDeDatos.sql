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
CREATE TABLE Emisor (
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
CREATE TABLE Producto (
    proveedor_id INT NULL,
    nombre NVARCHAR(255) NULL,
    detalle NVARCHAR(255) NULL,
    precio FLOAT NULL,
    cantidad INT NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (proveedor_id) REFERENCES emisor(id),
);
CREATE TABLE EmisorItem (
    emisor_id INT NULL,
    emisorEstablecimiento_id INT NULL,
    puntoEmision NVARCHAR(255) NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (emisor_id) REFERENCES emisor(id),
    FOREIGN KEY (emisorEstablecimiento_id) REFERENCES emisorEstablecimiento(id),
);
CREATE TABLE Cliente (
    persona_id INT NULL,
    identificacion NVARCHAR(255) NULL,
    tipoIdentificacion_id INT NULL,
    telefono NVARCHAR(255) NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (persona_id) REFERENCES persona(id),
    FOREIGN KEY (tipoIdentificacion_id) REFERENCES tipoIdentificacion(id),
);
CREATE TABLE TipoIdentificacion (
    nombre NVARCHAR(255) NULL,
    detalle NVARCHAR(255) NULL,
    pais_id INT NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (pais_id) REFERENCES nacionalidad(id),
);
CREATE TABLE EmisorEstablecimiento (
    emisor_id INT NULL,
    numero INT NULL,
    nombre NVARCHAR(255) NULL,
    direccion NVARCHAR(255) NULL,
    puntosDeEmision NVARCHAR(255) NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (emisor_id) REFERENCES emisor(id),
);
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
    comproModif_id INT NULL,
    id INT IDENTITY(1,1) PRIMARY KEY,
    dtReg DATETIME NULL,
    idPersReg INT NULL,
    estado INT NULL,
    FOREIGN KEY (comproModif_id) REFERENCES facturaNotaCredito(id),
);
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
