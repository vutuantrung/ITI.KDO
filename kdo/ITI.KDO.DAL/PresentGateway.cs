using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace ITI.KDO.DAL
{
    public class PresentGateway
    {
        readonly string _connectionString;

        public PresentGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Add Present to User and return the PresentId
        /// </summary>
        /// <param name="presentName"></param>
        /// <param name="price"></param>
        /// <param name="linkPresent"></param>
        /// <param name="categoryPresentId"></param>
        /// <param name="userId"></param>
        public int AddToUser(string presentName, float price, string linkPresent, byte[] picture, int categoryPresentId, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PresentName", presentName, DbType.String);
                dynamicParameters.Add("@Price", price, DbType.Decimal);
                dynamicParameters.Add("@LinkPresent", linkPresent, DbType.String);
                dynamicParameters.Add("@Picture", picture, DbType.Binary);
                dynamicParameters.Add("@CategoryPresentId", categoryPresentId, DbType.Int32);
                dynamicParameters.Add("@UserId", userId, DbType.Int32);
                dynamicParameters.Add("@PresentId", DbType.Int32, direction: ParameterDirection.ReturnValue);

                con.Execute(
                    "dbo.sPresentCreate",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure
                );
                return dynamicParameters.Get<int>("@PresentId");
            }
        }

        /// <summary>
        /// Update Present Of User
        /// </summary>
        /// <param name="presentName"></param>
        /// <param name="price"></param>
        /// <param name="linkPresent"></param>
        /// <param name="categoryPresentId"></param>
        /// <param name="userId"></param>
        public void Update(int presentId, string presentName, float price, string linkPresent, byte[] picture, int categoryPresentId, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
               "dbo.sPresentUpdate",
               new {
                   PresentId = presentId,
                   PresentName = presentName,
                   Price = price,
                   LinkPresent = linkPresent,
                   Picture = picture,
                   CategoryPresentId = categoryPresentId,
                   UserId = userId },
               commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete Present Of User
        /// </summary>
        /// <param name="presentId"></param>
        /// <param name="userId"></param>
        public void Delete(int presentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sPresentDelete",
                    new { PresentId = presentId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Get all by the User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Present> GetAllByUserId(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Present>(
                    @"select p.UserId,
                             p.CategoryPresentId,
                             p.CategoryName,
                             p.Link,
                             p.PresentId,
                             p.PresentName,
                             p.Price,
                             p.LinkPresent,
                             p.Picture
                      from dbo.vPresent p
                      where p.UserId = @UserId;",
                    new { UserId = userId });
            }
        }
        /// <summary>
        /// Create present
        /// </summary>
        /// <param name="presentName"></param>
        /// <param name="price"></param>
        /// <param name="linkPresent"></param>
        /// <param name="categoryPresentId"></param>
        /// <param name="userId"></param>
        public void Create(string presentName, float price, string linkPresent, byte[] picture, int categoryPresentId, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sPresentCreate",
                    new
                    {
                        PresentName = presentName,
                        Price = price,
                        LinkPresent = linkPresent,
                        Picture = picture,
                        CategoryPresentId = categoryPresentId,
                        UserId = userId
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Find a present by the PresentId
        /// </summary>
        /// <param name="presentId"></param>
        /// <returns></returns>
        public Present FindByPresentId(int presentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Present>(
                        @"select p.UserId,
                                 p.CategoryPresentId,
                                 p.CategoryName,
                                 p.Link,
                                 p.PresentId,
                                 p.PresentName,
                                 p.Price,
                                 p.LinkPresent,
                                 p.Picture
                          from dbo.vPresent p
                          where p.PresentId = @PresentId;",
                        new { PresentId = presentId })
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Find a present by the PresentName
        /// </summary>
        /// <param name="presentName"></param>
        /// <returns></returns>
        public Present FindByName(string presentName)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Present>(
                        @"select p.UserId,
                                 p.CategoryPresentId,
                                 p.CategoryName,
                                 p.Link,
                                 p.PresentId,
                                 p.PresentName,
                                 p.Price,
                                 p.LinkPresent,
                                 p.Picture
                          from dbo.vPresent p
                          where p.PresentName = @PresentName;",
                        new { PresentName = presentName })
                    .FirstOrDefault();
            }
        }
        /// <summary>
        /// Update Picture of Present with the PresentId
        /// </summary>
        /// <param name="presentId"></param>
        /// <param name="coverImageBytes"></param>
        public void PresentSetPicture(int presentId, byte[] coverImageBytes)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sPresentUpdatePhoto",
                    new { PresentId = presentId, Picture = coverImageBytes },
                    commandType: CommandType.StoredProcedure);
            }
        }

    }
}
