using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Models.QuantityViewModels;
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
    public class QuantityController : Controller
    {
        readonly QuantityServices _quantityService;

        public QuantityController(QuantityServices quantityService)
        {
            _quantityService = quantityService;
        }

        [HttpGet("{quantityId}", Name = "GetQuantity")]
        public IActionResult GetQuantityById(int quantityId)
        {
            Result<ItemQuantity> result = _quantityService.GetById(quantityId);
            return this.CreateResult<ItemQuantity, ItemQuantityViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToQuantityViewModel();
            });
        }

        [HttpGet("{eventId}/getQuantityByEventId")]
        public IActionResult FindQuantityByEventId(int eventId)
        {
            Result<IEnumerable<ItemQuantity>> result = _quantityService.GetByEventId(eventId);
            return this.CreateResult<IEnumerable<ItemQuantity>, IEnumerable<ItemQuantityViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToQuantityViewModel());
            });
        }

        [HttpPost]
        public IActionResult CreateQuantity([FromBody] ItemQuantityViewModel model)
        {
            Result<ItemQuantity> result = _quantityService.CreateQuantity(model.QuantityId, model.Quantity, model.RecipientId, model.NominatorId, model.EventId, model.PresentId);
            return this.CreateResult<ItemQuantity, ItemQuantityViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToQuantityViewModel();
                o.RouteName = "GetQuantity";
                o.RouteValues = s => new { id = s.QuantityId };
            });
        }

        [HttpPut("{quantityId}")]
        public IActionResult UpdateQuantity(int quantityId, [FromBody] ItemQuantityViewModel model)
        {
            Result<ItemQuantity> result = _quantityService.UpdateQuantity(model.QuantityId, model.Quantity, model.RecipientId, model.NominatorId, model.EventId, model.PresentId);
            return this.CreateResult<ItemQuantity, ItemQuantityViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToQuantityViewModel();
            });
        }

        [HttpDelete("{quantityId}")]
        public IActionResult DeleteQuantity(int quantityId)
        {
            Result<int> result = _quantityService.Delete(quantityId);
            return this.CreateResult(result);
        }

        [HttpGet("{userId}/{eventId}")]
        public IActionResult GetUserPresentEvent(int userId, int eventId)
        {
            Result <IEnumerable<ItemPresentQuantity>> result = _quantityService.GetUserPresentEvent(userId, eventId);
            return this.CreateResult<IEnumerable<ItemPresentQuantity>, IEnumerable<ItemQuantityPresentViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToQuantityPresentViewModel());
            });
        }
    }
}
