-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Создает группу
-- =============================================

CREATE PROCEDURE ChatCreateGroup @Id UNIQUEIDENTIFIER,
                                 @Name NVARCHAR(50)
AS
BEGIN
    INSERT INTO ChatGroup
    VALUES (@Id, @Name);
END