using System;
using ITI.KDO.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class ParticipantServices
    {
        readonly ParticipantGateway _participantGateway;
        readonly UserGateway _userGateway;
        readonly EventGateway _eventGateway;

        public ParticipantServices(ParticipantGateway participantGateway, UserGateway userGateway, EventGateway eventGateway)
        {
            _participantGateway = participantGateway;
            _userGateway = userGateway;
            _eventGateway = eventGateway;
        }

        public Result SetEventInvitation(int userId, int eventId)
        {
            if (_userGateway.FindById(userId) == null) return Result.Failure(Status.NotFound, "User not found.");
            if (_eventGateway.FindById(eventId) == null) return Result.Failure(Status.NotFound, "Event not found.");
            if (_participantGateway.FindByIds(userId, eventId) == null) return Result.Failure(Status.BadRequest, "Event invitation not found.");
            if (_participantGateway.FindByIds(userId, eventId) != null && _participantGateway.FindByIds(userId, eventId).Invitation == true)
                return Result.Failure(Status.BadRequest, "Participant existed.");

            _participantGateway.SetEventInvitaion(userId, eventId);
            return Result.Success(Status.Ok);
        }

        public Result<Participant> GetParticipant(int userId, int eventId)
        {
            if (_participantGateway.FindByIds(userId, eventId) == null)
                return Result.Failure<Participant>(Status.NotFound, "Participant doesn't exist.");
            Participant participant = _participantGateway.FindByIds(userId, eventId);
            return Result.Success(Status.Ok, participant);
        }

        public Result<IEnumerable<Participant>> GetInviteNotification(int userId)
        {
            return Result.Success(Status.Ok, _participantGateway.GetInviteNotification(userId));
        }

        public Result<IEnumerable<Participant>>FindById(int userId,int eventId)
        {
            return Result.Success(Status.Ok, _participantGateway.FindParticipantsForEvent(eventId));
        }

        public Result<IEnumerable<Participant>> FindParticipantsInvitedById(int userId, int eventId)
        {
            return Result.Success(Status.Ok, _participantGateway.FindParticipantsInvitedForEvent(eventId));
        }       

        public Result<Participant> CreateParticipant(int userId, int eventId, bool participantType, bool invitation)
        {
            if (_userGateway.FindById(userId) == null) return Result.Failure<Participant>(Status.NotFound, "User not found");
            if (_eventGateway.FindById(eventId) == null) return Result.Failure<Participant>(Status.NotFound, "Event not found.");
            if (_participantGateway.FindByIds(userId, eventId) != null && _participantGateway.FindByIds(userId, eventId).Invitation == true) return Result.Failure<Participant>(Status.BadRequest, "Event existed.");

            _participantGateway.Create(userId, eventId, false, false);
            Participant participant = _participantGateway.FindByIds(userId, eventId);
            return Result.Success(Status.Ok, participant);
        }

        public Result<Participant> UpdateParticipant(int userId, int eventId, bool participantType, bool invitation)
        {
            Participant participant;
            if ((participant = _participantGateway.FindByIds(userId, eventId)) == null)
            {
                return Result.Failure<Participant>(Status.NotFound, "Participant not found.");
            }

            _participantGateway.Update(userId, eventId, participantType, invitation);
            participant = _participantGateway.FindByIds(userId, eventId);
            return Result.Success(Status.Ok, participant);
        }

        public Result<int> Delete(int userId, int eventId)
        {
            if (_participantGateway.FindByIds(userId, eventId) == null) return Result.Failure<int>(Status.NotFound, "Participant not found.");
            _participantGateway.Delete(userId, eventId);
            return Result.Success(Status.Ok, userId);
        }
    }
}
