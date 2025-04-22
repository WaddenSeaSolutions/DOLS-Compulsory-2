Create Database Dols;

CREATE TABLE Notes (
                       Id INT PRIMARY KEY IDENTITY(1,1),
                       Title NVARCHAR(255) NOT NULL,
                       Content NVARCHAR(MAX) NOT NULL,
                       CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE()
);