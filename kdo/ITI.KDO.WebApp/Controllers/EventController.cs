using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Services;
using ITI.KDO.WebApp.Models.EventViewModels;
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
    public class EventController: Controller
    {
        readonly EventServices _eventService;
        public EventController(EventServices eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{userId}/getEventList")]
        public IActionResult GetEventList(int userId)
        {
            Result<IEnumerable<Event>> result = _eventService.GetAllEvents(userId);

            return this.CreateResult<IEnumerable<Event>, IEnumerable<EventViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToEventViewModel());
            });
        }

        [HttpGet("{eventId}/{userId}/getEventSuggestList")]
        public IActionResult GetEventSuggestList(int userId, int eventId)
        {
            Result<IEnumerable<EventSuggest>> result = _eventService.GetAllEventsSuggest(userId, eventId);

            return this.CreateResult<IEnumerable<EventSuggest>, IEnumerable<EventSuggestViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToEventSuggestViewModel());
            });
        }

        [HttpGet("{userId}/getEventSuggestListByUserId")]
        public IActionResult GetEventSuggestListByUserId(int userId)
        {
            Result<IEnumerable<EventSuggest>> result = _eventService.GetAllEventsSuggestByUserId(userId);

            return this.CreateResult<IEnumerable<EventSuggest>, IEnumerable<EventSuggestViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToEventSuggestViewModel());
            });
        }

        [HttpGet("{eventId}", Name = "GetEvent")]
        public IActionResult GetEventById(int eventId)
        {
            Result<Event> result = _eventService.GetById(eventId);
            return this.CreateResult<Event, EventViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToEventViewModel();
            });
        }

        [HttpGet("{eventId}/{userId}/getEvent")]
        public IActionResult GetEventIds(int eventId, int userId)
        {
            Result<Event> result = _eventService.GetByIds(userId, eventId);
            return this.CreateResult<Event, EventViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToEventViewModel();
            });
        }

        [HttpPost("{createEvent}")]
        public IActionResult CreateEvent([FromBody] EventViewModel model)
        {
            Result<int> result = _eventService.CreateEvent(model.UserId, model.EventName, model.Descriptions, model.Dates);
            return this.CreateResult(result);
        }

        [HttpPut("{eventId}")]
        public IActionResult UpdateEvent(int eventId, [FromBody] EventViewModel model)
        {
            Result<Event> result = _eventService.UpdateEvent(model.EventId, model.UserId, model.EventName, model.Descriptions, model.Dates);
            return this.CreateResult<Event, EventViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToEventViewModel();
            });
        }

        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent(int eventId)
        {
            Result<int> result = _eventService.Delete(eventId);
            return this.CreateResult(result);
        }

        [HttpPost("createEventSuggest")]
        public IActionResult CreateSuggestEvent([FromBody] EventSuggestViewModel model)
        {
            Result<EventSuggest> result = _eventService.CreateEventSuggest(model.EventId, model.UserId, model.DateSuggest, model.Descriptions);
            return this.CreateResult<EventSuggest, EventSuggestViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToEventSuggestViewModel();
                o.RouteName = "GetEventSuggest";
                o.RouteValues = s => new { eventId = s.EventId, userId = s.UserId };
            });
        }

        [HttpGet("{eventId}/{userId}/getEventSuggest", Name = "GetEventSuggest")]
        public IActionResult GetEventSuggestByIds(int eventId, int userId)
        {
            Result<EventSuggest> result = _eventService.GetEventSuggestByIds(userId, eventId);
            return this.CreateResult<EventSuggest, EventSuggestViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToEventSuggestViewModel();
            });
        }
    }
}
