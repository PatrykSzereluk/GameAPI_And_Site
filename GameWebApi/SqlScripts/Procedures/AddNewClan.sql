IF(EXISTS(SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID('[Common].[AddNewClan]') AND [Type] = 'P'))
DROP PROCEDURE [Common].[AddNewClan]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Common].[AddNewClan]
	@Acronym	NVARCHAR(5),
	@Name		NVARCHAR(16),
	@AvatarId	TINYINT,
	@AvatarUrl	NVARCHAR(MAX)

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @IsNameValid bit = (SELECT 1)
	DECLARE @IsAcronymValid bit = (SELECT 1)
	DECLARE @ClanId int

	IF(EXISTS(SELECT * FROM Common.Clans WHERE [Name] = @Name))
		SET @IsNameValid = 0
	IF(EXISTS(SELECT * FROM Common.Clans WHERE Acronym = @Acronym))
		SET @IsAcronymValid = 0

	INSERT INTO 
		Common.Clans ([Acronym], [Name], [Experience], [AvatarId], [AvatarURL])
	VALUES
		(@Acronym,@Name,0,@AvatarId,@AvatarUrl)

	SET @ClanId = @@IDENTITY

	SELECT 
		@IsNameValid as 'IsNameValid',
		@IsAcronymValid as 'IsAcronymValid',
		@ClanId as 'ClanId'
		


END
