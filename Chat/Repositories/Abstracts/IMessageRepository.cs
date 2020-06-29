using System;
using System.Collections.Generic;
using Chat.Models;

namespace Chat.Repositories.Abstracts
{
    /// <summary>
    /// Интерфейс предназначен для работы с сообщениями
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Сохраняет сообщение
        /// </summary>
        /// <param name="senderId">Guid отправителя</param>
        /// <param name="receiverId">Guid получателя</param>
        /// <param name="content">Текстовое содержимое</param>
        /// <param name="time">Время отправки</param>
        /// <returns>Id сохраненного сообщения</returns>
        int SaveMessage(Guid senderId, Guid receiverId, string content, DateTime time);

        /// <summary>
        /// Возвращает превью сообщений для пользователя
        /// </summary>
        /// <param name="userId">Guid пользователя</param>
        /// <param name="offset">Сдвиг от последнего сообщения</param>
        /// <param name="limit">Кол-во возвращаемых значений</param>
        /// <returns></returns>
        IList<MessagePreview> GetMessagePreviewsForUser(Guid userId, int offset, int limit);

        /// <summary>
        /// Возвращает сообщение из чата с собеседником
        /// </summary>
        /// <param name="interlocutorId">GUID юзера-собеседника</param>
        /// <param name="userId">Guid пользователя, который запрашивает историю</param>
        /// <param name="offset">Сдвиг от последнего сообщения</param>
        /// <param name="limit">Кол-во возвращаемых сообщений за раз</param>
        IList<TextMessage> GetTextMessageFromInterlocutor(Guid interlocutorId, Guid userId, int offset, int limit);

        /// <summary>
        /// Возвращает сообщение из группового чата
        /// </summary>
        /// <param name="groupId">GUID группы</param>
        /// <param name="offset">Сдвиг от последнего сообщения</param>
        /// <param name="limit">Кол-во возвращаемых сообщений за раз</param>
        IList<TextMessage> GetTextMessageFromGroup(Guid groupId, int offset, int limit);
    }
}