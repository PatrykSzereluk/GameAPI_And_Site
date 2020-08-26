IF(EXISTS(SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID('[Web].[GetClanRanking]') AND [Type] = 'P'))
DROP PROCEDURE [Web].[GetClanRanking]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE or ALTER PROCEDURE [Web].[GetClanRanking]
	@Take int,
	@Skip int,
	@RankingCategory int, -- 1 - Wins, 2 - Losses ( LATER Exp/lvl), 3 - Draws, 4 - exp
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
			   [C].Name,
			   [CS].Wins, 
			   [CS].Losses, 
			   [CS].Draws,
			   [C].Experience
		FROM Common.Clans [C]
		LEFT JOIN Common.[ClanStatistics] [CS] ON [C].ID = [CS].ClanId
	)
	SELECT TOP [_TAKE_] *
	FROM DATA
	WHERE Place > [_SKIP_]'
	
	IF(@RankingCategory = 2)
		SET @Sql = REPLACE(@Sql,'[_CATEGORY_]', '[CS].Draws')
	ELSE IF(@RankingCategory = 3)
		SET @Sql = REPLACE(@Sql,'[_CATEGORY_]', '[CS].Losses')
	ELSE IF(@RankingCategory = 4)
		SET @Sql = REPLACE(@Sql,'[_CATEGORY_]', '[C].Experience')
	ELSE
		SET @Sql = REPLACE(@Sql,'[_CATEGORY_]', '[CS].Wins')
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

--exec [Web].[GetClanRanking] 3,3,1,0

