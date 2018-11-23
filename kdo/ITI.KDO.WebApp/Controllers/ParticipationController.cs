using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Models.ParticipationViewModels;
using ITI.KDO.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ParticipationController : Controller
    {
        readonly ParticipationServices _participationService;

        public ParticipationController(ParticipationServices participationService)
        {
            _participationService = participationService;
        }

        [HttpGet("{quantityId}/{userId}/getParticipation")]
        public IActionResult GetParticipation(int quantityId, int userId)
        {
            Result<Participation> result = _participationService.GetParticipation(quantityId, userId);
            return this.CreateResult<Participation, ParticipationViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToParticipationViewModel();
            });
        }

        [HttpGet("{quantityId}")]
        public IActionResult GetParticipationByQuantity(int quantityId)
        {
            Result<IEnumerable<Participation>> result = _participationService.GetParticipationByQuantity(quantityId);
            return this.CreateResult<IEnumerable<Participation>, IEnumerable<ParticipationViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToParticipationViewModel());
            });
        }

        [HttpGet("{quantityId}/{userId}/existingParticipation")]
        public bool ExistingParticipation(int quantityId, int userId)
        {
            return _participationService.ParticipationExist(quantityId, userId);
        }

        [HttpPost]
        public IActionResult CreateParticipation([FromBody] ParticipationViewModel model)
        {
            Result<Participation> result = _participationService.CreateParticipation(model.QuantityId, model.UserId, model.EventId, model.AmountUserPrice);
            return this.CreateResult<Participation, ParticipationViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToParticipationViewModel();
            });
        }

        [HttpPut("{quantityId}/{userId}")]
        public IActionResult UpdateParticipation(int quantityId, int userId, [FromBody] ParticipationViewModel model)
        {
            Result<Participation> result = _participationService.UpdateParticipation(model.QuantityId, model.UserId, model.EventId, model.AmountUserPrice);
            return this.CreateResult<Participation, ParticipationViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToParticipationViewModel();
            });
        }

        [HttpDelete("{quantityId}/{userId}")]
        public IActionResult DeleteParticipation(int quantityId, int userId)
        {
            Result result = _participationService.Delete(quantityId, userId);
            return this.CreateResult(result);
        }
    }
}