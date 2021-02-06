SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasswordModel](
	[PasswordId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PreviousPassword] [binary](64) NOT NULL,
	[dateTrans] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PasswordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PasswordModel]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserModel] ([UserId])
GO
