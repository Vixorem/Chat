-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Создает новый личный чат (Addee с ChatId) или добавляет пользователя в группу (Addee в ChatId)
-- =============================================

CREATE PROCEDURE ChatBindToChat @Addee UNIQUEIDENTIFIER,
                                @ChatId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO ChatUserChatRelation(userid, chatid)
    VALUES (@Addee, @ChatId);
END