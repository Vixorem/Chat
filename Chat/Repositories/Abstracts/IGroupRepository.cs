using System;
using System.Collections.Generic;
using Chat.Models;

namespace Chat.Repositories.Abstracts
{
    /// <summary>
    /// Интерфейс предназначен для работы с групповыми чатами
    /// </summary>
    public interface IGroupRepository
    {
        /// <summary>
        /// Получает группу по Id
        /// </summary>
        /// <param name="groupId">Guid группы</param>
        Group GetById(Guid groupId);

        /// <summary>
        /// Получает список групп, в которых состоит юзер
        /// </summary>
        /// <param name="userId">Guid юзера</param>
        IList<Group> GetGroupsForUser(Guid userId);

        /// <summary>
        /// Создает группу
        /// </summary>
        /// <param name="groupId">Guid группы</param>
        /// <param name="name">Имя группы</param>
        void Add(Guid groupId, string name);

        /// <summary>
        /// Удаляет юзера из группы
        /// </summary>
        /// <param name="groupId">Группа, из которой удаляют</param>
        /// <param name="removeeId">Guid юзера, которого удаляют</param>
        void KickUser(Guid groupId, Guid removeeId);
    }
}