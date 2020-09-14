DROP TABLE [Common].[PlayerBans]
DROP TABLE [Common].[ClanStatistics]
DROP TABLE [Common].[ClanMembers]
DROP TABLE [Common].[Clans]	
DROP TABLE [Common].[PlayerStatistics]
DROP TABLE [Common].[PlayerDates]
DROP TABLE [Common].[PlayerSalt]
DROP TABLE [Common].[PlayerIdentity]


 alter table common.playeridentity
 add [PasswordChanging] bit not null default(0)







IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[PlayerIdentity]') and [TYPE] = N'U'))
DROP TABLE [Common].[PlayerIdentity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [Common].[PlayerIdentity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](32) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[Nick] [varchar](32) NOT NULL,
	[Email] [varchar](64) NOT NULL,
	[GameToken] [varchar](max) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[PlayerSalt]') and [TYPE] = N'U'))
DROP TABLE [Common].[PlayerSalt]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Common].[PlayerSalt](
	[PlayerId] [int] NOT NULL,
	[Salt] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PlayerSalt] PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Common].[PlayerSalt]  WITH CHECK ADD  CONSTRAINT [FK_Salt_PlayerIdentity1] FOREIGN KEY([PlayerId])
REFERENCES [Common].[PlayerIdentity] ([ID])
GO

ALTER TABLE [Common].[PlayerSalt] CHECK CONSTRAINT [FK_Salt_PlayerIdentity1]
GO

IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[PlayerDates]') and [TYPE] = N'U'))
DROP TABLE [Common].[PlayerDates]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Common].[PlayerDates](
	[PlayerId] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModificationDate] [date] NOT NULL,
	[BanDate] [date] NULL,
	[LastPasswordChangeDate] [date] NOT NULL,
 CONSTRAINT [PK_PlayerDates] PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Common].[PlayerDates]  WITH CHECK ADD  CONSTRAINT [FK_PlayerDates_PlayerIdentity] FOREIGN KEY([PlayerId])
REFERENCES [Common].[PlayerIdentity] ([Id])
GO

ALTER TABLE [Common].[PlayerDates] CHECK CONSTRAINT [FK_PlayerDates_PlayerIdentity]
GO

IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[PlayerStatistics]') and [TYPE] = N'U'))
DROP TABLE [Common].[PlayerStatistics]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Common].[PlayerStatistics](
	[PlayerId] [int] NOT NULL,
	[Kills] [int] NOT NULL,
	[Deaths] [int] NOT NULL,
	[Assists] [int] NOT NULL,
	[GamesPlayed] [int] NOT NULL,
	[GamesWon] [int] NOT NULL,
	[GameLose] [int] NOT NULL,
 CONSTRAINT [PK_PlayerStatistics] PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Common].[PlayerStatistics]  WITH CHECK ADD  CONSTRAINT [FK_PlayerStatistics_PlayerIdentity] FOREIGN KEY([PlayerId])
REFERENCES [Common].[PlayerIdentity] ([ID])
GO

ALTER TABLE [Common].[PlayerStatistics] CHECK CONSTRAINT [FK_PlayerStatistics_PlayerIdentity]
GO

IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[Clans]') and [TYPE] = N'U'))
DROP TABLE [Common].[Clans]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Common].[Clans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Acronym] [varchar](5) NOT NULL,
	[Name] [varchar](16) NOT NULL,
	[Experience] [int] NOT NULL,
	[AvatarId] [tinyint],
	[AvatarURL] [varchar](max)
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[ClanMembers]') and [TYPE] = N'U'))
DROP TABLE [Common].[ClanMembers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Common].[ClanMembers](
	[ClanId] [int] NOT NULL,
	[PlayerId] [int] NOT NULL,
	[Function] [tinyint] NOT NULL,
	[DateOfJoin] [date] NOT NULL,
 CONSTRAINT [PK_ClanMembers] PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Common].[ClanMembers]  WITH CHECK ADD  CONSTRAINT [FK_ClanMembers_Clans] FOREIGN KEY([ClanId])
REFERENCES [Common].[Clans] ([ID])
GO

ALTER TABLE [Common].[ClanMembers] CHECK CONSTRAINT [FK_ClanMembers_Clans]
GO

ALTER TABLE [Common].[ClanMembers]  WITH CHECK ADD  CONSTRAINT [FK_ClanMembers_PlayerIdentity] FOREIGN KEY([PlayerId])
REFERENCES [Common].[PlayerIdentity] ([ID])
GO

ALTER TABLE [Common].[ClanMembers] CHECK CONSTRAINT [FK_ClanMembers_PlayerIdentity]
GO

IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[PlayerBans]') and [TYPE] = N'U'))
DROP TABLE [Common].[PlayerBans]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Common].[PlayerBans](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlayerId] [int] NOT NULL,
	[BanReason] [tinyint] NOT NULL,
	[BanMessage] [nvarchar](256) NOT NULL,
	[BeginBanDate] [date] NOT NULL,
	[EndBanDate] [date] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Cancelled] [bit] NOT NULL
 CONSTRAINT [PK_PlayerBans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Common].[PlayerBans]  WITH CHECK ADD  CONSTRAINT [FK_PlayerBans_PlayerIdentity] FOREIGN KEY([PlayerId])
REFERENCES [Common].[PlayerIdentity] ([ID])
GO

ALTER TABLE [Common].[PlayerBans] CHECK CONSTRAINT [FK_PlayerBans_PlayerIdentity]
GO

IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[ClanStatistics]') and [TYPE] = N'U'))
DROP TABLE [Common].[ClanStatistics]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Common].[ClanStatistics](
	[ClanId] [int] NOT NULL,
	[Wins] [int] NOT NULL,
	[Losses] [int] NOT NULL,
	[Draws] [int] NOT NULL,
 CONSTRAINT [PK_ClanStatistics] PRIMARY KEY CLUSTERED 
(
	[ClanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Common].[ClanStatistics]  WITH CHECK ADD  CONSTRAINT [FK_ClanStatistics_Clans] FOREIGN KEY([ClanId])
REFERENCES [Common].[Clans] ([ID])
GO

ALTER TABLE [Common].[ClanStatistics] CHECK CONSTRAINT [FK_ClanStatistics_Clans]
GO









