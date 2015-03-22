﻿CREATE TABLE [dbo].[Forwarder]
(
	[Id]		BIGINT			IDENTITY (1, 1) NOT NULL,
	[UserId]	BIGINT			NOT NULL,
	[Name]		NVARCHAR (MAX)	NOT NULL,
	[Email]		NVARCHAR (320)	NOT NULL,

	CONSTRAINT [PK_dbo.Forwarder] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Forwarder_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Forwarder]([UserId] ASC);
GO