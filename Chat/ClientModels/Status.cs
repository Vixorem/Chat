namespace Chat.ClientModels
{
    /// <summary>
    /// Статус сообщения
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Если сообщение отправлено с клиента и еще не пришло на сервер
        /// </summary>
        Sent,

        /// <summary>
        /// Если сообщение дошло до сервера
        /// </summary>
        Received
    }
}