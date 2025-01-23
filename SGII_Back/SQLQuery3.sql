﻿use SGII_BackDB

DROP PROCEDURE RegistrarFactura

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



