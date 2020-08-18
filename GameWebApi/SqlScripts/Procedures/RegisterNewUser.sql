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
	@SaltHash	  NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Common].[PlayerIdentity]([Login],[Password],[Nick],[Email],[GameToken])
	VALUES (@Login,@Password,@NickName,@Email,replace(newid(), '-', ''))

	DECLARE @CurrentUserId INT = (SELECT @@identity)

	INSERT INTO [Common].[PlayerDates] ([PlayerId],[ModificationDate],[LastPasswordChangeDate],[CreationDate],[BanDate])
	VALUES (@CurrentUserId,GETDATE(),GETDATE(),GETDATE(),NULL)

	INSERT INTO [Common].[PlayerSalt] ([PlayerId],[Salt]) VALUES
	(@CurrentUserId,@SaltHash);


		SELECT @CurrentUserId 

END
GO