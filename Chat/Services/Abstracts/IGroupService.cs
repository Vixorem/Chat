﻿using System;
using System.Collections.Generic;
using Chat.ClientModels;
using Chat.Models;

namespace Chat.Services.Abstracts
{
    /// <summary>
    /// Сервис групповой беседы
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Возвращает объект группы
        /// </summary>
        /// <param name="groupId">Guid группы</param>
        ServiceResponse<GroupDto> GetById(Guid groupId);

        /// <summary>
        /// Возвращает список групп, в которых учавствует пользователь
        /// </summary>
        /// <param name="userId">Guid пользователя</param>
        ServiceResponse<IList<GroupDto>> GetGroupsForUser(Guid userId);

        /// <summary>
        /// Создает группу
        /// </summary>
        /// <param name="name">Название группы</param>
        ServiceResponse Add(string name);

        /// <summary>
        /// Исключает пользователя из группы
        /// </summary>
        /// <param name="groupId">Guid группы</param>
        /// <param name="removeeId">Guid удаляемого</param>
        /// <param name="removerId">Guid удаляющего</param>
        ServiceResponse KickUser(Guid groupId, Guid removeeId, Guid removerId);
    }
}