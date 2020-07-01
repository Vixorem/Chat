using System;
using System.Collections.Generic;
using Chat.ClientModels;
using Chat.Services;
using Chat.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
        [HttpPost]
        [Route("removeuserfromgroup")]
        public ServiceResponse RemoveUserFromGroup(string guidSet)
        {
            var guids = JObject.Parse(guidSet);
            return _groupService.KickUser(Guid.Parse(guids["groupId"].ToString()),
                Guid.Parse(guids["removeeId"].ToString()),
                Guid.Parse(guids["removerId"].ToString()));
        }
    }
}