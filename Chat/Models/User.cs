using System;

namespace Chat.Models
{
    /// <summary>
    /// Описывает пользователя
    /// </summary>
    /// <inheritdoc cref="BaseReceiver"/>>
    public class User : BaseReceiver
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }
    }
}