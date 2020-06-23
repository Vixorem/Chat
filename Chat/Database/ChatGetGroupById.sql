-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Получить группу по ее GUID
-- =============================================

CREATE PROCEDURE ChatGetGroupById @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT Id, Name FROM ChatGroup WHERE Id = @Id;
END