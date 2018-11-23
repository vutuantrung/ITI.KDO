using System;
using ITI.KDO.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class ParticipationServices
    {
        readonly ParticipationGateway _participationGateway;

        public ParticipationServices(ParticipationGateway participationGateway)
        {
            _participationGateway = participationGateway;
        }

        public Result Delete(int quantityId, int userId)
        {
            if (_participationGateway.FindByIds(userId, quantityId) == null) return Result.Failure(Status.NotFound, "Participation not found.");
            _participationGateway.Delete(userId, quantityId);
            return Result.Success(Status.Ok);
        }

        public Result<Participation> UpdateParticipation(int quantityId, int userId, int eventId, int amountUserPrice)
        {
            Participation participation;
            if ((participation = _participationGateway.FindByIds(userId, quantityId)) == null)
            {
                return Result.Failure<Participation>(Status.NotFound, "Participation not found.");
            }

            _participationGateway.Update(quantityId, userId, eventId, amountUserPrice);
            participation = _participationGateway.FindByIds(userId, quantityId);
            return Result.Success(Status.Ok, participation);
        }

        public Result<Participation> CreateParticipation(int quantityId, int userId, int eventId, int amountUserPrice)
        {
            _participationGateway.Create(quantityId, userId, eventId, amountUserPrice);
            Participation participation = _participationGateway.FindByIds(userId, quantityId);
            return Result.Success(Status.Ok, participation);
        }

        public Result<Participation> GetParticipation(int quantityId, int userId)
        {
            Participation participation;
            if ((participation = _participationGateway.FindByIds(userId, quantityId)) == null)
                return Result.Failure<Participation>(Status.NotFound, "Participation not found.");
            return Result.Success(Status.Ok, participation);
        }

        public Result<IEnumerable<Participation>> GetParticipationByQuantity(int quantityId)
        {
            IEnumerable<Participation> participation;
            if ((participation = _participationGateway.FindParticipationById(quantityId)) == null)
                return Result.Failure<IEnumerable<Participation>>(Status.NotFound, "Participation not found.");
            return Result.Success(Status.Ok, participation);
        }

        public bool ParticipationExist(int quantityId, int userId)
        {
            return ((_participationGateway.FindByIds(userId, quantityId)) != null);
        }
    }
}
