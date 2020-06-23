﻿using System;
using System.Collections.Generic;
using Chat.ClientModels;
using Chat.Models;

namespace Chat.Services.Abstracts
{
    /// <summary>
    /// Сервис сообщений
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Сохраняет текстовое сообщение
        /// </summary>
        /// <param name="senderId">Guid отправителя</param>
        /// <param name="receiverId">Guid получателя</param>
        /// <param name="content">Содержание текстового сообщения</param>
        ServiceResponse SaveTextMessage(Guid senderId, Guid receiverId, string content);

        /// <summary>
        ///  Возвращает превью для чатов
        /// </summary>
        /// <param name="userId">Guid пользователя</param>
        /// <param name="offset">Сдвиг от последнего превью</param>
        /// <param name="limit">Кол-во сообщений</param>
        /// <returns></returns>
        ServiceResponse<IList<MessagePreviewDto>> GetMessagePreviewsForUser(Guid userId, int offset, int limit);

        /// <summary>
        /// Возвращает историю сообщений с чатом
        /// </summary>
        /// <param name="chatId">Guid обеседника или групповой беседы</param>
        /// <param name="userId">Guid пользователя, для которого загружается история</param>
        /// <param name="offset">Сдвиг от последнего сообщения</param>
        /// <param name="limit">Верхняя граница кол-ва возвращаемых записей</param>
        //TODO: Нужно хэшировать chatId, чтобы заранее знать, группа это или пользователь
        ServiceResponse<IList<MessageDto>> GetTextMessageFromChat(Guid chatId, Guid userId, int offset, int limit);
    }
}