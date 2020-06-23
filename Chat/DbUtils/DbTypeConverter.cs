using System;
using System.Data;

namespace Chat.DbUtils
{
    /// <summary>
    /// Статический класс, реализующий методы для преобразования типов из БД в типы C#
    /// </summary>
    public static class DbTypeConverter
    {
        /// <summary>
        /// Возвращает объект данного типа
        /// </summary>
        /// <param name="dataReader">reader по результирующей строке</param>
        /// <param name="name">Имя столбца</param>
        /// <returns></returns>
        public static Guid GetGuid(this IDataReader dataReader, string name)
        {
            return (Guid) dataReader[name];
        }

        /// <inheritdoc cref="GetGuid"/>
        public static string GetString(this IDataReader dataReader, string name)
        {
            return dataReader[name].ToString();
        }

        /// <inheritdoc cref="GetGuid"/>
        public static int GetInt(this IDataReader dataReader, string name)
        {
            return (int) dataReader[name];
        }

        /// <inheritdoc cref="GetGuid"/>
        public static DateTime GetDateTime(this IDataReader dataReader, string name)
        {
            return (DateTime) dataReader[name];
        }
    }
}