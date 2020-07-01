using System;
using System.Collections.Generic;
using Chat.ClientModels;
using Chat.Repositories.Abstracts;
using Chat.Services;
using Chat.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    /// <summary>
    /// Реализует методы для работы с пользователями
    /// </summary>
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        /// <summary>
        /// .ctor
        /// </summary>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Возвращает пользователя по Guid
        /// </summary>
        /// <param name="userId">Guid пользователя</param>
        [HttpGet]
        [Route("getuserbyid")]
        public ServiceResponse<UserDto> GetUserById(Guid userId)
        {
            return _userService.GetById(userId);
        }
        
        /// <summary>
        /// Возвращает список личных бесед
        /// </summary>
        /// <param name="userId">Guid пользователя, для которого загружается список</param>
        [HttpGet]
        [Route("getexistingdialogsforuser")]
        public ServiceResponse<IList<UserDto>> GetExistingDialogsForUser(Guid userId)
        {
            return _userService.GetDialogsForUser(userId);
        }
        
        /// <summary>
        /// Возвращает пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        [HttpGet]
        [Route("getuserbylogin")]
        public ServiceResponse<UserDto> GetUserByLogin(string login)
        {
            return _userService.GetByLogin(login);
        }
        
        /// <summary>
        /// Возвращает список участников беседы
        /// </summary>
        /// <param name="groupId">Guid группового чата</param>
        /// <param name="userId">Guid пользователя, для которого выполняется запрос</param>
        [HttpGet]
        [Route("getusersingroup")]
        public ServiceResponse<IList<UserDto>> GetUsersInGroup(Guid groupId, Guid userId)
        {
            return _userService.GetUsersInGroup(groupId, userId);
        }
        
        /// <summary>
        /// Добавляет пользователя в группу
        /// </summary>
        /// <param name="addeeId">Guid пользователя, которого добавляют</param>
        /// <param name="adderId">Guid пользователя, который добавляет</param>
        /// <param name="groupId">Guid группы</param>
        [HttpPost]
        [Route("addusertogroup")]
        public ServiceResponse AddUserToGroup(Guid addeeId, Guid adderId, Guid groupId)
        {
            return _userService.AddToGroup(addeeId, adderId, groupId);
        }

        /// <summary>
        /// Создает личную беседу
        /// </summary>
        /// <param name="initiatorId">Инициатор беседы</param>
        /// <param name="interlocutorId">Собеседник</param>
        [HttpPost]
        [Route("startconvowithuser")]
        public ServiceResponse StartConvoWithUser(Guid initiatorId, Guid interlocutorId)
        {
            return _userService.StartDialog(initiatorId, interlocutorId);
        }
        
        /// <summary>
        /// Регистрирует нового пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        [HttpPost]
        [Route("signupuser")]
        public JsonResult SignUpUser(string login, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        [HttpPost]
        [Route("authenticateuser")]
        public JsonResult AuthenticateUser(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}