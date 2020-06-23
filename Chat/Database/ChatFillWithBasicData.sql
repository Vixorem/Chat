-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Тестовое заполнение таблиц данными
-- =============================================

DECLARE
    @UserId1  UNIQUEIDENTIFIER,
    @UserId2  UNIQUEIDENTIFIER,
    @UserId3  UNIQUEIDENTIFIER,
    @UserId4  UNIQUEIDENTIFIER,
    @UserId5  UNIQUEIDENTIFIER,
    @UserId6  UNIQUEIDENTIFIER,
    @GroupId1 UNIQUEIDENTIFIER,
    @GroupId2 UNIQUEIDENTIFIER,
    @GroupId3 UNIQUEIDENTIFIER

    SET @UserId1 = NEWID()
    SET @UserId2 = NEWID()
    SET @Userid3 = NEWID()
    SET @Userid4 = NEWID()
    SET @UserId5 = NEWID()
    SET @Userid6 = NEWID()
    SET @GroupId1 = NEWID()
    SET @GroupId2 = NEWID()
    SET @GroupId3 = NEWID()

INSERT INTO ChatUser(Id, Login, PswdHash, HashSalt)
VALUES (@UserId1, 'victor', '1234', 'aaa');
INSERT INTO ChatUser(Id, Login, PswdHash, HashSalt)
VALUES (@UserId2, 'killer_stalker2008', 'sf34', 'asdf');
INSERT INTO ChatUser(Id, Login, PswdHash, HashSalt)
VALUES (@Userid3, 'alexdarkstalker98', 'qwert', '1222');
INSERT INTO ChatUser(Id, Login, PswdHash, HashSalt)
VALUES (@Userid4, 'kolyan228', 'sdfsdfs', 'qqqq');
INSERT INTO ChatUser(Id, Login, PswdHash, HashSalt)
VALUES (@UserId5, 'egor420', 'lolololol', 'fff');
INSERT INTO ChatUser(Id, Login, PswdHash, HashSalt)
VALUES (@Userid6, 'denis8888', 'zxcv', 'sfdf');

INSERT INTO ChatGroup(Id, Name)
VALUES (@GroupId1, N'Родители учеников 3Б');
INSERT INTO ChatGroup(Id, Name)
VALUES (@GroupId2, N'Йога (Геометрия фитнеса)');
INSERT INTO ChatGroup(Id, Name)
VALUES (@GroupId3, N'Объявления ПМИ 3-й курс');

INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId1, @GroupId1);
INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId1, @GroupId3);

INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId1, @UserId5);
INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId5, @UserId1);
INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId1, @UserId6);
INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId6, @UserId1);
INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId1, @UserId2);
INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId2, @UserId1);

INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId3, @UserId2);
INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId2, @UserId3);

INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId2, @GroupId3);
INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId2, @GroupId1);
INSERT INTO ChatUserChatRelation(UserId, ChatId)
VALUES (@UserId2, @GroupId2);

INSERT INTO ChatTextMsg(SenderId, ReceiverId, Content, SentTime)
VALUES (@UserId1, @GroupId3, N'Чуваки, че задали на завтра?', GETDATE());
INSERT INTO ChatTextMsg(SenderId, ReceiverId, Content, SentTime)
VALUES (@UserId2, @GroupId3, N'Реферат по матфизике. Вроде больше ничего.', GETDATE());

INSERT INTO ChatTextMsg(SenderId, ReceiverId, Content, SentTime)
VALUES (@UserId1, @UserId2, N'Йоу, кинь номер своег мастера по ноготочкам', GETDATE());
INSERT INTO ChatTextMsg(SenderId, ReceiverId, Content, SentTime)
VALUES (@UserId2, @UserId1, N'Здарова, вот, +7-228-420-12345', GETDATE());