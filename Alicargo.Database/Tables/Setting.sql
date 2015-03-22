﻿CREATE TABLE [dbo].[Setting]
(
	[Type] INT NOT NULL,
	[RowVersion] ROWVERSION NOT NULL,
	[Data] VARBINARY(MAX) NOT NULL,

	CONSTRAINT [PK_dbo.Setting] PRIMARY KEY CLUSTERED ([Type] ASC),
)
GO