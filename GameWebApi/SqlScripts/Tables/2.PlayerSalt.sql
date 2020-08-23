IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[PlayerSalt]') and [TYPE] = N'U'))
DROP TABLE [Common].[PlayerSalt]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Common].[PlayerSalt](
	[PlayerId] [int] NOT NULL,
	[Salt] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Common].[PlayerSalt]  WITH CHECK ADD  CONSTRAINT [FK_Salt_PlayerIdentity1] FOREIGN KEY([PlayerId])
REFERENCES [Common].[PlayerIdentity] ([ID])
GO

ALTER TABLE [Common].[PlayerSalt] CHECK CONSTRAINT [FK_Salt_PlayerIdentity1]
GO