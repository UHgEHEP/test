﻿USE [master] 
GO

ALTER PROCEDURE [dbo].[sp_RestoreDatabase]
	@newDb NVARCHAR(50),
	@oldDb NVARCHAR(50),
	@fromFile NVARCHAR(200),
	@dataFolder NVARCHAR(200)

AS BEGIN
	SET NOCOUNT ON;

	DECLARE @sqlRestoreDb NVARCHAR(1000)
		= N'RESTORE DATABASE ' + @newDb + N' FROM '
		+ N'DISK = N''' + @fromFile + N''' WITH FILE = 1, '
		+ N'MOVE N''' + @oldDb + N''' TO N''' + @dataFolder +  @newDb + N'.mdf'', '
		+ N'MOVE N''' + @oldDb + N'_log'' TO N''' + @dataFolder + @newDb + N'_log.ldf'', '
		+ N'NOUNLOAD, REPLACE, STATS = 5;';
	EXEC(@sqlRestoreDb)	

	IF @newDb <> @oldDb BEGIN
		DECLARE @sqlModifyName NVARCHAR(1000)
			= N'ALTER DATABASE ' + @newDb + N' MODIFY FILE (NAME=N'''+ @oldDb + N''', NEWNAME=N''' + @newDb + N''');'
			+ N'ALTER DATABASE ' + @newDb + N' MODIFY FILE (NAME=N'''+ @oldDb + N'_log'', NEWNAME=N''' + @newDb + N'_log'');'

		EXEC(@sqlModifyName)	
	END

END
GO

USE [$(MainDbName)]
GO

GO
PRINT N'Altering [dbo].[Application]...';


GO
ALTER TABLE [dbo].[Application]
    ADD [FactureCostEx]          MONEY NULL,
        [FactureCostExEdited]    MONEY NULL,
        [InsuranceRate]          REAL  CONSTRAINT [DF_Application_InsuranceRate] DEFAULT 0.01 NOT NULL,
        [InsuranceRateForClient] REAL  CONSTRAINT [DF_Application_InsuranceRateForClient] DEFAULT 0.01 NOT NULL;


GO
PRINT N'Altering [dbo].[Calculation]...';


GO
ALTER TABLE [dbo].[Calculation] DROP COLUMN [InsuranceCost];


GO
ALTER TABLE [dbo].[Calculation]
    ADD [FactureCostEx]          MONEY CONSTRAINT [DF_Calculation_FactureCostEx] DEFAULT 0 NOT NULL,
        [InsuranceRate]          REAL  CONSTRAINT [DF_Calculation_InsuranceRate] DEFAULT 0.01 NOT NULL,
        [InsuranceRateForClient] REAL  CONSTRAINT [DF_Calculation_InsuranceRateForClient] DEFAULT 0.01 NOT NULL;

GO
ALTER TABLE [dbo].[Calculation] DROP CONSTRAINT [DF_Calculation_FactureCostEx];

GO
PRINT N'Altering [dbo].[Carrier]...';


GO
ALTER TABLE [dbo].[Carrier]
    ADD [Contact] NVARCHAR (MAX) NULL,
        [Address] NVARCHAR (MAX) NULL,
        [Phone]   NVARCHAR (MAX) NULL;


GO
PRINT N'Altering [dbo].[Sender]...';


GO
ALTER TABLE [dbo].[Sender]
    ADD [Contact] NVARCHAR (MAX) NULL,
        [Phone]   NVARCHAR (MAX) NULL,
        [Address] NVARCHAR (MAX) NULL;


GO
PRINT N'Altering [dbo].[Calculation_Add]...';


GO
ALTER PROCEDURE [dbo].[Calculation_Add]
	@ClientId BIGINT,
	@ApplicationHistoryId BIGINT,
	@AirWaybillDisplay NVARCHAR(320),
	@ApplicationDisplay NVARCHAR(320),
	@MarkName NVARCHAR(320),
	@Weight REAL,
	@TariffPerKg MONEY,
	@ScotchCost MONEY,
	@InsuranceRate REAL,
	@InsuranceRateForClient REAL,
	@FactureCost MONEY,
	@FactureCostEx MONEY,
	@TransitCost MONEY,
	@PickupCost MONEY,
	@FactoryName NVARCHAR(320),
	@CreationTimestamp DATETIMEOFFSET,
	@ClassId INT,
	@Invoice NVARCHAR(MAX),
	@Value MONEY,
	@Count INT

AS BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Calculation]
		([ClientId]
		,[ApplicationHistoryId]
		,[AirWaybillDisplay]
		,[ApplicationDisplay]
		,[MarkName]
		,[Weight]
		,[TariffPerKg]
		,[ScotchCost]
		,[FactureCost]
		,[FactureCostEx]
		,[TransitCost]
		,[PickupCost]
		,[FactoryName]
		,[CreationTimestamp]
		,[ClassId]
		,[Invoice]
		,[Value]
		,[Count]
		,[InsuranceRate]
		,[InsuranceRateForClient])
     VALUES
		(@ClientId
		,@ApplicationHistoryId
		,@AirWaybillDisplay
		,@ApplicationDisplay
		,@MarkName
		,@Weight
		,@TariffPerKg
		,@ScotchCost
		,@FactureCost
		,@FactureCostEx
		,@TransitCost
		,@PickupCost
		,@FactoryName
		,@CreationTimestamp
		,@ClassId
		,@Invoice
		,@Value
		,@Count
		,@InsuranceRate
		,@InsuranceRateForClient)
END
GO
PRINT N'Altering [dbo].[Calculation_GetByApplication]...';


GO
ALTER PROCEDURE [dbo].[Calculation_GetByApplication]
	@ApplicationId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1)
		[ClientId],
		[AirWaybillDisplay],
		[ApplicationDisplay],
		[MarkName],
		[Weight],
		[TariffPerKg],
		[ScotchCost],
		[FactureCost],
		[FactureCostEx],
		[TransitCost],
		[PickupCost],
		[FactoryName],
		[CreationTimestamp],
		[ClassId] AS [Class],
		[Invoice],
		[Value],
		[Count],
		[InsuranceRate],
		[InsuranceRateForClient]
	FROM [dbo].[Calculation]
	WHERE [ApplicationHistoryId] = @ApplicationId

END
GO
PRINT N'Altering [dbo].[Calculation_GetByClient]...';


GO
ALTER PROCEDURE [dbo].[Calculation_GetByClient]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	[ClientId],
			[AirWaybillDisplay],
			[ApplicationDisplay],
			[MarkName],
			[Weight],
			[TariffPerKg],
			[ScotchCost],
			[FactureCost],
			[FactureCostEx],
			[TransitCost],
			[PickupCost],
			[FactoryName],
			[CreationTimestamp],
			[ClassId] AS [Class],
			[Invoice],
			[Value],
			[Count],
			[InsuranceRate],
			[InsuranceRateForClient]
	FROM [dbo].[Calculation]
	WHERE [ClientId] = @ClientId

END
GO
PRINT N'Altering [dbo].[Carrier_Add]...';


GO
ALTER PROCEDURE [dbo].[Carrier_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@Language CHAR(2),
	@Name NVARCHAR (MAX),
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @UserId BIGINT;
		EXEC @UserId = [dbo].[User_Add]
				@Login = @Login,
				@PasswordHash = @PasswordHash, 
				@PasswordSalt = @PasswordSalt, 
				@TwoLetterISOLanguageName = @Language

		INSERT [dbo].[Carrier] ([UserId], [Name], [Email], [Contact], [Phone], [Address])
		OUTPUT INSERTED.[Id]
		VALUES (@UserId, @Name, @Email, @Contact, @Phone, @Address)

	COMMIT

END
GO
PRINT N'Altering [dbo].[Carrier_Get]...';


GO
ALTER PROCEDURE [dbo].[Carrier_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			c.[Id],
			u.[Id] AS [UserId],
			c.[Email],
			u.[TwoLetterISOLanguageName] AS [Language],
			u.[Login],
			c.[Name],
			c.[Contact],
			c.[Phone],
			c.[Address]
	FROM	[dbo].[Carrier] c
	JOIN	[dbo].[User] u
	ON		u.[Id] = c.[UserId]
	WHERE	c.[Id] = @Id

END
GO
PRINT N'Altering [dbo].[Carrier_GetAll]...';


GO
ALTER PROCEDURE [dbo].[Carrier_GetAll]

AS BEGIN
	SET NOCOUNT ON;

	SELECT	c.[Id],
			u.[Id] AS [UserId],
			c.[Email],
			u.[TwoLetterISOLanguageName] AS [Language],
			u.[Login],
			c.[Name],
			c.[Contact],
			c.[Phone],
			c.[Address]
	FROM	[dbo].[Carrier] c
	JOIN	[dbo].[User] u
	ON		u.[Id] = c.[UserId]

END
GO
PRINT N'Altering [dbo].[Carrier_Update]...';


GO
ALTER PROCEDURE [dbo].[Carrier_Update]
	@Id BIGINT,
	@Name NVARCHAR (MAX),
	@Login NVARCHAR(320),
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	DECLARE @Table TABLE ([UserId] BIGINT);

	BEGIN TRAN

		UPDATE	TOP(1) [dbo].[Carrier]
		SET		[Name] = @Name,
				[Email] = @Email,
				[Contact] = @Contact,
				[Phone] = @Phone,
				[Address] = @Address
		OUTPUT	INSERTED.[UserId] INTO @Table
		WHERE	[Id] = @Id


		UPDATE	TOP(1) [dbo].[User]
		SET		[Login] = @Login
		WHERE	[Id] IN (SELECT [UserId] FROM @Table);

	COMMIT

END
GO
PRINT N'Altering [dbo].[Sender_Add]...';


GO
ALTER PROCEDURE [dbo].[Sender_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@TwoLetterISOLanguageName CHAR(2),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320),
	@TariffOfTapePerBox MONEY,
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @UserId BIGINT;
		EXEC	@UserId = [dbo].[User_Add]
				@Login = @Login, @PasswordHash = @PasswordHash, 
				@PasswordSalt = @PasswordSalt, 
				@TwoLetterISOLanguageName = @TwoLetterISOLanguageName

		INSERT	[dbo].[Sender] ([UserId], [Name], [Email], [TariffOfTapePerBox], [Contact], [Phone], [Address])
		OUTPUT	INSERTED.[Id]
		VALUES	(@UserId, @Name, @Email, @TariffOfTapePerBox, @Contact, @Phone, @Address)

	COMMIT

END
GO
PRINT N'Altering [dbo].[Sender_Get]...';


GO
ALTER PROCEDURE [dbo].[Sender_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			s.[Name],
			s.[Email],
			s.[TariffOfTapePerBox],
			u.[Login],
			s.[Contact],
			s.[Phone],
			s.[Address],
			u.[TwoLetterISOLanguageName] AS [Language]
	FROM	[dbo].[Sender] s
	JOIN	[dbo].[User] u
	ON		u.[Id] = s.[UserId]
	WHERE	s.[Id] = @Id

END
GO
PRINT N'Altering [dbo].[Sender_GetAll]...';


GO
ALTER PROCEDURE [dbo].[Sender_GetAll]

AS BEGIN
	SET NOCOUNT ON;

	SELECT	s.[Id] AS [EntityId],
			u.[Id] AS [UserId],
			s.[Name],
			u.[Login],
			s.[Email],
			s.[Contact],
			s.[Phone],
			s.[Address],
			u.[TwoLetterISOLanguageName] AS [Language]
	FROM	[dbo].[Sender] s
	JOIN	[dbo].[User] u
	ON		u.[Id] = s.[UserId]

END
GO
PRINT N'Altering [dbo].[Sender_Update]...';


GO
ALTER PROCEDURE [dbo].[Sender_Update]
	@Id BIGINT,
	@Login NVARCHAR(320),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320),
	@TwoLetterISOLanguageName CHAR(2),
	@TariffOfTapePerBox	MONEY,
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX)

AS BEGIN
	SET NOCOUNT ON;

	DECLARE @Table TABLE ([UserId] BIGINT);

	BEGIN TRAN

		UPDATE	TOP(1) [dbo].[Sender]
		SET		[Name] = @Name,
				[Email] = @Email,
				[TariffOfTapePerBox] = @TariffOfTapePerBox,
				[Contact] = @Contact,
				[Phone] = @Phone,
				[Address] = @Address
		OUTPUT	inserted.[UserId] INTO @Table
		WHERE	[Id] = @Id

		UPDATE	TOP(1) [dbo].[User]
		SET		[Login] = @Login,
				[TwoLetterISOLanguageName] = @TwoLetterISOLanguageName
		WHERE	[Id] IN (SELECT [UserId] FROM @Table);

	COMMIT

END
GO
PRINT N'Refreshing [dbo].[Client_DeleteForce]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_DeleteForce]';


GO
PRINT N'Refreshing [dbo].[Transit_GetByApplication]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Transit_GetByApplication]';


GO
PRINT N'Refreshing [dbo].[Calculation_RemoveByApplication]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Calculation_RemoveByApplication]';


GO
PRINT N'Refreshing [dbo].[Carrier_GetByUserId]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Carrier_GetByUserId]';


GO
PRINT N'Refreshing [dbo].[Sender_GetByUser]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Sender_GetByUser]';


GO
PRINT N'Refreshing [dbo].[Sender_GetTariffs]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Sender_GetTariffs]';


GO
PRINT N'Refreshing [dbo].[Sender_GetUserId]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Sender_GetUserId]';


GO
PRINT N'Refreshing [dbo].[User_GetUserIdByEmail]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_GetUserIdByEmail]';


GO
PRINT N'Update complete.';


GO