using System;

namespace Chat.Models
{
    /// <summary>
    /// Класс, который должны реализовывать получатели сообщений
    /// </summary>
    public class BaseReceiver
    {
        /// <summary>
        /// Guid 
        /// </summary>
        public Guid Id { get; set; }
    }
}