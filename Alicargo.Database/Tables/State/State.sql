﻿CREATE TABLE [dbo].[State] (
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[Name] NVARCHAR (320) NOT NULL,
	[Position] INT NOT NULL,
	[IsSystem] BIT NOT NULL,

	CONSTRAINT [PK_dbo.State] PRIMARY KEY CLUSTERED ([Id] ASC)
);