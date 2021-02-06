CREATE OR ALTER TRIGGER PasswordChange
ON dbo.UserModel
AFTER UPDATE
AS 
BEGIN
    IF(UPDATE(PasswordHash))
    BEGIN
        DECLARE @UserId INT;
        DECLARE @PreviousPassword BINARY(64);

        SELECT @UserId = UserId, @PreviousPassword = PasswordHash
        FROM deleted;

        INSERT INTO dbo.PasswordModel(UserId, PreviousPassword, dateTrans)
        VALUES (@UserId, @PreviousPassword, GETDATE());
    END
END    