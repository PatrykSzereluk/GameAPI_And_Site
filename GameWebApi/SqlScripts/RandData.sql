DECLARE @i int = 0
						-- ADDED USERS
WHILE @i < 1000
BEGIN
    SET @i = @i + 1
	declare @nick nvarchar(32) = (select 'nick' + cast(@i as nvarchar))
  exec common.RegisterNewPlayer 'elo2','pass1',@nick,'em1','sa', 0
END


						-- ADDED STATISTICS
Set @i = 0

WHILE @i <= 50
BEGIN
    SET @i = @i + 1
	update Common.PlayerStatistics set
	Kills = FLOOR(RAND()*(250-10+1))+10,
	Deaths = FLOOR(RAND()*(250-10+1))+10,
	Assists = FLOOR(RAND()*(250-10+1))+10,
	GamesWon = FLOOR(RAND()*(250-10+1))+10,
	GamesPlayed = FLOOR(RAND()*(250-10+1))+10,
	GameLose = FLOOR(RAND()*(250-10+1))+10
	where PlayerId = @i
END
						-- ADDED CLANS
Set @i = 0

WHILE @i <= 50
BEGIN
    SET @i = @i + 1
	
	declare @nick nvarchar(max) = (select 'Acronym' + cast(@i as nvarchar))
	declare @name nvarchar(max) = (select 'Name' + cast(@i as nvarchar))

	INSERT INTO Common.Clans 
           ([Acronym]
           ,[Name]
           ,[Experience]
           ,[AvatarId]
           ,[AvatarURL])
     VALUES
			(@nick,@name,0,0,null)
END
					-- ADDED CLAN MEMBERS
Set @i = 0

WHILE @i <= 50
BEGIN
    SET @i = @i + 1
	declare @pId int 
	set @pId= (select @i + 20)
	declare @d date
	set @d = (select getdate())
	EXEC [Common].[AddMemberToClan] @pId, @i, 2, @d

END

					-- ADDED CLAN STATISTICS
declare @i int
Set @i = 71
WHILE @i <= 121
BEGIN
    SET @i = @i + 1
	declare @ClanId int
	declare @Wins int
	declare @Losses int
	declare @Draws int

	set @Wins = FLOOR(RAND()*(250-10+1))+10
	set @Losses = FLOOR(RAND()*(250-10+1))+10
	set @Draws = FLOOR(RAND()*(250-10+1))+10
	insert into Common.ClanStatistics values(@i,@Wins,@Losses,@Draws)
END
