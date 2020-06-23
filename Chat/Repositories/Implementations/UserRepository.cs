using System;
using System.Collections.Generic;
using System.Data;
using Chat.DbUtils;
using Chat.Models;
using Chat.Repositories.Abstracts;

namespace Chat.Repositories.Implementations
{
    ///<inheritdoc cref="IUserRepository"/>
    public class UserRepository : IUserRepository
    {
        private readonly DbRequest _db;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="db"></param>
        public UserRepository(DbRequest db)
        {
            _db = db;
        }

        ///<inheritdoc cref="IUserRepository"/>
        public (string PasswordHash, string SaltHash) GetPasswordInfo(Guid userId)
        {
            return _db.GetItemFromEntry(
                "ChatGetUserById",
                dataReader => (dataReader.GetString("PswdHash"), dataReader.GetString("HashSalt")),
                new DbParam("@Id", userId));
        }

        ///<inheritdoc cref="IUserRepository"/>
        public IList<User> GetUsersInGroup(Guid groupId) =>
            _db.GetItemListFromEntries("ChatGetUsersInGroup", FromReader, new DbParam("@GroupId", groupId));

        ///<inheritdoc cref="IUserRepository"/>
        public User GetById(Guid userId) =>
            _db.GetItemFromEntry("ChatGetUserById", FromReader, new DbParam("@Id", userId));

        ///<inheritdoc cref="IUserRepository"/>
        public User GetByLogin(string login) =>
            _db.GetItemFromEntry("ChatGetUserByLogin", FromReader, new DbParam("@Login", login));

        ///<inheritdoc cref="IUserRepository"/>
        public IList<User> GetDialogsForUser(Guid userId) =>
            _db.GetItemListFromEntries("ChatGetDialogsForUserId", FromReader, new DbParam("@UserId", userId));

        ///<inheritdoc cref="IUserRepository"/>
        public void Add(Guid userId, string login, string hashPswd, string hashSalt)
        {
            _db.ExecuteNonQuery(
                "ChatCreateUser",
                new DbParam("@Id", userId),
                new DbParam("@Login",
                    login),
                new DbParam("@HashedPswd",
                    hashPswd),
                new DbParam("@Salt",
                    hashSalt));
        }

        ///<inheritdoc cref="IUserRepository"/>
        public void BindToChat(Guid bindeeId, Guid chatId)
        {
            _db.ExecuteNonQuery(
                "ChatBindToChat",
                new DbParam("@Addee", bindeeId),
                new DbParam("@ChatId", chatId));
        }

        ///<inheritdoc cref="IUserRepository"/>
        public bool DoesUserBelongToChat(Guid chatId, Guid userId)
        {
            return (_db.GetItemListFromEntries(
                "ChatIsUserInChat",
                // Это лямбда-заглушка, т.к. функция не возвращает все поля, которые используются в FromReader,
                // нам важно только кол-во
                (r) => default(User),
                new DbParam("@UserId", userId),
                new DbParam("@ChatId", chatId)).Count > 0);
        }

        /// <summary>
        /// Создает объект по записи
        /// </summary>
        /// <param name="dataReader">reader результирующей строки</param>
        private User FromReader(IDataReader dataReader)
        {
            return new User
            {
                Id = dataReader.GetGuid("Id"),
                Login = dataReader.GetString("Login")
            };
        }
    }
}