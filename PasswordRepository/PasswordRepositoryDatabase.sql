CREATE DATABASE [PasswordRepository]
GO

USE [PasswordRepository]
GO

CREATE TABLE Accounts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    AccountName NVARCHAR(100) UNIQUE NOT NULL,
    LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
    Comments NVARCHAR(500),
    PasswordHash NVARCHAR(500) NOT NULL
);

INSERT INTO Accounts VALUES('Account1', '2024-10-10', 'Comment1; Comment2;', 'TAvAy3oEPKN6MUi2icXTBg==');
INSERT INTO Accounts VALUES('Account2', '2024-10-10', 'Comment3; Comment4;', 'TAvAy3oEPKN6MUi2icXTBg==');
INSERT INTO Accounts VALUES('Account3', '2024-10-10', 'Comment5; Comment6;', 'TAvAy3oEPKN6MUi2icXTBg==');
INSERT INTO Accounts VALUES('Account4', '2024-10-10', 'Comment7; Comment8;', 'TAvAy3oEPKN6MUi2icXTBg==');
INSERT INTO Accounts VALUES('Account5', '2024-10-10', 'Comment9; Comment10;', 'TAvAy3oEPKN6MUi2icXTBg==');
INSERT INTO Accounts VALUES('Account6', '2024-10-10', 'Comment10; Comment11; Comment12; Comment13;', 'TAvAy3oEPKN6MUi2icXTBg==');