IF(EXISTS(SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID('[Common].[RegisterNewPlayer]') AND [Type] = 'P'))
DROP PROCEDURE [Common].[RegisterNewPlayer]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Common].[RegisterNewPlayer]
	@Login NVARCHAR(32),
	@Password NVARCHAR(MAX),
	@NickName NVARCHAR(32)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Common.PlayerIdentity([Login],[Password],[Nick],[GameToken])
	VALUES (@Login,@Password,@NickName,replace(newid(), '-', ''))

	DECLARE @CurrentUserId INT = (SELECT @@identity)

	INSERT INTO Common.PlayerDates ([PlayerId],[ModificationDate],[LastPasswordChangeDate],[CreationDate],[BanDate])
	VALUES (@CurrentUserId,GETDATE(),GETDATE(),GETDATE(),NULL)


END
GO