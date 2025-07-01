
 USE master;
-- Drop existing DB if present, This part drops the entire OrderManagementPlatform database
/* GO
DROP DATABASE IF EXISTS [OrderManagementPlatform];
GO
*/
-- Create new DB
CREATE DATABASE [OrderManagementPlatform]
 CONTAINMENT = NONE
 ON  PRIMARY 
( 
    NAME = N'OrderManagementPlatform', 
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\OrderManagementPlatform.mdf',
    SIZE = 8192KB, 
    MAXSIZE = UNLIMITED, 
    FILEGROWTH = 65536KB 
)
 LOG ON 
( 
    NAME = N'OrderManagementPlatform_log', 
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\OrderManagementPlatform_log.ldf', 
    SIZE = 8192KB, 
    MAXSIZE = 2048GB, 
    FILEGROWTH = 65536KB 
)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF;
GO

-- Set compatibility
ALTER DATABASE [OrderManagementPlatform] SET COMPATIBILITY_LEVEL = 160;
GO

-- Enable full-text if installed
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
BEGIN
    EXEC [OrderManagementPlatform].[dbo].[sp_fulltext_database] @action = 'enable';
END
GO

-- Set database options
ALTER DATABASE [OrderManagementPlatform] SET ANSI_NULL_DEFAULT OFF;
ALTER DATABASE [OrderManagementPlatform] SET ANSI_NULLS OFF;
ALTER DATABASE [OrderManagementPlatform] SET ANSI_PADDING OFF;
ALTER DATABASE [OrderManagementPlatform] SET ANSI_WARNINGS OFF;
ALTER DATABASE [OrderManagementPlatform] SET ARITHABORT OFF;
ALTER DATABASE [OrderManagementPlatform] SET AUTO_CLOSE OFF;
ALTER DATABASE [OrderManagementPlatform] SET AUTO_SHRINK OFF;
ALTER DATABASE [OrderManagementPlatform] SET AUTO_UPDATE_STATISTICS ON;
ALTER DATABASE [OrderManagementPlatform] SET CURSOR_CLOSE_ON_COMMIT OFF;
ALTER DATABASE [OrderManagementPlatform] SET CURSOR_DEFAULT GLOBAL;
ALTER DATABASE [OrderManagementPlatform] SET CONCAT_NULL_YIELDS_NULL OFF;
ALTER DATABASE [OrderManagementPlatform] SET NUMERIC_ROUNDABORT OFF;
ALTER DATABASE [OrderManagementPlatform] SET QUOTED_IDENTIFIER OFF;
ALTER DATABASE [OrderManagementPlatform] SET RECURSIVE_TRIGGERS OFF;
ALTER DATABASE [OrderManagementPlatform] SET DISABLE_BROKER;
ALTER DATABASE [OrderManagementPlatform] SET AUTO_UPDATE_STATISTICS_ASYNC OFF;
ALTER DATABASE [OrderManagementPlatform] SET DATE_CORRELATION_OPTIMIZATION OFF;
ALTER DATABASE [OrderManagementPlatform] SET TRUSTWORTHY OFF;
ALTER DATABASE [OrderManagementPlatform] SET ALLOW_SNAPSHOT_ISOLATION OFF;
ALTER DATABASE [OrderManagementPlatform] SET PARAMETERIZATION SIMPLE;
ALTER DATABASE [OrderManagementPlatform] SET READ_COMMITTED_SNAPSHOT OFF;
ALTER DATABASE [OrderManagementPlatform] SET HONOR_BROKER_PRIORITY OFF;
ALTER DATABASE [OrderManagementPlatform] SET RECOVERY SIMPLE;
ALTER DATABASE [OrderManagementPlatform] SET MULTI_USER;
ALTER DATABASE [OrderManagementPlatform] SET PAGE_VERIFY CHECKSUM;
ALTER DATABASE [OrderManagementPlatform] SET DB_CHAINING OFF;
ALTER DATABASE [OrderManagementPlatform] SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF);
ALTER DATABASE [OrderManagementPlatform] SET TARGET_RECOVERY_TIME = 60 SECONDS;
ALTER DATABASE [OrderManagementPlatform] SET DELAYED_DURABILITY = DISABLED;
ALTER DATABASE [OrderManagementPlatform] SET ACCELERATED_DATABASE_RECOVERY = OFF;
ALTER DATABASE [OrderManagementPlatform] SET QUERY_STORE = ON;
ALTER DATABASE [OrderManagementPlatform] SET QUERY_STORE (
    OPERATION_MODE = READ_WRITE,
    CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30),
    DATA_FLUSH_INTERVAL_SECONDS = 900,
    INTERVAL_LENGTH_MINUTES = 60,
    MAX_STORAGE_SIZE_MB = 1000,
    QUERY_CAPTURE_MODE = AUTO,
    SIZE_BASED_CLEANUP_MODE = AUTO,
    MAX_PLANS_PER_QUERY = 200,
    WAIT_STATS_CAPTURE_MODE = ON
);
GO

-- Use the new database
USE [OrderManagementPlatform];
GO

-- Tables
CREATE TABLE [dbo].[__EFMigrationsHistory](
    [MigrationId] NVARCHAR(150) NOT NULL,
    [ProductVersion] NVARCHAR(32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId])
);
GO

CREATE TABLE [dbo].[Users](
    [UserId] INT IDENTITY(1,1) NOT NULL,
    [UserName] VARCHAR(50),
    [UserPassword] VARCHAR(250),
    [Role] NVARCHAR(50) DEFAULT 'User',
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId])
);
GO

CREATE TABLE [dbo].[Categories](
    [CategoryID] INT IDENTITY(1,1) NOT NULL,
    [CategoryName] VARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryID])
);
GO

CREATE TABLE [dbo].[Products](
    [ProductId] INT IDENTITY(1,1) NOT NULL,
    [ProductName] VARCHAR(100),
    [ProductDescription] TEXT,
    [Price] DECIMAL(16,2),
    [Stock] INT,
    [CategoryId] INT,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId])
);
GO

CREATE TABLE [dbo].[Orders](
    [OrderId] INT IDENTITY(1,1) NOT NULL,
    [TotalPrice] DECIMAL(16,2),
    [Date] DATETIME2(7) DEFAULT SYSDATETIME(),
    [Status] VARCHAR(50),
    [UserId] INT,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId])
);
GO

CREATE TABLE [dbo].[OrderItem](
    [Quantity] INT,
    [OrderId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED ([ProductId], [OrderId])
);
GO

CREATE TABLE [dbo].[CartItems](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [ProductId] INT,
    [Quantity] INT NOT NULL,
    [userId] INT,
    CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED ([Id])
);
GO

-- Foreign Keys
ALTER TABLE [dbo].[CartItems] ADD CONSTRAINT [FK_CartItems_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]);
ALTER TABLE [dbo].[CartItems] ADD CONSTRAINT [FK_CartItems_Users] FOREIGN KEY ([userId]) REFERENCES [dbo].[Users] ([UserId]);

ALTER TABLE [dbo].[OrderItem] ADD CONSTRAINT [FK_OrderItem_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]);
ALTER TABLE [dbo].[OrderItem] ADD CONSTRAINT [FK_OrderItem_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]);

ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_Orders_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]);
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryID]);
GO

-- Seed data
INSERT INTO Users (UserName, UserPassword, Role)
VALUES 
('admin', 'NCZ26/F6un29lxriLL/kc0K4Z1QKFMbS7o1ZSmTxdGA=', 'Admin'),
('johndoe', 'DummyHash123', 'User'),
('janedoe', 'DummyHash456', 'User');
GO

INSERT INTO Categories (CategoryName)
VALUES 
('Stationary Fishing'),
('Spinning Fishing'),
('Accessories'),
('Apparel');
GO

INSERT INTO Products (ProductName, ProductDescription, Price, Stock, CategoryId)
VALUES
('Feeder Rod 3.6m', 'Medium-action rod for still water fishing', 129.99, 10, 1),
('Spinning Rod 2.4m', 'Fast action rod for predators', 89.99, 15, 2),
('Fishing Line 150m', 'Braided line 0.12mm', 12.99, 30, 3),
('Thermal Jacket', 'Waterproof thermal jacket for cold sessions', 89.99, 7, 4);
GO

-- One sample order for johndoe (UserId = 2)
INSERT INTO Orders (TotalPrice, Status, UserId)
VALUES (219.98, 'Confirmed', 2);
GO

INSERT INTO OrderItem (OrderId, ProductId, Quantity)
VALUES 
(1, 1, 1),
(1, 2, 1);
GO

-- Cart items for janedoe (UserId = 3)
INSERT INTO CartItems (ProductId, Quantity, userId)
VALUES 
(3, 2, 3),
(4, 1, 3);
GO
