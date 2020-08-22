IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Common].[Clans]') and [TYPE] = N'U'))
DROP TABLE [Common].[Clans]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Common].[Clans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Acronym] [varchar](max) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Experience] [varchar](max) NOT NULL,
	[AvatarId] [varchar](64),
	[AvatarURL] [varchar](max)
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


