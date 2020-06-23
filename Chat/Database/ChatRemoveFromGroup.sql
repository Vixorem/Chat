-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Удаление юзера из группы
-- =============================================

CREATE PROCEDURE ChatRemoveFromGroup @UserId UNIQUEIDENTIFIER,
                                     @GroupId UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM ChatUserChatRelation WHERE @UserId = UserId AND ChatId = @GroupId;
END