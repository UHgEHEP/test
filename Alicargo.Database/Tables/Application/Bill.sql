﻿CREATE TABLE [dbo].[Bill]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[ApplicationId] BIGINT NOT NULL,
	[SaveDate] DATETIMEOFFSET NOT NULL,
	[Number] INT NOT NULL,
	[Bank] NVARCHAR(MAX) NOT NULL,
	[BIC] NVARCHAR(MAX) NOT NULL,
	[CorrespondentAccount] NVARCHAR(MAX) NOT NULL,
	[TIN] NVARCHAR(MAX) NOT NULL,
	[TaxRegistrationReasonCode] NVARCHAR(MAX) NOT NULL,
	[CurrentAccount] NVARCHAR(MAX) NOT NULL,
	[Payee] NVARCHAR(MAX) NOT NULL,
	[Shipper] NVARCHAR(MAX) NOT NULL,
	[Head] NVARCHAR(MAX) NOT NULL,
	[Accountant] NVARCHAR(MAX) NOT NULL,
	[HeaderText] NVARCHAR(MAX) NOT NULL,
	[Client] NVARCHAR(MAX) NOT NULL,
	[Goods] NVARCHAR(MAX) NOT NULL,
	[Count] SMALLINT NOT NULL,
	[Price] MONEY NOT NULL,
	[VAT] MONEY NOT NULL,
	[EuroToRuble] MONEY NOT NULL,	

	CONSTRAINT [PK_dbo.Bill] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE UNIQUE INDEX [IX_Bill_ApplicationId] ON [dbo].[Bill] ([ApplicationId])
GO