using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Chat.ClientModels;
using Chat.Models;
using Chat.Repositories.Abstracts;
using Chat.Services.Abstracts;
using Chat.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Chat.Services.Implementations
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// .ctor
        /// </summary>
        public UserService(IUserRepository userRepository, ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc cref="IUserService"/>
        public ServiceResponse<UserDto> GetById(Guid userId)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                if (userId.IsEmpty())
                    return ServiceResponse<UserDto>.Warning(GuidIsNotCorrect);

                var user = _userRepository.GetById(userId);
                return user == null
                    ? ServiceResponse<UserDto>.Warning(UserNotFound)
                    : ServiceResponse<UserDto>.Ok(UserDto.ConvertFromDomain(user));
            });
        }

        /// <inheritdoc cref="IUserService"/>
        public ServiceResponse<UserDto> GetByLogin(string login)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                if (string.IsNullOrWhiteSpace(login))
                    return ServiceResponse<UserDto>.Warning(LoginIsNotCorrect);

                var user = _userRepository.GetByLogin(login);
                return user == null
                    ? ServiceResponse<UserDto>.Warning(UserNotFound)
                    : ServiceResponse<UserDto>.Ok(UserDto.ConvertFromDomain(user));
            });
        }

        /// <inheritdoc cref="IUserService"/>
        public ServiceResponse<IList<UserDto>> GetUsersInGroup(Guid groupId, Guid userId)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                if (groupId.IsEmpty())
                    return ServiceResponse<IList<UserDto>>.Warning(GuidIsNotCorrect);

                var belongs = _userRepository.DoesUserBelongToChat(groupId, userId);
                if (!belongs)
                    return ServiceResponse<IList<UserDto>>.Warning(ChatInfoAccessDenied);

                var users = _userRepository.GetUsersInGroup(groupId);
                return ServiceResponse<IList<UserDto>>.Ok(users.Select(UserDto.ConvertFromDomain).ToList());
            });
        }

        /// <inheritdoc cref="IUserService"/>
        public ServiceResponse<IList<UserDto>> GetDialogsForUser(Guid userId)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                if (userId.IsEmpty())
                    return ServiceResponse<IList<UserDto>>.Warning(GuidIsNotCorrect);

                var interlocutors = _userRepository.GetDialogsForUser(userId);
                return ServiceResponse<IList<UserDto>>.Ok(interlocutors.Select(UserDto.ConvertFromDomain).ToList());
            });
        }

        /// <inheritdoc cref="IUserService"/>
        public ServiceResponse BindToChat(Guid bindeeId, Guid adderId, Guid chatId)
        {
            return ExecuteWithCatch(() =>
            {
                if (bindeeId.IsEmpty() || chatId.IsEmpty())
                    return ServiceResponse.Warning(GuidIsNotCorrect);

                var bindeeBelongs = _userRepository.DoesUserBelongToChat(chatId, bindeeId);
                if (bindeeBelongs)
                    return ServiceResponse.Warning(TheChatAlreadyExists);

                var adderBelongs = _userRepository.DoesUserBelongToChat(chatId, adderId);
                if (!adderBelongs)
                    return ServiceResponse.Warning(NotMemberAdder);

                _userRepository.BindToChat(bindeeId, chatId);
                return ServiceResponse.Ok();
            });
        }

        /// <inheritdoc cref="IUserService"/>
        public ServiceResponse StartConvoWithUser(Guid initiatorId, Guid interlocutorId)
        {
            return ExecuteWithCatch(() =>
            {
                if (initiatorId.IsEmpty() || interlocutorId.IsEmpty())
                    return ServiceResponse.Warning(GuidIsNotCorrect);

                var bindeeBelongs = _userRepository.DoesUserBelongToChat(initiatorId, interlocutorId);
                if (bindeeBelongs)
                    return ServiceResponse.Warning(TheChatAlreadyExists);

                _userRepository.BindToChat(initiatorId, interlocutorId);
                return ServiceResponse.Ok();
            });
        }
    }
}