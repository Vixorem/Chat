-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Получает список других пользователей, с которыми существует чат (диалог)
-- =============================================

CREATE PROCEDURE ChatGetDialogsForUserId @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT cu.Id, cu.Login
    FROM (
             SELECT UserId, ChatId
             FROM ChatUserChatRelation
             WHERE UserId = @UserId
         ) AS cucr
             INNER JOIN ChatUser AS cu ON cucr.ChatId = cu.Id;
END
