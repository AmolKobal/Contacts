/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [ContactHub]    Script Date: 22-04-2018 12:31:54 ******/
CREATE DATABASE [ContactHub]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ContactHub', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\MSSQL\DATA\ContactHub.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ContactHub_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\MSSQL\DATA\ContactHub_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ContactHub] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ContactHub].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ContactHub] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ContactHub] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ContactHub] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ContactHub] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ContactHub] SET ARITHABORT OFF 
GO
ALTER DATABASE [ContactHub] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ContactHub] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ContactHub] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ContactHub] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ContactHub] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ContactHub] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ContactHub] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ContactHub] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ContactHub] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ContactHub] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ContactHub] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ContactHub] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ContactHub] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ContactHub] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ContactHub] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ContactHub] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ContactHub] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ContactHub] SET RECOVERY FULL 
GO
ALTER DATABASE [ContactHub] SET  MULTI_USER 
GO
ALTER DATABASE [ContactHub] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ContactHub] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ContactHub] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ContactHub] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ContactHub] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ContactHub', N'ON'
GO
ALTER DATABASE [ContactHub] SET QUERY_STORE = OFF
GO
USE [ContactHub]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ContactHub]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 22-04-2018 12:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EMail] [varchar](100) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Status] [bit] NOT NULL,
	[DateCreated] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deleted_Contacts]    Script Date: 22-04-2018 12:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deleted_Contacts](
	[ID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EMail] [varchar](100) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Status] [bit] NOT NULL,
	[DateCreated] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 22-04-2018 12:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[LogDateTime] [datetime] NULL,
	[Module] [varchar](50) NULL,
	[Description] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TempContact]    Script Date: 22-04-2018 12:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[usp_AddContact]    Script Date: 22-04-2018 12:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_AddContact]
(
	@FirstName varchar(50),
	@LastName  varchar(50),
	@EMail  varchar(100),
	@PhoneNumber  varchar(250) 
)
As
Begin

	INSERT INTO Contacts(FirstName, LastName, EMail, PhoneNumber, Status, DateCreated)
	VALUES(@FirstName, @LastName, @EMail, @PhoneNumber, 1, GETDATE())

End

GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteContact]    Script Date: 22-04-2018 12:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_DeleteContact]
(
	@ID int
)
As
Begin

	INSERT INTO Deleted_Contacts(ID, FirstName, LastName, EMail, PhoneNumber, Status, DateCreated)  
	SELECT ID, FirstName, LastName, EMail, PhoneNumber, Status, DateCreated
	FROM Contacts
	WHERE ID = @id;

	Delete FROM Contacts WHERE ID = @id;

End
GO
/****** Object:  StoredProcedure [dbo].[usp_EditContact]    Script Date: 22-04-2018 12:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_EditContact]
(
	@ID int,
	@FirstName varchar(50),
	@LastName  varchar(50),
	@EMail  varchar(100),
	@PhoneNumber  varchar(250),
	@Status bit
)
As
Begin

	UPDATE Contacts
		Set FirstName = @FirstName,
			LastName = @LastName,
			EMail = @EMail,
			PhoneNumber = @PhoneNumber,
			Status = @Status
	WHERE ID = @id;

End
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllContact]    Script Date: 22-04-2018 12:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_GetAllContact]
As
Begin

	SELECT ID, FirstName, LastName, EMail, PhoneNumber, Status, DateCreated
	FROM Contacts; 

End
GO
USE [master]
GO
ALTER DATABASE [ContactHub] SET  READ_WRITE 
GO
