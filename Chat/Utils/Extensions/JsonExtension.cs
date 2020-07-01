using System;
using Chat.ClientModels;
using Newtonsoft.Json.Linq;

namespace Chat.Utils.Extensions
{
    /// <summary>
    /// Расширение для типа JObject
    /// </summary>
    public static class JsonExtension
    {
        /// <summary>
        /// Конвертирует json строку в MessageDto
        /// </summary>
        /// <param name="obj">Экземпляр JObject</param>
        public static MessageDto ToMessageDto(this JObject obj)
        {
            var senderId = Guid.Parse(obj["sender"]["id"].ToString());
            var senderLogin = obj["sender"]["id"].ToString();
            var receiverId = Guid.Parse(obj["receiverId"].ToString());
            var content = obj["content"].ToString();
            var clientMesId = obj["clientMessageId"].ToString();
            return new MessageDto
            {
                // Id должно быть перезаписано в вызываемых методах
                Id = 0,
                Content = content,
                Sender = new UserDto
                {
                    Id = senderId,
                    Login = senderLogin
                },
                ReceiverId = receiverId,
                // SentTime должно быть перезаписано в вызываемых методах
                SentTime = default,
                Status = Status.Received,
                ClientMessageId = clientMesId
            };
        }

        /// <summary>
        /// Конвертирует json строку в GroupDto
        /// </summary>
        /// <param name="obj">Экземпляр JObject</param>
        public static GroupDto ToGroupDto(this JObject obj)
        {
            var name = obj["group"]["name"].ToString();
            return new GroupDto
            {
                // Id должно быть перезаписано в вызываемых методах
                Id = Guid.Empty,
                Name = name
            };
        }
    }
}