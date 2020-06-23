﻿using System;
using System.Collections.Generic;
using Chat.ClientModels;
using Chat.Models;

namespace Chat.Services.Abstracts
{
    /// <summary>
    /// Сервис пользователя
    /// </summary>
    public interface IUserService
    {
        //TODO: авторизацию и регистрацию сделать потом, т.к. будет отдельный таск

        /// <summary>
        /// Возвращает объект пользователя по Id
        /// </summary>
        /// <param name="userId">Guid пользователя</param>
        ServiceResponse<UserDto> GetById(Guid userId);

        /// <summary>
        /// Возвращает объект пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        ServiceResponse<UserDto> GetByLogin(string login);

        /// <summary>
        /// Возвращает участников беседы
        /// </summary>
        /// <param name="groupId">Guid группы</param>
        /// <param name="userId">Guid пользователя, который запрашивает информацию</param>
        ServiceResponse<IList<UserDto>> GetUsersInGroup(Guid groupId, Guid userId);

        /// <summary>
        /// Возвращает список существующих личных бесед 
        /// </summary>
        /// <param name="userId">Guid пользователя</param>
        ServiceResponse<IList<UserDto>> GetDialogsForUser(Guid userId);

        /// <summary>
        /// Связывает пользователя с группой
        /// </summary>
        /// <param name="bindeeId">Пользователь, которого добавляют</param>
        /// <param name="adderId">Пользователь, который добавляет в группу</param>
        /// <param name="chatId">Групповая беседа</param>
        ServiceResponse BindToChat(Guid bindeeId, Guid adderId, Guid chatId);

        /// <summary>
        /// Создает личную беседу с пользователем
        /// </summary>
        /// <param name="initiatorId">Инициатор беседы</param>
        /// <param name="interlocutorId">Собеседник</param>
        ServiceResponse StartConvoWithUser(Guid initiatorId, Guid interlocutorId);
    }
}