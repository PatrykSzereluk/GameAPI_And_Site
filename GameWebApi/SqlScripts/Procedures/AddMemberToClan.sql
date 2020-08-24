IF(EXISTS(SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID('[Common].[AddMemberToClan]') AND [Type] = 'P'))
DROP PROCEDURE [Common].[AddMemberToClan]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [Common].[AddMemberToClan]
	@PlayerId	INT,
	@ClanId		INT,
	@Function	TINYINT,
	@DateOfJoin	DATE

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @playerHasClan bit = (SELECT 0)
	DECLARE @existstClan bit = (SELECT 0)

	IF(EXISTS(SELECT * FROM Common.ClanMembers WHERE PlayerId = @PlayerId))
		SET @playerHasClan = 1

	IF(EXISTS(SELECT * FROM Common.Clans WHERE ID = @ClanId))
		SET @existstClan = 1

	IF(@playerHasClan = 1 OR @existstClan = 0)
		BEGIN
			SELECT 
				@playerHasClan AS 'PlayerHasClan',
				@existstClan AS 'ExistsClan',
				CAST(0 as bit) AS 'IsSuccess'
			RETURN
		END

	INSERT INTO Common.ClanMembers (PlayerId, ClanId, [Function], DateOfJoin)
	VALUES (@PlayerId, @ClanId, @Function, @DateOfJoin)

	SELECT 
		@playerHasClan AS 'PlayerHasClan',
		@existstClan AS 'ExistsClan',
		CAST(1 as bit) AS 'IsSuccess'

END
