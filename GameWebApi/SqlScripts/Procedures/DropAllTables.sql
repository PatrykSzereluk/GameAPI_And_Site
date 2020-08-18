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

	DROP TABLE [Common].[PlayerDates]
	DROP TABLE [Common].[Salt]
	DROP TABLE [Common].[PlayerIdentity]

END
GO

GO