using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ITI.KDO.DAL
{
    public class EventGateway
    {
        readonly string _connectionString;

        public EventGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        ///<summary>
        ///Find Event of User with UserId
        ///</summary>
        ///<param name="userId"></param>
        ///<returns></return>
        public Event FindById(int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Event>(
                    @"select e.UserId,
                             e.EventId,
                             e.EventName,
                             e.Descriptions,
                             e.Picture,
                             e.Dates
                          from dbo.vEvent e
                          where e.EventId = @EventId",
                          new { EventId = eventId })
                          .FirstOrDefault(); 
            }
        }

        /// <summary>
        /// Get event by userId and eventId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event FindByIds(int userId, int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Event>(
                    @"select e.UserId,
                             e.EventId,
                             e.EventName,
                             e.Descriptions,
                             e.Picture,
                             e.Dates
                          from dbo.vEvent e
                          where e.EventId = @EventId and e.UserId = @UserId",
                          new { EventId = eventId, UserId = userId })
                          .FirstOrDefault();
            }
        }

        /// <summary>
        /// Add Event to User and return the EventId
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="description"></param>
        /// <param name="dates"></param>
        /// <param name="userId"></param>

        public int Create(string eventName, string descriptions, DateTime dates, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EventName", eventName, DbType.String);
                dynamicParameters.Add("@Descriptions", descriptions, DbType.String);
                dynamicParameters.Add("@Dates", dates, DbType.DateTime);
                dynamicParameters.Add("@UserId", userId, DbType.Int32);
                dynamicParameters.Add("@Id", DbType.Int32, direction: ParameterDirection.ReturnValue);

                con.Execute(
                    "dbo.sEventCreate",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure
                );
                return dynamicParameters.Get<int>("@Id");
            }
        }

        /// <summary>
        /// Update Event Of User
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="descriptions"></param>
        /// <param name="dates"></param>
        /// <param name="userId"></param>
        public void Update(int eventId, string eventName, string descriptions, DateTime dates)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
               "dbo.sEventUpdate",
               new
               {
                   EventId = eventId,
                   EventName = eventName,
                   Descriptions = descriptions,
                   Dates = dates
               },
               commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete Event Of User
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        public void Delete(int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sEventDelete",
                    new { EventId = eventId },
                    commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Get ALL Event by the UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Event> GetAllByUserId(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Event>(
                    @"select e.EventId,
                             e.UserId,
                             e.EventName,
                             e.Descriptions,
                             e.Picture,
                             e.Dates
                      from dbo.vEvent e
                      where e.UserId = @UserId;",
                    new { UserId = userId });
            }
        }

        /// <summary>
        /// Update Event
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="descriptions"></param>
        /// <param name="dates"></param>
        /// <param name="userId"></param>
        public void Update(string eventName, string descriptions, DateTime dates, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sEventUpdate",
                    new
                    {
                        EventName = eventName,
                        Descriptions = descriptions,
                        Dates = dates,
                        UserId = userId
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
        
        /// <summary>
        /// Find Event by this name
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public Event FindByName(string eventName)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Event>(
                          @"select e.eventId,
                                   e.picture,
                                   e.UserId,
                                   e.EventName,
                                   e.Descriptions,
                                   e.Dates
                            from dbo.vEvent e
                            where e.EventName = @EventName;",
                        new { EventName = eventName })
                    .FirstOrDefault();
            }
        }

        public IEnumerable<EventSuggest> GetAllEventSuggest(int userId, int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<EventSuggest>(
                    @"select e.EventId,
                             e.UserId,
                             e.DateSuggest,
                             e.Descriptions
                      from dbo.vEventSuggest e
                      where e.UserId = @UserId and e.EventId = @EventId;",
                    new { UserId = userId, EventId = eventId });
            }
        }

        public IEnumerable<EventSuggest> GetAllEventSuggestByUserId(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<EventSuggest>(
                    @"select e.EventId,
                             e.UserId,
                             e.DateSuggest,
                             e.Descriptions
                      from dbo.vEventSuggest e
                      where e.UserId = @UserId;",
                    new { UserId = userId });
            }
        }

        public EventSuggest FindEventSuggestByIds(int userId, int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<EventSuggest>(
                    @"select e.EventId,
                             e.UserId,
                             e.DateSuggest,
                             e.Descriptions
                      from dbo.vEventSuggest e
                      where e.UserId = @UserId and e.EventId = @EventId;",
                    new { UserId = userId, EventId = eventId }).FirstOrDefault();
            }
        }

        public void CreateEventSuggest(int eventId, int userId, DateTime dateSuggest, string descriptions)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sEventSuggestCreate", new
                    {
                        UserId = userId,
                        EventId = eventId,
                        DateSuggest = dateSuggest,
                        Descriptions = descriptions
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        /// Update Picture of Event with the EventId
        /// </summary>
        /// <param name="presentId"></param>
        /// <param name="coverImageBytes"></param>
        public void EventSetPicture(int eventId, byte[] coverImageBytes)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sEventUpdatePhoto",
                    new { EventId = eventId, Picture = coverImageBytes },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
