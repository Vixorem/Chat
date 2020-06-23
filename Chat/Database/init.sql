-- =============================================
-- Author:      Шулепкин В.А.
-- Create date: 03.09.2020
-- Description: Инизиализация таблиц
-- =============================================

CREATE TABLE ChatUser
(
    Id       UNIQUEIDENTIFIER,
    Login    NVARCHAR(50) UNIQUE,
    PswdHash NVARCHAR(128),
    HashSalt NVARCHAR(64)
);

CREATE TABLE ChatGroup
(
    Id   UNIQUEIDENTIFIER,
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE ChatTextMsg
(
    Id         INT IDENTITY(1, 1) PRIMARY KEY,
    ReceiverId UNIQUEIDENTIFIER,
    SenderId   UNIQUEIDENTIFIER,
    Content    NVARCHAR(1000) NOT NULL,
    SentTime   DATETIME2      NOT NULL
);

CREATE TABLE ChatUserChatRelation
(
    Id     INT IDENTITY(1, 1) PRIMARY KEY,
    UserId UNIQUEIDENTIFIER,
    ChatId UNIQUEIDENTIFIER,
    CONSTRAINT UsrChatUQ UNIQUE (UserId, ChatId)
);