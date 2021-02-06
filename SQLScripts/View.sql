SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserLoginCount] AS
SELECT COUNT(Sessions.SessionId) as totalSessions, UserModel.Email
FROM dbo.Sessions, dbo.UserModel
WHERE Sessions.UserId = UserModel.UserId
GROUP BY UserModel.Email
GO
