using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Services;
using ITI.KDO.DAL;
using ITI.KDO.WebApp.Models.UserViewModels;
using ITI.KDO.WebApp.Models.NotificationViewModels;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ContactController : Controller
    {
        readonly ContactServices _contactServices;
        readonly UserServices _userServices;

        public ContactController(ContactServices contactServices, UserServices userServices)
        {
            _contactServices = contactServices;
            _userServices = userServices;
        }

        [HttpGet("{userId}/getAll")]
        public IActionResult GetContactList(int userId)
        {
            Result<IEnumerable<Contact>> result = _contactServices.GetAllByUserId(userId);
            return this.CreateResult<IEnumerable<Contact>, IEnumerable<ContactViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToContactViewModel());
            });
        }

        [HttpPost("createContact")]
        public IActionResult CreateContact([FromBody] ContactDataViewModel model)
        {
            Result result = _contactServices.CreateContact(model.UserId, model.FriendId, model.Invitation);
            return this.CreateResult(result);
        }

        [HttpGet("{userId}/{friendId}")]
        public IActionResult GetContactByIds(int userId, int friendId)
        {
            Result<ContactData> result = _contactServices.FindByIds(userId, friendId);
            return this.CreateResult<ContactData, ContactDataViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToContactDataViewModel();
            });
        }

        [HttpGet("{userId}/getFriends")]
        public IActionResult GetFriends(int userId)
        {
            Result<IEnumerable<User>> result = _contactServices.GetFriendsByUserId(userId);
            return this.CreateResult<IEnumerable<User>, IEnumerable<UserViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToUserViewModel());
            });
        }

        [HttpDelete("{contactId}")]
        public IActionResult DeleteContact(int contactId)
        {
            Result result = _contactServices.Delete(contactId);
            return this.CreateResult(result);
        }
    }
}