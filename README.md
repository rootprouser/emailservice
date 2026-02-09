USE [practice2]
GO

/****** Object:  Table [dbo].[EmailLog]    Script Date: 09-02-2026 10.59.26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmailLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToEmail] [nvarchar](200) NOT NULL,
	[Subject] [nvarchar](200) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[FileName] [nvarchar](200) NULL,
	[FileData] [varbinary](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[EmailLog] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO


