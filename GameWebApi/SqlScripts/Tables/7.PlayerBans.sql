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

