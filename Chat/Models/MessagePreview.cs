using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chat.Models
{
    /// <summary>
    /// Содержит информацию для превью чата
    /// </summary>
    public class MessagePreview
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
        public string ChatName { get; set; }

        /// <summary>
        /// Последнее сообщение в чате
        /// </summary>
        public string LastMessage { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime SentTime { get; set; }
    }
}