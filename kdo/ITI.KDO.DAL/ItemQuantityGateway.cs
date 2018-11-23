using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ITI.KDO.DAL
{
    public class ItemQuantityGateway
    {
        readonly string _connectionString;

        public ItemQuantityGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Can create a Quantity and return this QuantityId
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="recipientId"></param>
        /// <param name="nominatorId"></param>
        /// <param name="eventId"></param>
        /// <param name="presentId"></param>
        /// <returns></returns>
        public int Create(int quantity, int recipientId, int nominatorId, int eventId, int presentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Quantity", quantity, DbType.Int32);
                dynamicParameters.Add("@RecipientId", recipientId, DbType.Int32);
                dynamicParameters.Add("@NominatorId", nominatorId, DbType.Int32);
                dynamicParameters.Add("@EventId", eventId, DbType.Int32);
                dynamicParameters.Add("@PresentId", presentId, DbType.Int32);
                dynamicParameters.Add("@Id", DbType.Int32, direction: ParameterDirection.ReturnValue);

                con.Execute(
                    "dbo.sQuantityCreate",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure
                );

                return dynamicParameters.Get<int>("@Id");
            }
        }

        /// <summary>
        /// Find a Quantity with this QuantityId
        /// </summary>
        /// <param name="quantityId"></param>
        /// <returns></returns>
        public ItemQuantity FindById(int quantityId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<ItemQuantity>(
                    @"select q.QuantityId,
                             q.Quantity,
                             q.RecipientId,
                             q.NominatorId,
                             q.EventId,
                             q.PresentId
                      from dbo.vQuantity q
                      where q.QuantityId = @QuantityId",
                    new { QuantityId = quantityId })
                    .FirstOrDefault();
            }
        }

        public IEnumerable<ItemQuantity> FindByEventId(int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<ItemQuantity>(
                    @"select q.QuantityId,
                             q.Quantity,
                             q.RecipientId,
                             q.NominatorId,
                             q.EventId,
                             q.PresentId
                      from dbo.vQuantity q
                      where q.EventId = @EventId",
                    new { EventId = eventId });
            }
        }

        /// <summary>
        /// Update Quantity where QuantityId = @QuantityId ( that not update the NominatorId (NominatorId is like UserId) )
        /// </summary>
        /// <param name="quantityId"></param>
        /// <param name="quantity"></param>
        /// <param name="recipientId"></param>
        /// <param name="nominatorId"></param>
        /// <param name="eventId"></param>
        /// <param name="presentId"></param>
        public void Update(int quantityId, int quantity, int recipientId, int nominatorId, int eventId, int presentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sQuantityUpdate",
                    new { QuantityId = quantityId, Quantity = quantity, RecipientId = recipientId, NominatorId = nominatorId, EventId = eventId, PresentId = presentId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete a Quantity by the QuantityId
        /// </summary>
        /// <param name="quantityId"></param>
        public void Delete(int quantityId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sQuantityDelete",
                    new { QuantityId = quantityId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<ItemPresentQuantity> GetUserPresentEvent(int userId, int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<ItemPresentQuantity>(
                    @"select PresentId,
                             PresentName,
                             Price,
                             LinkPresent,
                             CategoryPresentId,
                             QuantityId,
                             Quantity,
                             RecipientId,
                             NominatorId,
                             ParticipantType,
                             Invitation,
                             EventId
                      from dbo.vUserPresentEvent
                      where RecipientId = @RecipientId and EventId = @EventId;",
                    new { RecipientId = userId, EventId = eventId });
            }
        }
    }
}
