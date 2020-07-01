using System;
using System.Collections.Generic;
using System.Data;
using Chat.DbUtils;
using Chat.Models;
using Chat.Repositories.Abstracts;

namespace Chat.Repositories.Implementations
{
    ///<inheritdoc cref="IGroupRepository"/>
    public class GroupRepository : IGroupRepository
    {
        private readonly DbRequest _db;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="db"></param>
        public GroupRepository(DbRequest db)
        {
            _db = db;
        }

        ///<inheritdoc cref="IGroupRepository"/>
        public Group GetById(Guid groupId) =>
            _db.GetItemFromEntry("ChatGetGroupById", FromReader, new DbParam("@Id", groupId));

        ///<inheritdoc cref="IGroupRepository"/>
        public IList<Group> GetGroupsForUser(Guid userId) =>
            _db.GetItemListFromEntries("ChatGetGroupsForUserId", FromReader, new DbParam("@UserId", userId));

        ///<inheritdoc cref="IGroupRepository"/>
        public Group Add(Guid groupId, string name)
        {
            return _db.GetItemFromEntry(
                "ChatCreateGroup",
                FromReader,
                new DbParam("@Id", groupId),
                new DbParam("@Name", name));
        }

        ///<inheritdoc cref="IGroupRepository"/>
        public void KickUser(Guid groupId, Guid removeeId)
        {
            _db.ExecuteNonQuery("ChatRemoveFromGroup",
                new DbParam("@UserId", removeeId),
                new DbParam("@GroupId", groupId));
        }

        private Group FromReader(IDataReader dataReader)
        {
            return new Group
            {
                Id = dataReader.GetGuid("Id"),
                Name = dataReader.GetString("Name")
            };
        }
    }
}