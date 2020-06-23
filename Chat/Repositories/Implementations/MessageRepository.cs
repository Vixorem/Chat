using System;
using System.Collections.Generic;
using Chat.DbUtils;
using Chat.Models;
using Chat.Repositories.Abstracts;

namespace Chat.Repositories.Implementations
{
    ///<inheritdoc cref="IMessageRepository"/>
    public class MessageRepository : IMessageRepository
    {
        private readonly DbRequest _db;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="db"></param>
        public MessageRepository(DbRequest db)
        {
            _db = db;
        }

        ///<inheritdoc cref="IMessageRepository"/>
        public void SaveMessage(Guid senderId, Guid receiverId, string content, DateTime time)
        {
            _db.ExecuteNonQuery(
                "ChatCreateMsg",
                new DbParam("@Content", content),
                new DbParam("@ReceiverId", receiverId),
                new DbParam("@SenderId", senderId),
                new DbParam("@SentTime", time)
            );
        }

        public IList<MessagePreview> GetMessagePreviewsForUser(Guid userId, int offset, int limit)
        {
            return _db.GetItemListFromEntries("ChatGetPreviewMessagesForUser", dataReader => new MessagePreview
            {
                ChatId = dataReader.GetGuid("ChatId"),
                ChatName = dataReader.GetString("ChatName"),
                LastMessage = dataReader.GetString("Content"),
                SentTime = dataReader.GetDateTime("SentTime")
            }, new DbParam("@UserId", userId), new DbParam("@Offset", offset), new DbParam("@Limit", limit));
        }

        ///<inheritdoc cref="IMessageRepository"/>
        public IList<TextMessage> GetTextMessageFromInterlocutor(Guid interlocutorId, Guid userId, int offset,
            int limit)
        {
            return _db.GetItemListFromEntries("ChatGetTextMsgForUserId", dataReader =>
                    new TextMessage
                    {
                        Id = dataReader.GetInt("Id"),
                        Sender = new User
                        {
                            Id = dataReader.GetGuid("SenderId"),
                            Login = dataReader.GetString("SenderLogin")
                        },
                        ReceiverId = dataReader.GetGuid("ReceiverId"),
                        Content = dataReader.GetString("Content"),
                        SentTime = dataReader.GetDateTime("SentTime")
                    },
                new DbParam("@ChatId", interlocutorId),
                new DbParam("@UserId", userId),
                new DbParam("@PageShift", offset),
                new DbParam("@FetchNum", limit));
        }

        ///<inheritdoc cref="IMessageRepository"/>
        public IList<TextMessage> GetTextMessageFromGroup(Guid groupId, int shift, int fetchNum)
        {
            return
                _db.GetItemListFromEntries("ChatGetTextMsgForGroupId", dataReader => new TextMessage
                    {
                        Id = dataReader.GetInt("Id"),
                        Sender = new User
                        {
                            Id = dataReader.GetGuid("SenderId"),
                            Login = dataReader.GetString("SenderLogin")
                        },
                        ReceiverId = dataReader.GetGuid("ReceiverId"),
                        Content = dataReader.GetString("Content"),
                        SentTime = dataReader.GetDateTime("SentTime")
                    },
                    new DbParam("@ChatId", groupId),
                    new DbParam("@PageShift", shift),
                    new DbParam("@FetchNum", fetchNum));
        }
    }
}