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
	[EmailConfirmed] [BIT] NOT NULL,
	[PlayerHash] [nvarchar](255) NOT NULL,
	[RoleType] [tinyint] not null,
	[PasswordChanging] [bit] not null,
	[GameToken] [varchar](max) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

