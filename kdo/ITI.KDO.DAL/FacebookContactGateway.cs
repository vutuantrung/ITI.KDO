using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Linq;

namespace ITI.KDO.DAL
{
    public class FacebookContactGateway
    {
        readonly string _connectionString;

        public FacebookContactGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<FacebookContact> GetAll(int userId)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<FacebookContact>(
                            @"select f.ContactId,
                                     f.UserId,
                                     f.FacebookId,
                                     f.Email,
                                     f.FirstName,
                                     f.LastName,
                                     f.BirthDate,
                                     f.Phone
                            from dbo.vFacebookContact f
                            where f.UserId = @UserId;", 
                            new { UserId = userId});
            }
        }

        public void CreateFacebookContact(int userId, int facebookId, string email, string firstName, string lastName, DateTime birthDate, string phone)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sFacebookContactCreate", new
                    {
                        UserId = userId,
                        FacebookId = facebookId,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        BirthDate = birthDate,
                        Phone = phone
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public FacebookContact FindByUserId(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<FacebookContact>(
                    @"select f.ContactId,
                             f.UserId,
                             f.FacebookId,
                             f.Email,
                             f.FirstName,
                             f.LastName,
                             f.BirthDate,
                             f.Phone
                    from dbo.vFacebookContact f
                    where f.FacebookId = @FacebookId;",
                    new { UserId = userId })
                    .FirstOrDefault();
            }
        }

        public FacebookContact FindByFacebookId(int facebookId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<FacebookContact>(
                    @"select f.ContactId,
                             f.UserId,
                             f.FacebookId,
                             f.Email,
                             f.FirstName,
                             f.LastName,
                             f.BirthDate,
                             f.Phone
                    from dbo.vFacebookContact f
                    where f.FacebookId = @FacebookId;", 
                    new { FacebookId = facebookId})
                    .FirstOrDefault();
            }
        }

        public void Delete(int facebookId)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute("dbo.sFacebookContactDelete", 
                    new { FacebookId = facebookId }, 
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
