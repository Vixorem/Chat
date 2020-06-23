-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Сохраняет сообщение
-- =============================================

CREATE PROCEDURE ChatCreateMsg @Content NVARCHAR(1000),
                               @ReceiverId UNIQUEIDENTIFIER,
                               @SenderId UNIQUEIDENTIFIER,
                               @SentTime DATETIME2
AS
BEGIN
    INSERT INTO ChatTextMsg(ReceiverId, SenderId, Content, SentTime) OUTPUT INSERTED.Id
    VALUES (@ReceiverId, @SenderId, @Content, @SentTime);
END