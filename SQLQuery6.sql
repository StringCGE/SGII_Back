CREATE PROCEDURE RegistrarFactura
    @emisor_id INT,
    @registroFactura_id INT,
    @cliente_id INT,
    @claveAcceso NVARCHAR(50),
    @esFactura BIT,
    @autorizacion NVARCHAR(50),
    @subtotalPrevio DECIMAL(18, 2),
    @subtotal0 DECIMAL(18, 2),
    @descuento DECIMAL(18, 2),
    @subtotal DECIMAL(18, 2),
    @iva DECIMAL(18, 2),
    @total DECIMAL(18, 2),
    @pagoEfectivo DECIMAL(18, 2),
    @pagoTarjetaDebCred DECIMAL(18, 2),
    @pagoOtraForma DECIMAL(18, 2),
    @pagoOtraFormaDetalle NVARCHAR(100),
    @dtReg DATETIME,
    @idPersReg INT,
    @estado NVARCHAR(20),
    @ItemFacturaTemp AS ItemFacturaType READONLY,
    @Result INT OUTPUT, -- Resultado (1 = éxito, 0 = fallo)
    @ErrorMessage NVARCHAR(MAX) OUTPUT -- Mensaje de error
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar factura
        DECLARE @FacturaNotaCreditoID INT;
        INSERT INTO FacturaNotaCredito (
            emisor_id, registroFactura_id, cliente_id, claveAcceso, esFactura, autorizacion,
            subtotalPrevio, subtotal0, descuento, subtotal, iva, total,
            pagoEfectivo, pagoTarjetaDebCred, pagoOtraForma, pagoOtraFormaDetalle,
            dtReg, idPersReg, estado
        )
        VALUES (
            @emisor_id, @registroFactura_id, @cliente_id, @claveAcceso, @esFactura, @autorizacion,
            @subtotalPrevio, @subtotal0, @descuento, @subtotal, @iva, @total,
            @pagoEfectivo, @pagoTarjetaDebCred, @pagoOtraForma, @pagoOtraFormaDetalle,
            @dtReg, @idPersReg, @estado
        );
        SET @FacturaNotaCreditoID = SCOPE_IDENTITY();

        -- Insertar ítems de factura
        INSERT INTO ItemFactura (
            factura_id, cantidad, producto_id, precioUnitario, total, dtReg, idPersReg, estado
        )
        SELECT
            @FacturaNotaCreditoID, cantidad, producto_id, precioUnitario, total, dtReg, idPersReg, estado
        FROM @ItemFacturaTemp;

        -- Insertar datos en RegistroDoc
        INSERT INTO RegistroDoc (
            secuencial, razonSocial, identificacion, fechaEmision, numeroGuiaRemision, codigoNumerico,
            verificador, denomComproModif, numComproModif, comproModif, dtReg, idPersReg, estado
        )
        VALUES (
            @claveAcceso, -- Secuencial (usamos la claveAcceso como ejemplo)
            (SELECT razonSocial FROM Emisor WHERE id = @emisor_id), -- Razón social (esto depende de la estructura de la tabla Emisor)
            (SELECT identificacion FROM Emisor WHERE id = @emisor_id), -- Identificación (esto depende de la estructura de la tabla Emisor)
            @dtReg, -- Fecha de emisión
            NULL, -- NumeroGuiaRemision (Aún no proporcionado, se puede modificar según sea necesario)
            NULL, -- CodigoNumerico (Aún no proporcionado, se puede modificar según sea necesario)
            NULL, -- Verificador (Aún no proporcionado, se puede modificar según sea necesario)
            NULL, -- DenomComproModif (Aún no proporcionado, se puede modificar según sea necesario)
            NULL, -- NumComproModif (Aún no proporcionado, se puede modificar según sea necesario)
            NULL, -- ComproModif (Aún no proporcionado, se puede modificar según sea necesario)
            @dtReg, -- Fecha de registro
            @idPersReg, -- Persona que registró
            @estado -- Estado
        );

        -- Confirmar transacción
        COMMIT TRANSACTION;

        SET @Result = 1; -- Éxito
        SET @ErrorMessage = NULL;
    END TRY
    BEGIN CATCH
        -- Revertir transacción en caso de error
        ROLLBACK TRANSACTION;

        SET @Result = 0; -- Fallo
        SET @ErrorMessage = ERROR_MESSAGE(); -- Mensaje de error
    END CATCH
END;


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