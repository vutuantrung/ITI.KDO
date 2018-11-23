using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Services;
using ITI.KDO.DAL;
using ITI.KDO.WebApp.Models.NotificationViewModels;
using ITI.KDO.WebApp.Models.ParticipantViewModels;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class NotificationController : Controller
    {
        readonly NotificationServices _notificationServices;
        readonly ContactServices _contactServices;
        readonly UserServices _userServices;
        readonly ParticipantServices _participantServices;

        public NotificationController(NotificationServices notificationServices, ContactServices contactServices, UserServices userServices, ParticipantServices participantServices)
        {
            _notificationServices = notificationServices;
            _contactServices = contactServices;
            _userServices = userServices;
            _participantServices = participantServices;
        }

        [HttpPost("setContactInvitation")]
        public IActionResult SetContactNotification([FromBody] ContactNotificationViewModel model)
        {
            Result result = _contactServices.SetContactInvitation(
                _userServices.FindUserByEmail(model.SenderEmail).UserId,
                _userServices.FindUserByEmail(model.RecipientsEmail).UserId
                );
            return this.CreateResult(result);
        }

        [HttpGet("{userId}/getContactNotification")]
        public IActionResult GetContactNotification(int userId)
        {
            Result<IEnumerable<ContactNotification>> result = _notificationServices.GetContactNotification(userId);
            return this.CreateResult<IEnumerable<ContactNotification>, IEnumerable<ContactNotificationViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToContactNotificationViewModel());
            });
        }

        [HttpPost("setEventInvitation")]
        public IActionResult SetEventNotification([FromBody] ParticipantViewModel model)
        {
            Result result = _participantServices.SetEventInvitation(model.UserId, model.EventId);
            return this.CreateResult(result);
        }

        [HttpGet("{userId}/getEventNotification")]
        public IActionResult GetEventNotification(int userId)
        {
            Result<IEnumerable<EventNotification>> result = _notificationServices.GetEventNotification(userId);
            return this.CreateResult<IEnumerable<EventNotification>, IEnumerable<EventNotificationViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToEventNotificationViewModel());
            });
        }
    }
}