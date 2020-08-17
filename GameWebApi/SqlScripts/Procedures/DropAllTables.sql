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

	DROP TABLE [History].[ArchiveStatistics]
	DROP TABLE [PlayerInfo].[BanInfo]
	DROP TABLE [PlayerInfo].[PlayerDates]
	DROP TABLE [PlayerInfo].[PlayerLoginInfo]
	DROP TABLE [PlayerInfo].[PlayerStats]
	DROP TABLE [PlayerInfo].[PlayerStatuses]
	DROP TABLE [PlayerInfo].[Salt]
	DROP TABLE [TheGame].[Configuration]

END
GO

GO