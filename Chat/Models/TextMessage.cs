using System;
using Chat.Repositories.Implementations;

namespace Chat.Models
{
    /// <summary>
    /// Описывает текстовые сообщения
    /// </summary>
    public class TextMessage
    {
        /// <summary>
        /// Id сообщения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Guid получателя
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public User Sender { get; set; }

        /// <summary>
        /// Текстовое содержимое
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime SentTime { get; set; }
    }
}