using System;
using System.Collections.Generic;
using Chat.ClientModels;
using Chat.Models;
using Chat.Services;
using Chat.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        
        /// <summary>
        /// .ctor
        /// </summary>
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Возвращает историю сообщений для линчной или групповой беседы
        /// </summary>
        /// <param name="chatId">Guid группы или беседы, из которых загружается история</param>
        /// <param name="userId">Guid пользователя, для которого загружается история</param>
        /// <param name="offset">Сдвиг от последнего сообщения в чате</param>
        /// <param name="limit">Кол-во возвращенных записей</param>
        [HttpGet]
        [Route("getchathistory")]
        public ServiceResponse<IList<MessageDto>> GetChatHistory(Guid chatId, Guid userId, int offset, int limit)
        {
            return _messageService.GetTextMessageFromChat(chatId, userId, offset, limit);;
        }

        /// <summary>
        /// Возвращает превью сообщений чата
        /// </summary>
        /// <param name="userId">Guid пользователя</param>
        /// <param name="offset">Сдвиг от последнего превью</param>
        /// <param name="limit">Кол-во возвращенных записей</param>
        [HttpGet]
        [Route("getmessagepreviewsforuser")]
        public ServiceResponse<IList<MessagePreviewDto>> GetMessagePreviewsForUser(Guid userId, int offset, int limit)
        {
            return _messageService.GetMessagePreviewsForUser(userId, offset, limit);
        }
        
        /// <summary>
        /// Отправляет сообщение
        /// </summary>
        /// <param name="senderId">Guid отправителя</param>
        /// <param name="receiverId">Guid получателя</param>
        /// <param name="content">Содержимое текстового сообщения</param>
        [HttpPost]
        [Route("sendtextmessage")]
        public ServiceResponse SendTextMessage(Guid senderId, Guid receiverId, string content)
        {
            return _messageService.SaveTextMessage(senderId, receiverId, content);
        }
    }
}