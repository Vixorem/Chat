namespace Chat.DbUtils
{
    /// <summary>
    /// Содержит связку (имя параметра, значение параметра) для хранимой процедуры
    /// </summary>
    public class DbParam
    {
        /// <summary>
        /// Имя параметра
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение параметра
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <param name="value">Значение параметра</param>
        public DbParam(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}