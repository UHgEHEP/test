﻿CREATE PROCEDURE [dbo].[ApplicationEvent_GetNext]
	@State INT,
	@ShardIndex INT,
	@ShardCount INT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			e.[Id],
			e.[ApplicationId],
			e.[EventTypeId] AS EventType,
			e.[Data]
	FROM [dbo].[ApplicationEvent] e
	WHERE e.[StateId] = @State AND e.[ApplicationId] % @ShardCount = @ShardIndex
	ORDER BY e.[Id]

END
GO