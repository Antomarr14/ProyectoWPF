-- Crear una base de datos llamada 'MiBaseDeDatos'
CREATE DATABASE SchoolShop;
GO

-- Usar la base de datos 'MiBaseDeDatos'
USE SchoolShop;
GO

-- Crear la tabla 'Productos'
CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),          -- Campo 'Id' como clave primaria y con incremento autom�tico
    Nombre NVARCHAR(100) NOT NULL,             -- Campo 'Nombre' con un tama�o m�ximo de 100 caracteres
    PrecioUnitario DECIMAL(18, 2) NOT NULL     -- Campo 'PrecioUnitario' con precisi�n de 18 d�gitos y 2 decimales
);
GO

-- Opcional: Insertar algunos datos de ejemplo en la tabla 'Productos'
INSERT INTO Productos (Nombre, PrecioUnitario) VALUES ('Producto A', 10.50);
INSERT INTO Productos (Nombre, PrecioUnitario) VALUES ('Producto B', 20.00);
INSERT INTO Productos (Nombre, PrecioUnitario) VALUES ('Producto C', 30.75);
GO
CREATE PROCEDURE SPGuardarProductos
    @Nombre NVARCHAR(100),
    @PrecioUnitario DECIMAL(18, 2)
AS
BEGIN
    -- Manejo de errores
    BEGIN TRY
        -- Iniciar la transacci�n
        BEGIN TRANSACTION;

        -- Insertar el producto en la tabla Productos
        INSERT INTO Productos (Nombre, PrecioUnitario)
        VALUES (@Nombre, @PrecioUnitario);

        -- Confirmar la transacci�n si todo es correcto
        COMMIT TRANSACTION;

        -- Devolver el n�mero de filas afectadas
        RETURN @@ROWCOUNT;
    END TRY
    BEGIN CATCH
        -- Si hay un error, deshacer la transacci�n
        ROLLBACK TRANSACTION;

        -- Devolver el n�mero de error
        RETURN ERROR_NUMBER();
    END CATCH
END;
Go
CREATE PROCEDURE SPMostrarProductos
AS
BEGIN
    -- Recuperar todos los productos de la tabla
    SELECT Id, Nombre, PrecioUnitario
    FROM Productos
END
Go
CREATE PROCEDURE SPModificarProductos
    @Id INT,
    @Nombre NVARCHAR(100),
    @PrecioUnitario DECIMAL(18, 2)
AS
BEGIN
    UPDATE Productos
    SET Nombre = @Nombre,
        PrecioUnitario = @PrecioUnitario
    WHERE Id = @Id
END
Go
CREATE PROCEDURE SPEliminarProductos
    @Id INT
AS
BEGIN
    -- Iniciar una transacci�n para garantizar la consistencia
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar el producto con el ID proporcionado
        DELETE FROM Productos
        WHERE Id = @Id;

        -- Confirmar la transacci�n si no hubo errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurri� un error, deshacer la transacci�n
        ROLLBACK TRANSACTION;

        -- Propagar el error para que pueda ser manejado por la capa de aplicaci�n
        THROW;
    END CATCH
END





select*from Productos