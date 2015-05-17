﻿GO
PRINT N'Altering [dbo].[EmailMessage]...';

GO
ALTER TABLE [dbo].[EmailMessage] DROP CONSTRAINT [PK__EmailMes__3214EC0748B81662]

GO
CREATE INDEX [IX_EmailMessage_State_Partition] ON [dbo].[EmailMessage] ([StateId], [PartitionId])

GO
ALTER TABLE [dbo].[EmailMessage] ADD CONSTRAINT
	[PK_dbo.EmailMessage] PRIMARY KEY CLUSTERED (Id) ON [PRIMARY]


GO
PRINT N'Altering [dbo].[Client]...';


GO
ALTER TABLE [dbo].[Client]
	ADD [DefaultSenderId] BIGINT NULL;


GO
PRINT N'Altering [dbo].[Client_Add]...';


GO
ALTER PROCEDURE [dbo].[Client_Add]
	@UserId BIGINT,
    @Emails NVARCHAR(MAX),
    @Nic NVARCHAR(MAX),
    @LegalEntity NVARCHAR(MAX),
    @Contacts NVARCHAR(MAX),
    @Phone NVARCHAR(MAX),
    @INN NVARCHAR(MAX),
    @KPP NVARCHAR(MAX),
    @OGRN NVARCHAR(MAX),
    @Bank NVARCHAR(MAX),
    @BIC NVARCHAR(MAX),
    @LegalAddress NVARCHAR(MAX),
    @MailingAddress NVARCHAR(MAX),
    @RS NVARCHAR(MAX),
    @KS NVARCHAR(MAX),
    @ContractNumber NVARCHAR(MAX),
    @ContractDate DATETIMEOFFSET,
    @TransitId BIGINT,
	@DefaultSenderId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[Client]
		([UserId]
		,[Emails]
		,[Nic]
		,[LegalEntity]
		,[Contacts]
		,[Phone]
		,[INN]
		,[KPP]
		,[OGRN]
		,[Bank]
		,[BIC]
		,[LegalAddress]
		,[MailingAddress]
		,[RS]
		,[KS]
		,[TransitId]
		,[ContractNumber]
		,[ContractDate]
		,[DefaultSenderId])
	OUTPUT INSERTED.[Id]
	VALUES
		(@UserId,
		@Emails,
		@Nic,
		@LegalEntity,
		@Contacts,
		@Phone,
		@INN,
		@KPP,
		@OGRN,
		@Bank,
		@BIC,
		@LegalAddress,
		@MailingAddress,
		@RS,
		@KS,
		@TransitId,
		@ContractNumber,
		@ContractDate,
		@DefaultSenderId)

END
GO
PRINT N'Altering [dbo].[Client_Get]...';


GO
ALTER PROCEDURE [dbo].[Client_Get]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1)
		c.[Id] AS [ClientId],
		u.[Id] AS [UserId],
		c.[Emails],
		c.[Nic],
		c.[LegalEntity],
		c.[Contacts],
		c.[Phone],
		c.[INN],
		c.[KPP],
		c.[OGRN],
		c.[Bank],
		c.[BIC],
		c.[LegalAddress],
		c.[MailingAddress],
		c.[RS],
		c.[KS],
		c.[TransitId],
		c.[Balance],
		u.[Login],
		u.[TwoLetterISOLanguageName] AS [Language],
		c.[ContractDate],
		c.[ContractNumber],
		c.[DefaultSenderId]
	  FROM [dbo].[Client] c
	  JOIN [dbo].[User] u
	  ON c.[UserId] = u.[Id]
	  WHERE c.[Id] = @ClientId

END
GO
PRINT N'Altering [dbo].[Client_GetAll]...';


GO
ALTER PROCEDURE [dbo].[Client_GetAll]

AS BEGIN
	SET NOCOUNT ON;

	SELECT
		c.[Id] AS [ClientId],
		u.[Id] AS [UserId],
		c.[Emails],
		c.[Nic],
		c.[LegalEntity],
		c.[Contacts],
		c.[Phone],
		c.[INN],
		c.[KPP],
		c.[OGRN],
		c.[Bank],
		c.[BIC],
		c.[LegalAddress],
		c.[MailingAddress],
		c.[RS],
		c.[KS],
		c.[TransitId],
		c.[Balance],
		u.[Login],
		u.[TwoLetterISOLanguageName] AS [Language],
		c.[ContractDate],
		c.[ContractNumber],
		C.[DefaultSenderId]
	  FROM [dbo].[Client] c
	  JOIN [dbo].[User] u
	  ON c.[UserId] = u.[Id]

END
GO
PRINT N'Altering [dbo].[Client_GetByUserId]...';


GO
ALTER PROCEDURE [dbo].[Client_GetByUserId]
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1)
		c.[Id] AS [ClientId],
		u.[Id] AS [UserId],
		c.[Emails],
		c.[Nic],
		c.[LegalEntity],
		c.[Contacts],
		c.[Phone],
		c.[INN],
		c.[KPP],
		c.[OGRN],
		c.[Bank],
		c.[BIC],
		c.[LegalAddress],
		c.[MailingAddress],
		c.[RS],
		c.[KS],
		c.[TransitId],
		c.[Balance],
		u.[Login],
		u.[TwoLetterISOLanguageName] AS [Language],
		c.[ContractDate],
		c.[ContractNumber],
		C.[DefaultSenderId]
	  FROM [dbo].[Client] c
	  JOIN [dbo].[User] u
	  ON c.[UserId] = u.[Id]
	  WHERE u.[Id] = @UserId

END
GO
PRINT N'Altering [dbo].[Client_Update]...';


GO
ALTER PROCEDURE [dbo].[Client_Update]
	@ClientId BIGINT,
    @Emails NVARCHAR(MAX),
    @Nic NVARCHAR(MAX),
    @LegalEntity NVARCHAR(MAX),
    @Contacts NVARCHAR(MAX),
    @Phone NVARCHAR(MAX),
    @INN NVARCHAR(MAX),
    @KPP NVARCHAR(MAX),
    @OGRN NVARCHAR(MAX),
    @Bank NVARCHAR(MAX),
    @BIC NVARCHAR(MAX),
    @LegalAddress NVARCHAR(MAX),
    @MailingAddress NVARCHAR(MAX),
    @RS NVARCHAR(MAX),
    @KS NVARCHAR(MAX),
    @ContractNumber NVARCHAR(MAX),
    @ContractDate DATETIMEOFFSET,
	@DefaultSenderId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Client]
	SET [Emails] = @Emails,
		[Nic] = @Nic,
		[LegalEntity] = @LegalEntity,
		[Contacts] = @Contacts,
		[Phone] = @Phone,
		[INN] = @INN,
		[KPP] = @KPP,
		[OGRN] = @OGRN,
		[Bank] = @Bank,
		[BIC] = @BIC,
		[LegalAddress] = @LegalAddress,
		[MailingAddress] = @MailingAddress,
		[RS] = @RS,
		[KS] = @KS,
		[ContractNumber] = @ContractNumber,
		[ContractDate] = @ContractDate,
		[DefaultSenderId] = @DefaultSenderId
	WHERE [Id] = @ClientId

END
GO
PRINT N'Refreshing [dbo].[Client_DeleteForce]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_DeleteForce]';


GO
PRINT N'Refreshing [dbo].[Client_GetBalance]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_GetBalance]';


GO
PRINT N'Refreshing [dbo].[Client_SetBalance]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_SetBalance]';


GO
PRINT N'Refreshing [dbo].[Client_SumBalance]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_SumBalance]';


GO
PRINT N'Refreshing [dbo].[ClientBalanceHistory_GetAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ClientBalanceHistory_GetAll]';


GO
PRINT N'Refreshing [dbo].[Transit_GetByClient]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Transit_GetByClient]';


GO
PRINT N'Refreshing [dbo].[User_Get]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_Get]';


GO
PRINT N'Refreshing [dbo].[User_GetUserIdByEmail]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_GetUserIdByEmail]';


GO
PRINT N'Refreshing [dbo].[EmailMessage_Add]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[EmailMessage_Add]';


GO
PRINT N'Refreshing [dbo].[EmailMessage_GetNext]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[EmailMessage_GetNext]';


GO
PRINT N'Refreshing [dbo].[EmailMessage_SetState]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[EmailMessage_SetState]';


GO
PRINT N'Update complete Alicargo-3_1-3_2.';


GO
