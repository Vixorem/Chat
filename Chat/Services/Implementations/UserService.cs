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
        public ServiceResponse AddToGroup(Guid addeeId, Guid adderId, Guid chatId)
        {
            return ExecuteWithCatch(() =>
            {
                if (addeeId.IsEmpty() || chatId.IsEmpty())
                    return ServiceResponse.Warning(GuidIsNotCorrect);

                var bindeeBelongs = _userRepository.DoesUserBelongToChat(chatId, addeeId);
                if (bindeeBelongs)
                    return ServiceResponse.Warning(TheChatAlreadyExists);

                var adderBelongs = _userRepository.DoesUserBelongToChat(chatId, adderId);
                if (!adderBelongs)
                    return ServiceResponse.Warning(NotMemberAdder);

                _userRepository.BindToChat(addeeId, chatId);
                return ServiceResponse.Ok();
            });
        }

        /// <inheritdoc cref="IUserService"/>
        public ServiceResponse BindToChat(Guid userId, Guid groupId)
        {
            return ExecuteWithCatch(() =>
            {
                if (userId.IsEmpty() || groupId.IsEmpty())
                    return ServiceResponse.Warning(GuidIsNotCorrect);

                var bindeeBelongs = _userRepository.DoesUserBelongToChat(userId, groupId);
                if (bindeeBelongs)
                    return ServiceResponse.Warning(TheChatAlreadyExists);

                _userRepository.BindToChat(userId, groupId);
                return ServiceResponse.Ok();
            });
        }

        /// <inheritdoc cref="IUserService"/>
        public ServiceResponse StartDialog(Guid firstUserId, Guid secondUserId)
        {
            return ExecuteWithCatch(() =>
            {
                var res1 = BindToChat(firstUserId, secondUserId);
                if (res1.ResultType != ServiceResultType.Ok)
                    return res1;
                var res2 = BindToChat(secondUserId, firstUserId);
                if (res2.ResultType != ServiceResultType.Ok)
                    return res2;

                return ServiceResponse.Ok();
            });
        }
    }
}