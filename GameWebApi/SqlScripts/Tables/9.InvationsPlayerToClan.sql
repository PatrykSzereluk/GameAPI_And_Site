IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[InvationsPlayerToClan]') and [TYPE] = N'U'))
DROP TABLE [Common].[InvationsPlayerToClan]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Common].[InvationsPlayerToClan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerId] [int] NOT NULL,
	[ClanId] [int] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Common].[InvationsPlayerToClan]  WITH CHECK ADD  CONSTRAINT [FK_InvationsPlayerToClan_Clans] FOREIGN KEY([ClanId])
REFERENCES [Common].[Clans] ([ID])
GO

ALTER TABLE [Common].[InvationsPlayerToClan] CHECK CONSTRAINT [FK_InvationsPlayerToClan_Clans]
GO

ALTER TABLE [Common].[InvationsPlayerToClan]  WITH CHECK ADD  CONSTRAINT [FK_InvationsPlayerToClan_PlayerIdentity] FOREIGN KEY([PlayerId])
REFERENCES [Common].[PlayerIdentity] ([ID])
GO

ALTER TABLE [Common].[InvationsPlayerToClan] CHECK CONSTRAINT [FK_InvationsPlayerToClan_PlayerIdentity]
GO
