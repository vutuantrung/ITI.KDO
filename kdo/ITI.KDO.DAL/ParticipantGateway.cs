using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ITI.KDO.DAL
{
    public class ParticipantGateway
    {
        readonly string _connectionString;

        public ParticipantGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Create Participant
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <param name="participantType"></param>
        public void Create(int userId, int eventId, bool participantType, bool invitation)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sParticipantCreate",
                    new
                    {
                        UserId = userId,
                        EventId = eventId,
                        ParticipantType = participantType,
                        Invitation = invitation
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(int userId, int eventId, bool participantType, bool invitation)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sParticipantUpdate",
                    new
                    {
                        UserId = userId,
                        EventId = eventId,
                        ParticipantType = participantType,
                        Invitation = invitation
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// Find a all participant with the userId for 1 Event
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IEnumerable<Participant> GetInviteNotification(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Participant>(
                    @"select p.UserId,
                             p.EventId,
                             p.ParticipantType,
                             p.Invitation
                  from dbo.vParticipant p
                    where p.UserId = @UserId and
                          p.Invitation = 0",
                    new { UserId = userId });
            }
        }


        /// Find a all participant with the userId for 1 Event
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IEnumerable<Participant> FindParticipantsForEvent(int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Participant>(
                    @"select p.UserId,
                             p.EventId,
                             p.ParticipantType,
                             p.Invitation
                  from dbo.vParticipant p
                    where p.EventId = @EventId and p.Invitation = 1",
                    new { EventId = eventId });
            }
        }

        public IEnumerable<Participant> FindParticipantsInvitedForEvent(int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Participant>(
                    @"select p.UserId,
                             p.EventId,
                             p.ParticipantType,
                             p.Invitation
                  from dbo.vParticipant p
                    where p.EventId = @EventId",
                    new { EventId = eventId });
            }
        }
        /// <summary>
        /// Get all participants of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Participant> FindParticipantsOfUser(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Participant>(
                    @"select p.UserId,
                             p.EventId,
                             p.ParticipantType,
                             p.Invitation
                  from dbo.vParticipant p
                    where p.UserId = @UserId and p.Invitation = 1",
                    new { UserId = userId });
            }
        }

        /// <summary>
        /// Find participant specific for an event
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Participant FindByIds(int userId, int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Participant>(
                    @"select p.UserId,
                             p.EventId,
                             p.ParticipantType,
                             p.Invitation
                  from dbo.vParticipant p
                    where p.EventId = @EventId and p.UserId = @UserId",
                    new { EventId = eventId, UserId = userId})
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Set true value for participant's Invitation
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        public void SetEventInvitaion(int userId, int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    @"UPDATE dbo.tParticipant 
                      SET Invitation = 1 
                      WHERE UserId = @UserId and EventId = @EventId",
                    new { UserId = userId, EventId = eventId });
            }
        }

        /// <summary>
        /// Delete a participant with userId and eventId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        public void Delete(int userId, int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sParticipantDelete",
                    new { UserId = userId, EventId = eventId },
                    commandType: CommandType.StoredProcedure);
            }
        }

    }
}
