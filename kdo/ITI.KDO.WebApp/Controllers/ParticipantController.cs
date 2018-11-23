using ITI.KDO.WebApp.Authentification;
using Microsoft.AspNetCore.Authorization;
using ITI.KDO.WebApp.Models.ParticipantViewModels;
using Microsoft.AspNetCore.Mvc;
using ITI.KDO.DAL;
using System;
using ITI.KDO.WebApp.Services;
using ITI.KDO.WebApp.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]

    public class ParticipantController: Controller
    {
        readonly ParticipantServices _participantService;

        public ParticipantController(ParticipantServices participantService)
        {
            _participantService = participantService;
        }

        [HttpGet("{userId}/{eventId}/getParticipant")]
        public IActionResult GetParticipant(int userId, int eventId)
        {
            Result<Participant> result = _participantService.GetParticipant(userId, eventId);
            return this.CreateResult<Participant, ParticipantViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToParticipantViewModel();
            });
        }

        [HttpGet("{userId}/{eventId}/getByUserId")]
        public IActionResult GetParticipantList(int userId, int eventId)
        {
            Result<IEnumerable<Participant>> result = _participantService.FindById(userId, eventId);
            return this.CreateResult<IEnumerable<Participant>, IEnumerable<ParticipantViewModel>>(result, o =>
             {
                 o.ToViewModel = x => x.Select(s => s.ToParticipantViewModel());
             });
        }

        [HttpGet("{userId}/{eventId}/getByEventId")]
        public IActionResult GetParticipantInvitedList(int userId, int eventId)
        {
            Result<IEnumerable<Participant>> result = _participantService.FindParticipantsInvitedById(userId, eventId);
            return this.CreateResult<IEnumerable<Participant>, IEnumerable<ParticipantViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToParticipantViewModel());
            });
        }

        [HttpPost]
        public IActionResult CreateParticipant([FromBody] ParticipantViewModel model)
        {
            Result<Participant> result = _participantService.CreateParticipant(model.UserId, model.EventId, false, false);
            return this.CreateResult<Participant, ParticipantViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToParticipantViewModel();
            });
        }

        [HttpPut("{userId}/{eventId}/update")]
        public IActionResult UpdateParticipant(int userId, int eventId, [FromBody] ParticipantViewModel model)
        {
            Result<Participant> result = _participantService.UpdateParticipant(model.UserId, model.EventId, model.ParticipantType, model.Invitation);
            return this.CreateResult<Participant, ParticipantViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToParticipantViewModel();
            });
        }

        [HttpDelete("{userId}/{eventId}/delete")]
        public IActionResult DeleteNotification(int userId, int eventId)
        {
            Result<int> result = _participantService.Delete(userId, eventId);
            return this.CreateResult(result);
        }

        [HttpDelete("{userId}/{eventId}/removeParticipant")]
        public IActionResult RemoveParticipant(int eventId, int userId)
        {
            Result<int> result = _participantService.Delete(userId, eventId);
            return this.CreateResult(result);
        }
    }
}
