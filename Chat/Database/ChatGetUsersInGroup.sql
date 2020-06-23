-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Получение списка участников беседы
-- =============================================

CREATE PROCEDURE ChatGetUsersInGroup @GroupId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT cu.Id, cu.Login
    FROM ChatUserChatRelation AS curc
             INNER JOIN ChatUser AS cu ON cu.Id = curc.UserId
    WHERE curc.ChatId = @GroupId;
END