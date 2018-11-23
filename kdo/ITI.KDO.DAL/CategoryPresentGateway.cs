using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ITI.KDO.DAL
{
    public class CategoryPresentGateway
    {
        readonly string _connectionString;

        public CategoryPresentGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get All Category Present
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryPresent> GetAll()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<CategoryPresent>(
                    @"select c.CategoryPresentId,
                             c.CategoryName,
                             c.Link
                      from dbo.vCategoryPresent c;");
            }
        }
    }
}
