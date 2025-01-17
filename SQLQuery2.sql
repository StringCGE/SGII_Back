
USE SGII_BackDB;
GO

-- Crear tabla para almacenar los datos de los colaboradores
CREATE TABLE Colaboradores (
    id INT IDENTITY(1,1) PRIMARY KEY,                    -- ID único, autoincremental
    idPersona INT NULL,                                   -- ID de la persona asignada (puede ser NULL)
    idCargo INT NULL,                                     -- ID del cargo del colaborador
    sueldo FLOAT NULL,                                    -- Sueldo del colaborador (puede ser NULL)
    estadoLaboral NVARCHAR(50) NULL,                      -- Estado laboral (puede ser NULL)
    tipoContrato NVARCHAR(50) NULL,                       -- Tipo de contrato (puede ser NULL)
    horasTrabajo INT NULL,                                -- Horas de trabajo (puede ser NULL)
    contrato NVARCHAR(255) NULL,                          -- Ruta del archivo de contrato (puede ser NULL)
    idTipoColab INT NULL,                                 -- ID del tipo de colaborador (puede ser NULL)
    
    -- Campos heredados de ClsDbObj
    idApi INT NULL,                                       -- ID en API
    dtReg DATETIME NULL,                                  -- Fecha de registro
    idPersReg INT NULL,                                   -- ID de la persona que registra
    estado INT NULL,                                      -- Estado del colaborador
    take INT NULL,                                        -- Cantidad de datos a obtener (si aplica)
    offsetDT DATETIME NULL,                               -- Offset de fecha y hora
     
    -- Campos heredados de ClsAsignacion
    fechaDeAsignacion DATETIME NULL,                      -- Fecha de asignación
    inicioAsignacion DATETIME NULL,                       -- Fecha de inicio de la asignación
    finAsignacion DATETIME NULL                           -- Fecha de fin de la asignación
);
GO