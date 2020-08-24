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
