USE [nopLocalHolmberg]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[ProductFileMap_ProductFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product_File_Mapping]'))
ALTER TABLE [dbo].[Product_File_Mapping] DROP CONSTRAINT [ProductFileMap_ProductFile]
GO

USE [nopLocalHolmberg]
GO

/****** Object:  Table [dbo].[Product_File_Mapping]    Script Date: 10/21/2012 19:44:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product_File_Mapping]') AND type in (N'U'))
DROP TABLE [dbo].[Product_File_Mapping]
GO

USE [nopLocalHolmberg]
GO

/****** Object:  Table [dbo].[Product_File_Mapping]    Script Date: 10/21/2012 19:44:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product_File_Mapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[ProductFile_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Product_File_Mapping]  WITH CHECK ADD  CONSTRAINT [ProductFileMap_ProductFile] FOREIGN KEY([ProductFile_Id])
REFERENCES [dbo].[ProductFiles] ([Id])
GO

ALTER TABLE [dbo].[Product_File_Mapping] CHECK CONSTRAINT [ProductFileMap_ProductFile]
GO


USE [nopLocalHolmberg]
GO

/****** Object:  Table [dbo].[ProductFiles]    Script Date: 10/21/2012 19:45:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductFiles]') AND type in (N'U'))
DROP TABLE [dbo].[ProductFiles]
GO

USE [nopLocalHolmberg]
GO

/****** Object:  Table [dbo].[ProductFiles]    Script Date: 10/21/2012 19:45:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DownloadGuid] [uniqueidentifier] NOT NULL,
	[UseDownloadUrl] [bit] NOT NULL,
	[DownloadUrl] [nvarchar](max) NULL,
	[DownloadBinary] [varbinary](max) NULL,
	[ContentType] [nvarchar](max) NULL,
	[Filename] [nvarchar](max) NULL,
	[Extension] [nvarchar](max) NULL,
	[IsNew] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


