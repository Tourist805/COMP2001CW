SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUserModel_DeleteUser]
    @UserId INT
AS 
BEGIN
    DECLARE @responseMessage AS NVARCHAR(250);
    BEGIN TRY
        DELETE dbo.UserModel
        WHERE UserId = @UserId

        SET @responseMessage = 'you have succesfully delete your data'
    END TRY
    BEGIN CATCH
        SET @responseMessage = ERROR_MESSAGE()
    END CATCH
END
GO
