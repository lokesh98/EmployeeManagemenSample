USE [master]
GO
/****** Object:  Database [EmployeeDB]    Script Date: 6/27/2020 5:45:58 PM ******/
CREATE DATABASE [EmployeeDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EmployeeDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\EmployeeDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EmployeeDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\EmployeeDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EmployeeDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmployeeDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmployeeDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmployeeDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmployeeDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmployeeDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmployeeDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmployeeDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EmployeeDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmployeeDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeeDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmployeeDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmployeeDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmployeeDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmployeeDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmployeeDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmployeeDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EmployeeDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmployeeDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmployeeDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmployeeDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmployeeDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmployeeDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmployeeDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmployeeDB] SET RECOVERY FULL 
GO
ALTER DATABASE [EmployeeDB] SET  MULTI_USER 
GO
ALTER DATABASE [EmployeeDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmployeeDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmployeeDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmployeeDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [EmployeeDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'EmployeeDB', N'ON'
GO
USE [EmployeeDB]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 6/27/2020 5:45:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](150) NULL,
	[DateofBirth] [date] NULL,
	[Gender] [varchar](50) NULL,
	[Salary] [decimal](18, 2) NULL,
	[Designation] [varchar](50) NULL,
	[ImageUrl] [varchar](50) NULL,
	[ImportedDate] [datetime] NULL,
	[ImportedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/27/2020 5:45:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [nvarchar](30) NULL,
	[email] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[UserGroupId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 6/27/2020 5:45:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserGroup](
	[UserGroupId] [int] IDENTITY(1,1) NOT NULL,
	[UserGroupName] [varchar](50) NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[UserGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Lokesh Chand', CAST(N'2020-01-02' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Developer', N'Content/img/ml1204028158.PNG', CAST(N'2020-06-27 12:53:10.677' AS DateTime), 1, 1, CAST(N'2020-06-27 17:42:27.643' AS DateTime))
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Rita Shahi', CAST(N'2020-06-11' AS Date), N'Male', CAST(3000.00 AS Decimal(18, 2)), N'QA', N'Content/img/ml1201225179.PNG', CAST(N'2020-06-27 12:53:10.700' AS DateTime), 1, 1, CAST(N'2020-06-27 14:37:13.787' AS DateTime))
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (3, N'Kabita Sing', CAST(N'2011-02-01' AS Date), N'Female', CAST(1100.00 AS Decimal(18, 2)), N'HR', N'Content/img/khalti203340870.PNG', CAST(N'2020-06-27 12:53:10.703' AS DateTime), 1, 1, CAST(N'2020-06-27 14:33:40.877' AS DateTime))
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (4, N'Suman', CAST(N'2020-01-02' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Operator', NULL, CAST(N'2020-06-27 12:53:10.703' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (5, N'Mahesh', CAST(N'2020-01-03' AS Date), N'Male', CAST(1400.00 AS Decimal(18, 2)), N'Office Support', N'Content/img/401-unauthorized-error201242206.jpg', CAST(N'2020-06-27 12:53:10.703' AS DateTime), 1, 1, CAST(N'2020-06-27 13:12:42.213' AS DateTime))
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (6, N'Rohit', CAST(N'2020-01-04' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Designer', NULL, CAST(N'2020-06-27 12:53:10.703' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (7, N'Lokesh Chand', CAST(N'2020-01-02' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Developer', NULL, CAST(N'2020-06-27 12:56:03.757' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (8, N'Rita Shahi', CAST(N'2020-01-02' AS Date), N'Male', CAST(3000.00 AS Decimal(18, 2)), N'QA', NULL, CAST(N'2020-06-27 12:56:03.757' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (9, N'Kabita Sing', CAST(N'2011-02-01' AS Date), N'Female', CAST(1100.00 AS Decimal(18, 2)), N'HR', NULL, CAST(N'2020-06-27 12:56:03.757' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (10, N'Suman', CAST(N'2020-01-02' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Operator', NULL, CAST(N'2020-06-27 12:56:03.757' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (11, N'Mahesh', CAST(N'2020-01-03' AS Date), N'Male', CAST(1400.00 AS Decimal(18, 2)), N'Office Support', NULL, CAST(N'2020-06-27 12:56:03.757' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (12, N'Rohit', CAST(N'2020-01-04' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Designer', NULL, CAST(N'2020-06-27 12:56:03.757' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (13, N'Lokesh Chand', CAST(N'2020-01-02' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Developer', NULL, CAST(N'2020-06-27 14:33:20.637' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (14, N'Rita Shahi', CAST(N'2020-01-02' AS Date), N'Male', CAST(3000.00 AS Decimal(18, 2)), N'QA', NULL, CAST(N'2020-06-27 14:33:20.690' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (15, N'Kabita Sing', CAST(N'2011-02-01' AS Date), N'Female', CAST(1100.00 AS Decimal(18, 2)), N'HR', NULL, CAST(N'2020-06-27 14:33:20.690' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (16, N'Suman', CAST(N'2020-01-02' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Operator', NULL, CAST(N'2020-06-27 14:33:20.690' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (17, N'Mahesh', CAST(N'2020-01-03' AS Date), N'Male', CAST(1400.00 AS Decimal(18, 2)), N'Office Support', NULL, CAST(N'2020-06-27 14:33:20.690' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (18, N'Rohit', CAST(N'2020-01-04' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Designer', NULL, CAST(N'2020-06-27 14:33:20.690' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (19, N'fsdf', CAST(N'2020-06-18' AS Date), N'Male', CAST(423423.00 AS Decimal(18, 2)), N'TEST', N'Content/img/IsPostBack_ASPNET204017513.PNG', CAST(N'2020-06-27 14:40:17.517' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (20, N'Lokesh Chand', CAST(N'2020-01-02' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Developer', NULL, CAST(N'2020-06-27 17:35:45.690' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (21, N'Rita Shahi', CAST(N'2020-01-02' AS Date), N'Male', CAST(3000.00 AS Decimal(18, 2)), N'QA', NULL, CAST(N'2020-06-27 17:35:45.717' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (22, N'Kabita Sing', CAST(N'2011-02-01' AS Date), N'Female', CAST(1100.00 AS Decimal(18, 2)), N'HR', NULL, CAST(N'2020-06-27 17:35:45.717' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (23, N'Suman', CAST(N'2020-01-02' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Operator', NULL, CAST(N'2020-06-27 17:35:45.717' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (24, N'Mahesh', CAST(N'2020-01-03' AS Date), N'Male', CAST(1400.00 AS Decimal(18, 2)), N'Office Support', NULL, CAST(N'2020-06-27 17:35:45.717' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeId], [FullName], [DateofBirth], [Gender], [Salary], [Designation], [ImageUrl], [ImportedDate], [ImportedBy], [ModifiedBy], [ModifiedDate]) VALUES (25, N'Rohit', CAST(N'2020-01-04' AS Date), N'Male', CAST(20000.00 AS Decimal(18, 2)), N'Designer', NULL, CAST(N'2020-06-27 17:35:45.717' AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Employee] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [Password], [email], [CreatedDate], [UserGroupId]) VALUES (1, N'Admin', N'admin', N'admin@gmail.com', CAST(N'2020-03-04 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[User] ([UserId], [UserName], [Password], [email], [CreatedDate], [UserGroupId]) VALUES (2, N'User', N'user', N'user@gmail.com', CAST(N'2020-03-04 00:00:00.000' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[UserGroup] ON 

INSERT [dbo].[UserGroup] ([UserGroupId], [UserGroupName]) VALUES (1, N'Admin')
INSERT [dbo].[UserGroup] ([UserGroupId], [UserGroupName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[UserGroup] OFF
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_User] FOREIGN KEY([ImportedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroup] ([UserGroupId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserGroup]
GO
USE [master]
GO
ALTER DATABASE [EmployeeDB] SET  READ_WRITE 
GO
