IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Tools].[SelectAll]') and [TYPE] = N'P'))
DROP PROCEDURE [Tools].[SelectAll]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Tools].[SelectAll]
AS
BEGIN

SELECT * FROM Common.PlayerIdentity
SELECT * FROM Common.PlayerSalt
SELECT * FROM Common.PlayerDates
SELECT * FROM Common.PlayerStatistics
SELECT * FROM Common.Clans
SELECT * FROM Common.ClanMembers

END
GO
GO