using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Chat.ClientModels;
using Chat.Models;
using Chat.Repositories.Abstracts;
using Chat.Services.Abstracts;
using Chat.Utils.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Chat.Services.Implementations
{
    /// <inheritdoc cref="IMessageService"/>
    public class MessageService : BaseService, IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// .ctor
        /// </summary>
        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository,
            ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        /// <inheritdoc cref="IMessageService"/>
        public ServiceResponse<MessageDto> SaveTextMessage(Guid senderId, Guid receiverId, string content)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                if (senderId.IsEmpty() || receiverId.IsEmpty())
                    return ServiceResponse<MessageDto>.Warning(GuidIsNotCorrect);

                if (string.IsNullOrWhiteSpace(content))
                    return ServiceResponse<MessageDto>.Warning(EmptyContentNotAllowed);

                if (!_userRepository.DoesUserBelongToChat(receiverId, senderId))
                    return ServiceResponse<MessageDto>.Warning(NotMemberSender);

                var res = _messageRepository.SaveMessage(senderId, receiverId, content, DateTime.Now);
                return ServiceResponse<MessageDto>.Ok(new MessageDto
                {
                    Id = res.id,
                    // Sender должен быть перезаписан в вызываемых методах
                    Sender = default(UserDto),
                    Content = content,
                    Status = Status.Received,
                    ReceiverId = receiverId,
                    SentTime = res.sentTime
                });
            });
        }

        /// <inheritdoc cref="IMessageService"/>
        public ServiceResponse<IList<MessagePreviewDto>> GetMessagePreviewsForUser(Guid userId, int offset, int limit)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                if (userId.IsEmpty())
                    return ServiceResponse<IList<MessagePreviewDto>>.Warning(GuidIsNotCorrect);

                if (offset < 0 || limit < 0)
                    return ServiceResponse<IList<MessagePreviewDto>>.Warning(NumberNotPositive);

                var previews = _messageRepository.GetMessagePreviewsForUser(userId, offset, limit);
                return ServiceResponse<IList<MessagePreviewDto>>.Ok(previews.Select(MessagePreviewDto.ConvertFromDomain)
                    .ToList());
            });
        }

        /// <inheritdoc cref="IMessageService"/>
        public ServiceResponse<IList<MessageDto>> GetTextMessageFromChat(Guid chatId, Guid userId, int offset,
            int limit)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                if (chatId.IsEmpty() || userId.IsEmpty())
                    return ServiceResponse<IList<MessageDto>>.Warning(GuidIsNotCorrect);

                if (offset < 0 || limit < 0)
                    return ServiceResponse<IList<MessageDto>>.Warning(NumberNotPositive);

                var belongs = _userRepository.DoesUserBelongToChat(chatId, userId);
                if (!belongs)
                    return ServiceResponse<IList<MessageDto>>.Warning(HistoryAccessDenied);

                var user = _userRepository.GetById(chatId);
                if (user == null)
                    return ServiceResponse<IList<MessageDto>>.Ok(_messageRepository
                        .GetTextMessageFromGroup(chatId, offset, limit)
                        .Select(MessageDto.ConvertFromDomain)
                        .ToList());

                return ServiceResponse<IList<MessageDto>>.Ok(_messageRepository
                    .GetTextMessageFromInterlocutor(chatId, userId, offset, limit)
                    .Select(MessageDto.ConvertFromDomain)
                    .ToList());
            });
        }
    }
}