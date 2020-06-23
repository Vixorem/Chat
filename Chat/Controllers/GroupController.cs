using System;
using System.Collections.Generic;
using Chat.ClientModels;
using Chat.Services;
using Chat.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        
        /// <summary>
        /// .ctor
        /// </summary>
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        /// <summary>
        /// Возвращает группу
        /// </summary>
        /// <param name="groupId">Guid группы</param>
        [HttpGet]
        [Route("getgroupbyid")]
        public ServiceResponse<GroupDto> GetGroupById(Guid groupId)
        {
            return _groupService.GetById(groupId);
        }
        
        /// <summary>
        /// Возвращает список групповых бесед
        /// </summary>
        /// <param name="userId">Guid пользователя, для которого загружается список</param>
        [HttpGet]
        [Route("getexistinggroupsforuser")]
        public ServiceResponse<IList<GroupDto>> GetExistingGroupsForUser(Guid userId)
        {
            return _groupService.GetGroupsForUser(userId);
        }
        
        /// <summary>
        /// Удаляет пользователя из группы
        /// </summary>
        /// <param name="groupId">Guid группы</param>
        /// <param name="removerId">Guid пользователя, который удаляет</param>
        /// <param name="removeeId">Guid пользователя, которого удаляют</param>
        [HttpPost]
        [Route("removeuserfromgroup")]
        public ServiceResponse RemoveUserFromGroup(Guid groupId, Guid removerId, Guid removeeId)
        {
            return _groupService.KickUser(groupId, removeeId, removerId);
        }
    }
}