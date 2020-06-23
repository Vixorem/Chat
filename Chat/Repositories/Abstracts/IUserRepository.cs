using System;
using System.Collections.Generic;
using Chat.Models;

namespace Chat.Repositories.Abstracts
{
    /// <summary>
    /// Интерфейс предназначен для работы с юзерами
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получение логина и пароля
        /// </summary>
        /// <param name="userId">Guid пользователя</param>
        /// <returns>Кортеж (хэш пароля, соль)</returns>
        (string PasswordHash, string SaltHash) GetPasswordInfo(Guid userId);

        /// <summary>
        /// Получает юзера по Id
        /// </summary>
        /// <param name="userId">Guid юзера</param>
        User GetById(Guid userId);

        /// <summary>
        /// Получает список юзеров в группе
        /// </summary>
        /// <param name="groupId">Guid группы</param>
        IList<User> GetUsersInGroup(Guid groupId);

        /// <summary>
        /// Получает юзера по логину
        /// </summary>
        /// <param name="login">Логин юзера</param>
        User GetByLogin(string login);

        /// <summary>
        /// Получает список диалогов
        /// </summary>
        /// <param name="userId">Guid юзера</param>
        IList<User> GetDialogsForUser(Guid userId);

        /// <summary>
        /// Добавляет юзера
        /// </summary>
        /// <param name="userId">Новосгенерированный guid для юзера</param>
        /// <param name="login">Логин</param>
        /// <param name="hashPswd">Сгенерированный хэш пароля</param>
        /// <param name="hashSalt">Соль</param>
        void Add(Guid userId, string login, string hashPswd, string hashSalt);

        /// <summary>
        /// Связывает юзера с чатом
        /// </summary>
        /// <param name="bindeeId">Guid юзера, которого связывают</param>
        /// <param name="chatId">Guid группы или собеседника, с которыми связывают</param>
        void BindToChat(Guid bindeeId, Guid chatId);

        /// <summary>
        /// Проверяет, доступен ли чат юзеру
        /// </summary>
        /// <param name="chatId">Guid чата</param>
        /// <param name="userId">Guid зера</param>
        bool DoesUserBelongToChat(Guid chatId, Guid userId);
    }
}