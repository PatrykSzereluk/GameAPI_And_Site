IF(EXISTS(SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID('[Common].[RegisterNewPlayer]') AND [Type] = 'P'))
DROP PROCEDURE [Common].[RegisterNewPlayer]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Common].[RegisterNewPlayer]
	@Login		  NVARCHAR(32),
	@Password	  NVARCHAR(MAX),
	@NickName	  NVARCHAR(32),
	@Email		  NVARCHAR(64),
	@SaltHash	  NVARCHAR(MAX),
	@PlayerHash   NVARCHAR(255),
	@ReturnValue  bit
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Common].[PlayerIdentity](
		[Login],
		[Password],
		[Nick],
		[Email],
		[EmailConfirmed],
		[PlayerHash],
		[GameToken],
		[PasswordSeciurity],
		[RoleType],
		[PasswordChanging])
	VALUES (
		@Login,
		@Password,
		@NickName,
		@Email,
		0,
		@PlayerHash,
		replace(newid(), '-', ''),
		0,
		0,
		0)

	DECLARE @CurrentUserId INT = (SELECT @@identity)

	INSERT INTO [Common].[PlayerDates] (
		[PlayerId],
		[ModificationDate],
		[LastPasswordChangeDate],
		[CreationDate],[BanDate])
	VALUES (
		@CurrentUserId,
		GETDATE(),
		GETDATE(),
		GETDATE(),
		NULL)

	INSERT INTO [Common].[PlayerSalt] (
		[PlayerId],
		[Salt]) 
	VALUES(
		@CurrentUserId,
		@SaltHash);

	INSERT INTO [Common].[PlayerStatistics] (
		[PlayerId], 
		[Kills],
		[Deaths],
		[Assists],
		[GamesPlayed],
		[GamesWon],
		[GameLose]) 
	VALUES 
	(@CurrentUserId,0,0,0,0,0,0)

	IF(@ReturnValue = 1)
		SELECT @CurrentUserId 

END
GO