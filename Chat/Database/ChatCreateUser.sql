-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Создает нового пользователя
-- =============================================

CREATE PROCEDURE ChatCreateUser @Id UNIQUEIDENTIFIER,
                                @Login NVARCHAR(50),
                                @HashedPswd NVARCHAR(128),
                                @Salt NVARCHAR(64)
AS
BEGIN
    INSERT INTO ChatUser
    VALUES (@Id, @Login, @HashedPswd, @Salt);
END