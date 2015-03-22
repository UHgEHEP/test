﻿/*
Deployment script for Alicargo_Files

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Alicargo_Files"
:setvar DefaultFilePrefix "Alicargo_Files"
:setvar DefaultDataPath "A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[IdsTable]...';


GO
CREATE TYPE [dbo].[IdsTable] AS TABLE (
    [Id] BIGINT NOT NULL);


GO
PRINT N'Creating [dbo].[ApplicationFile]...';


GO
CREATE TABLE [dbo].[ApplicationFile] (
    [Id]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [ApplicationId] BIGINT          NOT NULL,
    [TypeId]        INT             NOT NULL,
    [Name]          NVARCHAR (320)  NOT NULL,
    [Data]          VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.ApplicationFile] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[AwbFile]...';


GO
CREATE TABLE [dbo].[AwbFile] (
    [Id]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [AwbId]  BIGINT          NOT NULL,
    [TypeId] INT             NOT NULL,
    [Name]   NVARCHAR (320)  NOT NULL,
    [Data]   VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.AwbFile] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[ClientContract]...';


GO
CREATE TABLE [dbo].[ClientContract] (
    [Id]              BIGINT             IDENTITY (1, 1) NOT NULL,
    [UpdateTimestamp] DATETIMEOFFSET (7) NOT NULL,
    [ClientId]        BIGINT             NOT NULL,
    [Data]            VARBINARY (MAX)    NOT NULL,
    [Name]            NVARCHAR (320)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[ClientContract].[IX_ClientContract_ClientId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ClientContract_ClientId]
    ON [dbo].[ClientContract]([ClientId] ASC);


GO
PRINT N'Creating Default Constraint on [dbo].[ClientContract]....';


GO
ALTER TABLE [dbo].[ClientContract]
    ADD DEFAULT (GETUTCDATE()) FOR [UpdateTimestamp];


GO
PRINT N'Creating [dbo].[ApplicationFile_Add]...';


GO
CREATE PROCEDURE [dbo].[ApplicationFile_Add]
	@ApplicationId BIGINT,
	@TypeId INT,
	@Name NVARCHAR(320),
	@Data VARBINARY(MAX) 

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[ApplicationFile] ([ApplicationId], [Data], [Name], [TypeId])
	OUTPUT INSERTED.[Id]
	VALUES (@ApplicationId, @Data, @Name, @TypeId)

END
GO
PRINT N'Creating [dbo].[ApplicationFile_Delete]...';


GO
CREATE PROCEDURE [dbo].[ApplicationFile_Delete]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[ApplicationFile]
	WHERE [Id] = @Id

END
GO
PRINT N'Creating [dbo].[ApplicationFile_Get]...';


GO
CREATE PROCEDURE [dbo].[ApplicationFile_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1) [Name], [Data]
	FROM [dbo].[ApplicationFile]
	WHERE [Id] = @Id

END
GO
PRINT N'Creating [dbo].[ApplicationFile_GetNames]...';


GO
CREATE PROCEDURE [dbo].[ApplicationFile_GetNames]
	@AppIds [dbo].[IdsTable] READONLY,
	@TypeId INT

AS BEGIN
	SET NOCOUNT ON

	SELECT f.[Id], f.[Name], f.[ApplicationId]
	FROM [dbo].[ApplicationFile] f
	WHERE f.[ApplicationId] IN (SELECT [Id] FROM @AppIds)
	AND f.[TypeId] = @TypeId
	ORDER BY f.[ApplicationId], f.[Name], f.[Id]

END
GO
PRINT N'Creating [dbo].[AwbFile_Add]...';


GO
CREATE PROCEDURE [dbo].[AwbFile_Add]
	@AwbId BIGINT,
	@TypeId INT,
	@Name NVARCHAR(320),
	@Data VARBINARY(MAX)

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[AwbFile] ([AwbId], [Data], [Name], [TypeId])
	OUTPUT INSERTED.[Id]
	VALUES (@AwbId, @Data, @Name, @TypeId)

END
GO
PRINT N'Creating [dbo].[AwbFile_Delete]...';


GO
CREATE PROCEDURE [dbo].[AwbFile_Delete]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[AwbFile]
	WHERE [Id] = @Id

END
GO
PRINT N'Creating [dbo].[AwbFile_Get]...';


GO
CREATE PROCEDURE [dbo].[AwbFile_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1) [Name], [Data]
	FROM [dbo].[AwbFile]
	WHERE [Id] = @Id

END
GO
PRINT N'Creating [dbo].[AwbFile_GetNames]...';


GO
CREATE PROCEDURE [dbo].[AwbFile_GetNames]
	@AwbIds [dbo].[IdsTable] READONLY,
	@TypeId INT

AS BEGIN
	SET NOCOUNT ON

	SELECT f.[Id], f.[Name], f.[AwbId]
	FROM [dbo].[AwbFile] f
	WHERE f.[AwbId] IN (SELECT [Id] FROM @AwbIds)
	AND f.[TypeId] = @TypeId
	ORDER BY f.[AwbId], f.[Name], f.[Id]

END
GO
PRINT N'Creating [dbo].[ClientContract_Get]...';


GO
CREATE PROCEDURE [dbo].[ClientContract_Get]
	@ClientId BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP 1 c.[Name], c.[Data]
	FROM	[dbo].[ClientContract] c
	WHERE	c.[ClientId] = @ClientId
GO
PRINT N'Creating [dbo].[ClientContract_GetFileName]...';


GO
CREATE PROCEDURE [dbo].[ClientContract_GetFileName]
	@ClientId BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP 1 c.[Name]
	FROM	[dbo].[ClientContract] c
	WHERE	c.[ClientId] = @ClientId
GO
PRINT N'Creating [dbo].[ClientContract_Merge]...';


GO
CREATE PROCEDURE [dbo].[ClientContract_Merge]
	@ClientId BIGINT,
	@Name NVARCHAR(320),
	@Data VARBINARY(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	MERGE	[dbo].[ClientContract] AS target

	USING	(VALUES (@ClientId, @Name, @Data))
			AS source ([ClientId], [Name], [Data])

		ON	target.[ClientId] = source.[ClientId]

	WHEN MATCHED AND source.[Name] IS NULL THEN
		DELETE

	WHEN MATCHED THEN
		UPDATE SET [Name] = Source.[Name], [Data] = source.[Data]

	WHEN NOT MATCHED THEN
		INSERT ([ClientId], [Name], [Data]) 
		VALUES (source.[ClientId], source.[Name], source.[Data]);
END
GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO