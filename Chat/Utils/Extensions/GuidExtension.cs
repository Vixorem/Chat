using System;

namespace Chat.Utils.Extensions
{
    /// <summary>
    /// Расширяет к
    /// </summary>
    public static class GuidExtension
    {
        /// <summary>
        /// Проверяет корректность guid
        /// </summary>
        /// <returns>true при найденных корректных данных, иначе false</returns>
        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }
    }
}