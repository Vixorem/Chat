﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Chat.ClientModels;
using Chat.Models;
using Chat.Repositories.Abstracts;
using Chat.Repositories.Implementations;
using Chat.Services.Abstracts;
using Chat.Utils.Extensions;
using Microsoft.Extensions.Logging;

namespace Chat.Services.Implementations
{
    /// <inheritdoc cref="IGroupService"/>
    public class GroupService : BaseService, IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// .ctor
        /// </summary>
        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository,
            ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        /// <inheritdoc cref="IGroupService"/>
        public ServiceResponse<GroupDto> GetById(Guid groupId)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                if (groupId.IsEmpty())
                    return ServiceResponse<GroupDto>.Warning(GuidIsNotCorrect);
                var group = _groupRepository.GetById(groupId);
                if (group == null)
                    return ServiceResponse<GroupDto>.Warning(GroupNotFound);
                return ServiceResponse<GroupDto>.Ok(GroupDto.ConvertFromDomain(group));
            });
        }

        /// <inheritdoc cref="IGroupService"/>
        public ServiceResponse<IList<GroupDto>> GetGroupsForUser(Guid userId)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                if (userId.IsEmpty())
                    return ServiceResponse<IList<GroupDto>>.Warning(GuidIsNotCorrect);

                var groups = _groupRepository.GetGroupsForUser(userId);
                return ServiceResponse<IList<GroupDto>>.Ok(groups.Select(GroupDto.ConvertFromDomain).ToList());
            });
        }

        /// <inheritdoc cref="IGroupService"/>
        public ServiceResponse<GroupDto> Add(string groupName, Guid groupCreatorId)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                if (string.IsNullOrWhiteSpace(groupName))
                    return ServiceResponse<GroupDto>.Warning(LoginIsNotCorrect);

                var group = _groupRepository.Add(Guid.NewGuid(), groupName);
                return ServiceResponse<GroupDto>.Ok(GroupDto.ConvertFromDomain(group));
            });
        }

        /// <inheritdoc cref="IGroupService"/>
        public ServiceResponse KickUser(Guid groupId, Guid removeeId, Guid removerId)
        {
            return ExecuteWithCatch(() =>
            {
                if (groupId.IsEmpty() || removeeId.IsEmpty() || removeeId.IsEmpty())
                    return ServiceResponse.Warning(GuidIsNotCorrect);

                if (!_userRepository.DoesUserBelongToChat(groupId, removerId))
                    return ServiceResponse.Warning(NotMemberKicker);

                if (!_userRepository.DoesUserBelongToChat(groupId, removeeId))
                    return ServiceResponse.Warning(NotMemberRemovee);

                _groupRepository.KickUser(groupId, removeeId);
                return ServiceResponse.Ok();
            });
        }
    }
}