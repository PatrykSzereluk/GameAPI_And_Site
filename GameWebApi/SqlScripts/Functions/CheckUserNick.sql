﻿--IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[CheckUserNick]') and [TYPE] = N'FN'))
--DROP FUNCTION [Common].[CheckUserNick]
--GO

--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE FUNCTION [Common].[CheckUserNick]
--(
--	@NickName NVARCHAR(64)
--)RETURNS BIT
--AS
--BEGIN
--	DECLARE @FineNick bit = 1

--	IF(EXISTS(SELECT NickName FROM [PlayerInfo].[PlayerLoginInfo] WHERE NickName = @NickName))
--		BEGIN
--			set @FineNick = 0
--		END
--	RETURN @FineNick;	
--END
