using System;

namespace Chat.Models
{
    /// <summary>
    /// Класс описывает групповые чаты
    /// </summary>
    ///<inheritdoc/>
    public class Group : BaseReceiver
    {
        /// <summary>
        /// Название группы
        /// </summary>
        public string Name { get; set; }
    }
}