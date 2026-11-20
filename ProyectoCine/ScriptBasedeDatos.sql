CREATE DATABASE CineDB;
GO

USE CineDB;
GO

CREATE TABLE dbo.Funciones (    
Id        INT IDENTITY(1,1) PRIMARY KEY,    
Nombre    NVARCHAR(100)  NOT NULL,  
Sala      NVARCHAR(100)  NOT NULL,
Precio    DECIMAL(10,2)  NOT NULL 
);

SELECT * FROM dbo.Funciones;