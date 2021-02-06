SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUserModel_Validate]
    @Email NVARCHAR(100),
    @Password NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId INT;
    DECLARE @returnValue INT;

    IF EXISTS(SELECT TOP 1 UserId FROM dbo.UserModel WHERE Email = @Email)
    BEGIN
        SET @UserId=(SELECT UserId FROM dbo.UserModel WHERE Email = @Email AND PasswordHash = HASHBYTES('SHA2_512', @Password+CAST(Salt AS NVARCHAR(36))))

        IF(@UserId IS NULL)
            SET @returnValue = 0
        ELSE
            BEGIN
            SET @returnValue = 1
            INSERT INTO dbo.Sessions(UserId, DateStart)
            VALUES (@UserId, GETDATE())
            END
    END

    ELSE
        SET @returnValue = 0

    RETURN @returnValue;    
END
GO
