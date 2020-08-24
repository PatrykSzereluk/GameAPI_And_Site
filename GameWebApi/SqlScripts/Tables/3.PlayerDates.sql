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
