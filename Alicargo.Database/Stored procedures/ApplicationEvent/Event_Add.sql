﻿CREATE PROCEDURE [dbo].[Event_Add]
	@StateId INT,
	@ApplicationId BIGINT,
	@EventTypeId INT,
	@Data VARBINARY(MAX)
AS
	SET NOCOUNT ON;

	INSERT [dbo].[ApplicationEvent] ([ApplicationId], [EventTypeId], [StateId], [Data])
	VALUES (@ApplicationId, @EventTypeId, @StateId, @Data)

GO