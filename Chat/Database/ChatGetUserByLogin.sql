-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Получение пользователя по логину
-- =============================================

CREATE PROCEDURE ChatGetUserByLogin @Login NVARCHAR(50)
AS
BEGIN
    SELECT Id, Login, PswdHash, HashSalt FROM ChatUser WHERE Login = @Login;
END