SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUserModel_UpdateUser]
    @UserId INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @Password NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @responseMessage AS NVARCHAR(250);
    BEGIN TRY
        UPDATE dbo.UserModel
        SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            PasswordHash = HASHBYTES('SHA2_512', @Password)
        WHERE 
            UserId = @UserId

        SET @responseMessage = 'You have successfully updated your data';
    END TRY
    BEGIN CATCH
        SET @responseMessage= ERROR_MESSAGE();
    END CATCH

END
GO
