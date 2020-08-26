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

