IF(EXISTS(SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID('[Web].[GetRanking]') AND [Type] = 'P'))
DROP PROCEDURE [Web].[GetRanking]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE or ALTER PROCEDURE [Web].[GetRanking]
	@Take int,
	@Skip int,
	@RankingCategory int, -- 1 - Kills, 2 - Assists ( LATER Exp/lvl), 3 - GamesPlayed
	@Order bit
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Sql NVARCHAR(MAX)

	SET @Sql = 'SELECT Nick,
		   [PS].Kills, 
		   [PS].Deaths, 
		   [PS].Assists, 
		   [PS].GamesPlayed, 
		   [PS].GamesWon, 
		   [PS].GameLose 
	FROM Common.PlayerIdentity [PI]
	LEFT JOIN Common.PlayerStatistics [PS] ON [PI].ID = [PS].PlayerId
	ORDER BY [_CATEGORY_] [_ORDER_]'
	
	IF(@RankingCategory = 2)
		SET @Sql = REPLACE(@Sql,'[_CATEGORY_]', '[PS].Assists')
	ELSE IF(@RankingCategory = 3)
		SET @Sql = REPLACE(@Sql,'[_CATEGORY_]', '[PS].GamesPlayed')
	ELSE
		SET @Sql = REPLACE(@Sql,'[_CATEGORY_]', '[PS].Kills')
	IF(@Order = 0)
		SET @Sql = REPLACE(@Sql,'[_ORDER_]', 'DESC')
	 ELSE
		SET @Sql = REPLACE(@Sql,'[_ORDER_]', 'ASC')


	exec sp_executesql @Sql

END
GO

--exec [Web].[GetRanking] 0,0,0,0

