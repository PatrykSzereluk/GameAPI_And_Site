IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Tools].[DropAllTables]') and [TYPE] = N'P'))
DROP PROCEDURE [Tools].[DropAllTables]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Tools].[DropAllTables]
AS
BEGIN
	DELETE FROM [Common].[ClanMembers]
	DELETE FROM [Common].[Clans]
	DELETE FROM [Common].[PlayerStatistics]
	DELETE FROM [Common].[PlayerDates]
	DELETE FROM [Common].[PlayerSalt]
	DELETE FROM [Common].[PlayerIdentity]



	DROP TABLE [Common].[ClanMembers]
	DROP TABLE [Common].[Clans]	
	DROP TABLE [Common].[PlayerStatistics]
	DROP TABLE [Common].[PlayerDates]
	DROP TABLE [Common].[PlayerSalt]
	DROP TABLE [Common].[PlayerIdentity]

END
GO

GO