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

	SET @Sql = '
	WITH DATA AS
	(
		SELECT 
			   ROW_NUMBER() OVER ( ORDER BY [_CATEGORY_] [_ORDER_]) AS ''Place'',
			   [PI].Nick,
			   [PS].Kills, 
			   [PS].Deaths,
			   CASE WHEN [PS].Kills = 0 OR [PS].Deaths = 0 THEN 0 
			   ELSE CAST(CAST([PS].Kills AS float)/CAST([PS].Deaths AS float) as decimal(38,2))
			   END as ''KD'',
			   [PS].Assists, 
			   [PS].GamesPlayed, 
			   [PS].GamesWon, 
			   [PS].GameLose 
		FROM Common.PlayerIdentity [PI]
		LEFT JOIN Common.PlayerStatistics [PS] ON [PI].ID = [PS].PlayerId
	)
	SELECT TOP [_TAKE_] *
	FROM DATA
	WHERE Place > [_SKIP_]'
	
	IF(@RankingCategory = 2)
		SET @Sql = REPLACE(@Sql,'[_CATEGORY_]', 'CASE WHEN [PS].Kills = 0 OR [PS].Deaths = 0 THEN 0 ELSE CAST(CAST([PS].Kills AS float)/CAST([PS].Deaths AS float) as decimal(38,2)) END')
	ELSE IF(@RankingCategory = 3)
		SET @Sql = REPLACE(@Sql,'[_CATEGORY_]', '[PS].GamesPlayed')
	ELSE
		SET @Sql = REPLACE(@Sql,'[_CATEGORY_]', '[PS].Kills')
	IF(@Order = 0)
		SET @Sql = REPLACE(@Sql,'[_ORDER_]', 'DESC')
	 ELSE
		SET @Sql = REPLACE(@Sql,'[_ORDER_]', 'ASC')
	
	SET @Sql = REPLACE(@Sql,'[_TAKE_]', @Take)
	SET @Sql = REPLACE(@Sql,'[_SKIP_]', @Skip)

	EXEC sp_executesql @Sql

	EXEC [TOOLS].[PrintSqlQuery] @Sql

END
GO

--exec [Web].[GetRanking] 10,0,2,0

