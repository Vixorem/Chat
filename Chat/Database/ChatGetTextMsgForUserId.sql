-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Получение сообщений юзера (UserId) с другим юзером (ChatId)
-- =============================================

CREATE PROCEDURE ChatGetTextMsgForUserId @ChatId UNIQUEIDENTIFIER,
                                         @UserId UNIQUEIDENTIFIER,
                                         @PageShift INT,
                                         @FetchNum INT
AS
BEGIN
    SELECT ctm.Id,
           cu1.Id as SenderId,
           cu1.Login as SenderLogin,
           cu2.Id as ReceiverId,
           ctm.Content,
           ctm.SentTime
    FROM ChatTextMsg as ctm
             INNER JOIN ChatUser AS cu1 ON ctm.SenderId = cu1.Id
             INNER JOIN ChatUser AS cu2 ON ctm.ReceiverId = cu2.Id
    WHERE (ctm.ReceiverId = @ChatId
        AND ctm.SenderId = @UserId)
       OR (ctm.ReceiverId = @UserId
        AND ctm.SenderId = @ChatId)
    ORDER BY ctm.SentTime DESC
    OFFSET @PageShift ROWS FETCH NEXT @FetchNum ROWS ONLY;
END