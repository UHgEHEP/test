﻿CREATE TABLE [dbo].[Broker] (
	[Id]		BIGINT			IDENTITY(1, 1) NOT NULL,
	[UserId]	BIGINT			NOT NULL,
	[Name]		NVARCHAR (MAX)	NOT NULL,
	[Email]		NVARCHAR(320)	NOT NULL, 

	CONSTRAINT [PK_dbo.Broker] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Broker_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Broker]([UserId] ASC);

