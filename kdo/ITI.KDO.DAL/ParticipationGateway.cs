using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ITI.KDO.DAL
{
    public class ParticipationGateway
    {
        readonly string _connectionString;

        public ParticipationGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Create a Participation
        /// </summary>
        /// <param name="quantityId"></param>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <param name="amountUserPrice"></param>
        public void Create(int quantityId, int userId, int eventId, int amountUserPrice)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sParticipationCreate",
                    new
                    {
                        QuantityId = quantityId,
                        UserId = userId,
                        EventId = eventId,
                        AmountUserPrice = amountUserPrice
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Find a participation bye the UserId and EventId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Participation FindByIds(int userId, int quantityId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Participation>(
                    @"select p.QuantityId,
                             p.UserId,
                             p.EventId,
                             p.AmountUserPrice
                    from dbo.vParticipation p
                    where p.UserId = @UserId and p.QuantityId = @QuantityId",
                    new { UserId = userId, QuantityId = quantityId })
                    .FirstOrDefault();
            }
        }


        public IEnumerable<Participation> FindParticipationById(int quantityId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Participation>(
                    @"select p.QuantityId,
                             p.UserId,
                             p.EventId,
                             p.AmountUserPrice
                    from dbo.vParticipation p
                    where p.QuantityId = @QuantityId",
                    new { QuantityId = quantityId });
            }
        }


        /// <summary>
        /// Update ONLY the AmountUserPrice where UserId = @UserId and EventId = @EventId
        /// </summary>
        /// <param name="quantityId"></param>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <param name="amoutUserPrice"></param>
        public void Update(int quantityId, int userId, int eventId, int amountUserPrice)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sParticipationUpdate",
                    new { QuantityId = quantityId, UserId = userId, EventId = eventId, AmountUserPrice = amountUserPrice },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete Participation with the userId and eventId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        public void Delete(int userId, int quantityId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sParticipationDelete",
                    new { UserId = userId, QuantityId = quantityId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
