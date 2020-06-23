-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Получения сообщений в группе ChatId
-- =============================================

CREATE PROCEDURE ChatGetTextMsgForGroupId @ChatId UNIQUEIDENTIFIER,
                                          @PageShift INT,
                                          @FetchNum INT
AS
BEGIN
    SELECT ctm.Id,
           ctm.SenderId,
           cu.Login as SenderLogin,
           ctm.ReceiverId,
           ctm.Content,
           ctm.SentTime
    FROM ChatTextMsg as ctm
             INNER JOIN ChatUser AS cu ON ctm.SenderId = cu.Id
             INNER JOIN ChatGroup AS cg ON ctm.ReceiverId = cg.Id
    WHERE ctm.ReceiverId = @ChatId
    ORDER BY ctm.SentTime DESC
    OFFSET @PageShift ROWS FETCH NEXT @FetchNum ROWS ONLY;
END