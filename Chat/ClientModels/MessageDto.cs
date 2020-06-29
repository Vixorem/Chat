using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Chat.Models;
using Newtonsoft.Json;

namespace Chat.ClientModels
{
    /// <summary>
    /// Клиентская модель текстового сообщения
    /// </summary>
    [DataContract]
    public class MessageDto
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        [DataMember]
        [JsonProperty("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Текстовое содержимое сообщения
        /// </summary>
        [DataMember]
        [JsonProperty("Content")]
        public string Content { get; set; }
        
        /// <summary>
        /// Статус сообщения
        /// </summary>
        [DataMember]
        [JsonProperty("Status")]
        public Status Status { get; set; }

        /// <summary>
        /// Отправитель сообщения
        /// </summary>
        [DataMember]
        [JsonProperty("Sender")]
        public UserDto Sender { get; set; }

        /// <summary>
        /// Получатель сообщения
        /// </summary>
        [DataMember]
        [JsonProperty("ReceiverId")]
        public Guid ReceiverId { get; set; }
        
        /// <summary>
        /// Идентификатор, сформированный на стороне клиента
        /// </summary>
        public string ClientMessageId { get; set; } = "";

        /// <summary>
        /// Время отправки
        /// </summary>
        [DataMember]
        [JsonProperty("SentTime")]
        public DateTime SentTime { get; set; }

        /// <summary>
        /// Конвертирует модель бизнес-логики в клиентскую модель
        /// </summary>
        /// <param name="textMessage">Экземпляр модели бизнес-логики</param>
        public static MessageDto ConvertFromDomain(TextMessage textMessage)
        {
            return new MessageDto
            {
                Id = textMessage.Id,
                Content = textMessage.Content,
                ReceiverId = textMessage.ReceiverId,
                Sender = UserDto.ConvertFromDomain(textMessage.Sender),
                SentTime = textMessage.SentTime,
                Status = Status.Received
            };
        }
    }
}