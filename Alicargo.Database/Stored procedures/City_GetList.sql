﻿CREATE PROCEDURE [dbo].[City_GetList]
	@Language VARCHAR(2)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT c.[Id], IIF(@Language = 'ru', c.[Name_Ru], c.[Name_En]) AS [Name]
	FROM [dbo].[City] c
	ORDER BY c.[Position]

END
GO