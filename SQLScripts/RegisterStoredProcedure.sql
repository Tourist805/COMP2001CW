SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUserModel_RegisterUser]
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @Password NVARCHAR(50)  
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @responseMessage NVARCHAR;
    DECLARE @salt UNIQUEIDENTIFIER = NEWID();
    BEGIN TRY
        INSERT INTO dbo.UserModel(FirstName, LastName, Salt, Email, PasswordHash)
        VALUES(@FirstName, @LastName, @salt, @Email, HASHBYTES('SHA2_512', @Password+CAST(@salt AS NVARCHAR(36))));

         SET @responseMessage = 'Status success';
    END TRY
    BEGIN CATCH
            SET @responseMessage= 'Failed inserting';
    END CATCH        

    RETURN @responseMessage;
END
GO
