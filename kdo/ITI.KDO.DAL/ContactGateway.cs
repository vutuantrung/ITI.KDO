using System.Text;
using System.Linq;
using System.Data.SqlClient;
﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace ITI.KDO.DAL
{
    public class ContactGateway
    {
        readonly string _connectionString;

        public ContactGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Add a Contact
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <param name="invitation"></param>
        public void CreateContact(int userId, int friendId, bool invitation)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sContactCreate", new
                    {
                        UserId = userId,
                        FriendId = friendId,
                        Invitation = invitation
                    }, commandType: CommandType.StoredProcedure);
            }
        }


        /// <summary>
        /// Get all friend contacts by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<ContactData> FindAllByUserId(int userId)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<ContactData>(
                    @"select c.ContactId,
                             c.UserId,
                             c.FriendId,
                             c.Invitation
                    from dbo.vContact c
                    where c.Invitation = 1 and
                         (c.UserId = @UserId or c.FriendId = @UserId);", 
                    new { UserId = userId });
            }
        }


        /// <summary>
        /// Get all notifications by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<ContactData> GetContactNotification(int userId)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<ContactData>(
                    @"select c.ContactId,
                             c.UserId,
                             c.FriendId
                    from dbo.vContact c
                    where c.Invitation = 0 and
                          c.FriendId = @UserId;",
                    new { UserId = userId });
            }
        }


        /// <summary>
        /// Find contact by the UserId, FriendId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        public ContactData FindByIds(int userId, int friendId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
                return con.Query<ContactData>(
                    @"select c.ContactId,
                             c.UserId,
                             c.FriendId,
                             c.Invitation
                      from dbo.vContact c
                      where (c.UserId = @UserId and c.FriendId = @FriendId) or 
                            (c.UserId = @FriendId and c.FriendId = @UserId)",
                    new { UserId = userId, friendId = friendId })
                    .FirstOrDefault();
        }

        /// <summary>
        /// Find Contact by ContactId
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public ContactData FindByContactId(int contactId)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<ContactData>(
                    @"select c.UserId,
                             c.FriendId,
                             c.Invitation
                      from dbo.vContact c
                      where c.ContactId = @ContactId",
                    new { ContactId = contactId })
                    .FirstOrDefault();
            }
        }


        /// <summary>
        /// Set friend relation between 2 users
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        public void SetContactInvitation(int userId, int friendId)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    @"UPDATE dbo.tContact 
                      SET Invitation = 1 
                      WHERE (UserId = @UserId and FriendId = @FriendId) or 
                            (FriendId = @UserId and UserId = @FriendId)",
                    new { UserId = userId, FriendId = friendId });
            }
        }
        
        /// <summary>
        /// Delete a Contactd
        /// </summary>
        /// <param name="contactId"></param>
        public void Delete(int contactId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sContactDelete",
                    new { ContactId = contactId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}