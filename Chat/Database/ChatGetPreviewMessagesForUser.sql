-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 22.06.2020
-- Description: Возвращает превью чата: название, последнее сообщение, время со сдвигом и заданным кол-вом
-- =============================================

CREATE PROCEDURE ChatGetPreviewMessagesForUser @UserId UNIQUEIDENTIFIER,
                                               @Offset INT,
                                               @Limit INT
AS
BEGIN
    SELECT Id,
           ChatId,
           ChatName,
           Content,
           SentTime
    FROM (
             SELECT chat.Id,
                    chat.ChatId,
                    chat.ChatName,
                    ctm.Content,
                    ctm.SentTime,
                    ROW_NUMBER() OVER (PARTITION BY ChatId ORDER BY ctm.SentTime DESC) AS lastDate
             FROM (
                      SELECT cucr.Id  AS Id,
                             cu.Id    AS ChatId,
                             cu.Login AS ChatName
                      FROM ChatUserChatRelation AS cucr
                               JOIN ChatUser AS cu ON cucr.ChatId = cu.Id AND cucr.UserId = @UserId
                      UNION
                      SELECT cucr.Id AS Id,
                             cg.Id   AS ChatId,
                             cg.Name AS ChatName
                      FROM ChatUserChatRelation AS cucr
                               JOIN ChatGroup AS cg ON cucr.ChatId = cg.Id AND cucr.UserId = @UserId
                  ) AS chat
                      JOIN ChatTextMsg AS ctm
                           ON (chat.ChatId = ctm.ReceiverId AND ctm.SenderId = @UserId)
                               OR
                              (chat.ChatId = ctm.SenderId AND ctm.ReceiverId = @UserId)
         ) AS lastMessages
    WHERE lastDate = 1
    ORDER BY SentTime DESC
        OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY;

END