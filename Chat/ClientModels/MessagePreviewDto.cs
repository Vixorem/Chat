using System;
using System.Runtime.Serialization;
using Chat.Models;
using Newtonsoft.Json;

namespace Chat.ClientModels
{
    /// <summary>
    /// Содержит информацию для превью чата
    /// </summary>
    [DataContract]
    public class MessagePreviewDto
    {
        /// <summary>
        /// Guid чата (пользователя или группы)
        /// </summary>
        [DataMember]
        [JsonProperty("ChatId")]
        public Guid ChatId { get; set; }

        /// <summary>
        /// Название чата (имя группы или логин пользователя)
        /// </summary>
        [DataMember]
        [JsonProperty("ChatName")]
        public string ChatName { get; set; }

        /// <summary>
        /// Последнее сообщение в чате
        /// </summary>
        [DataMember]
        [JsonProperty("LastMessage")]
        public string LastMessage { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        [DataMember]
        [JsonProperty("SentTime")]
        public DateTime SentTime { get; set; }

        /// <summary>
        /// Конвертирует модель бизнес-логики в клиентскую модель
        /// </summary>
        /// <param name="preview">Экземпляр модели бизнес-логики</param>
        public static MessagePreviewDto ConvertFromDomain(MessagePreview preview)
        {
            return new MessagePreviewDto
            {
                ChatId = preview.ChatId,
                ChatName = preview.ChatName,
                LastMessage = preview.LastMessage,
                SentTime = preview.SentTime
            };
        }
    }
}