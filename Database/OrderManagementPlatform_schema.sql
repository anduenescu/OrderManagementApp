USE [master]
GO
/****** Object:  Database [OrderManagementPlatform]    Script Date: 6/28/2025 1:49:34 PM ******/
CREATE DATABASE [OrderManagementPlatform]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OrderManagementPlatform', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\OrderManagementPlatform.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OrderManagementPlatform_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\OrderManagementPlatform_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [OrderManagementPlatform] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OrderManagementPlatform].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OrderManagementPlatform] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET ARITHABORT OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OrderManagementPlatform] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OrderManagementPlatform] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OrderManagementPlatform] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OrderManagementPlatform] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OrderManagementPlatform] SET  MULTI_USER 
GO
ALTER DATABASE [OrderManagementPlatform] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OrderManagementPlatform] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OrderManagementPlatform] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OrderManagementPlatform] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OrderManagementPlatform] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OrderManagementPlatform] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OrderManagementPlatform] SET QUERY_STORE = ON
GO
ALTER DATABASE [OrderManagementPlatform] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [OrderManagementPlatform]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/28/2025 1:49:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/28/2025 1:49:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserPassword] [varchar](250) NULL,
	[Role] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (sysdatetime()) FOR [Date]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('User') FOR [Role]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_User] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_User]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_Orders]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_UserId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Category]
GO
USE [master]
GO
ALTER DATABASE [OrderManagementPlatform] SET  READ_WRITE 
GO
	
/****** Object:  Table [dbo].[CartItems]    Script Date: 6/28/2025 1:49:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NOT NULL,
	[userId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/28/2025 1:49:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 6/28/2025 1:49:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[Quantity] [int] NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 6/28/2025 1:49:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[TotalPrice] [decimal](16, 2) NULL,
	[Date] [datetime2](7) NULL,
	[Status] [varchar](50) NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 6/28/2025 1:49:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](100) NULL,
	[ProductDescription] [text] NULL,
	[Price] [decimal](16, 2) NULL,
	[Stock] [int] NULL,
	[CategoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


-- Users (Admin + sample users)
INSERT INTO Users (UserName, UserPassword, Role)
VALUES 
('admin', 'NCZ26/F6un29lxriLL/kc0K4Z1QKFMbS7o1ZSmTxdGA=', 'Admin'), -- hashed 'adminpassword'
('johndoe', 'DummyHash123', 'User'),
('janedoe', 'DummyHash456', 'User');

-- Categories
INSERT INTO Categories (CategoryName)
VALUES 
('Stationary Fishing'),  -- ID 1
('Spinning Fishing'),    -- ID 2
('Accessories'),         -- ID 3
('Apparel');             -- ID 4

-- Products
INSERT INTO Products (ProductName, ProductDescription, Price, Stock, CategoryId)
VALUES
('Feeder Rod 3.6m', 'Medium-action rod for still water fishing', 129.99, 10, 1),
('Spinning Rod 2.4m', 'Fast action rod for predators', 89.99, 15, 2),
('Fishing Line 150m', 'Braided line 0.12mm', 12.99, 30, 3),
('Thermal Jacket', 'Waterproof thermal jacket for cold sessions', 89.99, 7, 4);

-- Orders
-- UserId 2 (johndoe) places an order with 2 items
INSERT INTO Orders (TotalPrice, Status, UserId)
VALUES (219.98, 'Confirmed', 2);

-- Order Items (OrderId 1, ProductId 1 and 2)
INSERT INTO OrderItem (OrderId, ProductId, Quantity)
VALUES 
(1, 1, 1),  -- Feeder Rod x1
(1, 2, 1);  -- Spinning Rod x1

-- Cart Items (not linked to an order)
-- UserId 3 (janedoe) has items in her cart
INSERT INTO CartItems (ProductId, Quantity, userId)
VALUES 
(3, 2, 3),  -- Fishing Line x2
(4, 1, 3);  -- Thermal Jacket x1



