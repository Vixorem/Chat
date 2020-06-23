-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Получение списка групп, в которых учавствует пользователь (Id)
-- =============================================

CREATE PROCEDURE ChatGetGroupsForUserId @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT cg.Id, cg.Name
    FROM (
             SELECT UserId,
                    ChatId
             FROM ChatUserChatRelation
             WHERE UserId = @UserId
         )
             AS curc
             INNER JOIN ChatGroup AS cg ON curc.ChatId = cg.Id;
END
