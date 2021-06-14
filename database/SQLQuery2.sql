/****** Object:  Database [GorceryDb1123]    Script Date: 06/06/2021 12:48:39 PM ******/
CREATE DATABASE [GorceryDb1123]   WITH CATALOG_COLLATION = DATABASE_DEFAULT;
GO
ALTER DATABASE [GorceryDb1123] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [GorceryDb1123] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GorceryDb1123] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GorceryDb1123] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GorceryDb1123] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GorceryDb1123] SET ARITHABORT OFF 
GO
ALTER DATABASE [GorceryDb1123] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GorceryDb1123] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GorceryDb1123] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GorceryDb1123] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GorceryDb1123] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GorceryDb1123] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GorceryDb1123] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GorceryDb1123] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GorceryDb1123] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GorceryDb1123] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GorceryDb1123] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [GorceryDb1123] SET  MULTI_USER 
GO
ALTER DATABASE [GorceryDb1123] SET QUERY_STORE = OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 06/06/2021 12:48:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Stock] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[Catagory] [nvarchar](max) NULL,
	[StoreId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Link] [nvarchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 06/06/2021 12:48:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreName] [nvarchar](max) NULL,
	[Latitude] [nvarchar](max) NULL,
	[Longitude] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
	[StoreCategoryId] [int] NOT NULL,
	[Region] [int] NOT NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreCategory]    Script Date: 06/06/2021 12:48:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](max) NULL,
 CONSTRAINT [PK_StoreCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/06/2021 12:48:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[UserType] [nvarchar](max) NULL,
	[Latitude] [nvarchar](max) NULL,
	[Longitude] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([Id], [Name], [Stock], [Price], [Catagory], [StoreId], [UserId], [Link]) VALUES (1, N'Gtx1060', 35, 123, N'GPU', 2, 2, N'https://www.amazon.com/Asus-Phoenix-PH-GTX1060-3G-GeForce-Graphic/dp/B07VVMGP4G/ref=sr_1_10?dchild=1&keywords=gtx+1060&qid=1622466554&sr=8-10')
GO
INSERT [dbo].[Product] ([Id], [Name], [Stock], [Price], [Catagory], [StoreId], [UserId], [Link]) VALUES (2, N'S9', 13, 123, N'Phone', 1, 1, N'https://www.amazon.com/Samsung-Galaxy-S9-Display-Resistance/dp/B07WVRJQ7V/ref=sr_1_2?crid=2BXW75J7J6OU&dchild=1&keywords=samsung+s9&qid=1622466509&sprefix=samsung+s9%2Caps%2C445&sr=8-2')
GO
INSERT [dbo].[Product] ([Id], [Name], [Stock], [Price], [Catagory], [StoreId], [UserId], [Link]) VALUES (3, N'S10', 123, 123, N'Phone', 1, 1, N'https://www.daraz.pk/products/samsung-s10-lite-8gb-ram-128gb-rom-4500-mah-battery-prism-white-i136130385-s1296426893.html?spm=a2a0e.searchlist.list.1.50bb5130LSO5Iz&search=1')
GO
INSERT [dbo].[Product] ([Id], [Name], [Stock], [Price], [Catagory], [StoreId], [UserId], [Link]) VALUES (4, N'Gtx1050ti', 35, 123, N'GPU', 2, 2, N'https://www.amazon.com/ASUS-Geforce-Phoenix-Graphics-PH-GTX1050TI-4G/dp/B01M360WG6/ref=sr_1_1?dchild=1&keywords=gtx+1050ti&qid=1622530878&sr=8-1')
GO
INSERT [dbo].[Product] ([Id], [Name], [Stock], [Price], [Catagory], [StoreId], [UserId], [Link]) VALUES (5, N'PS4', 35, 123, N'Console', 3, 3, N'https://www.amazon.com/PlayStation-Console-Light-System-Greatest-4/dp/B077QT6K94/ref=sr_1_2?dchild=1&keywords=ps4&qid=1622530950&sr=8-2')
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Store] ON 
GO
INSERT [dbo].[Store] ([Id], [StoreName], [Latitude], [Longitude], [UserId], [StoreCategoryId], [Region]) VALUES (1, N'NewMart', NULL, NULL, 3, 1, 3)
GO
INSERT [dbo].[Store] ([Id], [StoreName], [Latitude], [Longitude], [UserId], [StoreCategoryId], [Region]) VALUES (2, N'DMart', NULL, NULL, 2, 1, 2)
GO
INSERT [dbo].[Store] ([Id], [StoreName], [Latitude], [Longitude], [UserId], [StoreCategoryId], [Region]) VALUES (3, N'ExcelMart', NULL, NULL, 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Store] OFF
GO
SET IDENTITY_INSERT [dbo].[StoreCategory] ON 
GO
INSERT [dbo].[StoreCategory] ([Id], [Category]) VALUES (1, N'Sanitory')
GO
INSERT [dbo].[StoreCategory] ([Id], [Category]) VALUES (2, N'Electronics')
GO
SET IDENTITY_INSERT [dbo].[StoreCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [UserName], [Password], [Email], [Phone], [UserType], [Latitude], [Longitude]) VALUES (1, NULL, NULL, N'Henry', N'1234', N'henry@gamil.com', N'4234112', N'Seller', N'41234214', N'3412341324')
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [UserName], [Password], [Email], [Phone], [UserType], [Latitude], [Longitude]) VALUES (2, NULL, NULL, N'James', N'1234', N'james@gamil.com', N'4234112', N'Customer', N'41234214', N'3412341324')
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [UserName], [Password], [Email], [Phone], [UserType], [Latitude], [Longitude]) VALUES (3, NULL, NULL, N'William', N'1234', N'william@gamil.com', N'4234112', N'Seller', N'41234214', N'3412341324')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER DATABASE [GorceryDb1123] SET  READ_WRITE 
GO
