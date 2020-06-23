-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 16.06.2020
-- Description: Проверка на наличие юзера в чате
-- =============================================

CREATE PROCEDURE ChatIsUserInChat @UserId UNIQUEIDENTIFIER,
                                           @ChatId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT curc.UserId
    FROM ChatUserChatRelation AS curc
    WHERE curc.ChatId = @ChatId
      AND curc.UserId = @UserId;
END