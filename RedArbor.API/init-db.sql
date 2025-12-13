-- Esperar a que SQL Server esté completamente iniciado
WAITFOR DELAY '00:00:05';
GO

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DBReadArbor')
BEGIN
    CREATE DATABASE DBReadArbor;
END
GO

-- Usar la base de datos
USE DBReadArbor;
GO

-- Crear la tabla Employes si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employes')
BEGIN
    CREATE TABLE Employes (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        CompanyId INT NOT NULL,
        CreatedOn DATETIME NULL,
        DeleteOn DATETIME NULL,
        Email NVARCHAR(50) NOT NULL,
        Fax NVARCHAR(50) NULL,
        Name NVARCHAR(50) NULL,
        Lastlogin DATETIME NULL,
        Password NVARCHAR(50) NOT NULL,
        PortalId INT NOT NULL,
        RoleId INT NOT NULL,
        StatusId INT NOT NULL,
        Telephone NVARCHAR(50) NULL,
        UpdatedOn DATETIME NULL,
        Username NVARCHAR(50) NOT NULL
    );

    -- Insertar datos de prueba
    INSERT INTO Employes (CompanyId, Email, Password, PortalId, RoleId, StatusId, Username, Name, CreatedOn)
    VALUES 
    (1, 'admin@redarbor.com', 'admin123', 1, 1, 1, 'admin', 'Administrator', GETDATE()),
    (1, 'test1@test.test.tmp', 'test', 1, 1, 1, 'test1', 'Test User', GETDATE());

    PRINT 'Tabla Employes creada y datos de prueba insertados correctamente.';
END
ELSE
BEGIN
    PRINT 'La tabla Employes ya existe.';
END
GO