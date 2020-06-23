-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Получение пользователя по его GUID
-- =============================================

CREATE PROCEDURE ChatGetUserById @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT Id, Login, PswdHash, HashSalt FROM ChatUser WHERE Id = @Id;
END