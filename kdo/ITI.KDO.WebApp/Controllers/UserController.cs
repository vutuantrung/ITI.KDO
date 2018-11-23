using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Models.UserViewModels;
using ITI.KDO.WebApp.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class UserController : Controller
    {
        readonly UserServices _userServices;
        private object _userService;

        public UserController( UserServices userServices )
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult GetUserList()
        {
            Result<IEnumerable<User>> result = _userServices.GetAll();
            return this.CreateResult<IEnumerable<User>, IEnumerable<UserViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToUserViewModel());
            });
        }

        [HttpGet("{emailUser}")]
        public User GetUserByEmail(string emailUser)
        {
            User user = _userServices.FindUser(emailUser);
            return user;
        }

        [HttpGet("{userId}/search")]
        public User GetUserById(int userId)
        {
            User user = _userServices.FindUserById(userId);
            return user;
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] UserViewModel model)
        {
            Result<User> result = _userServices.UpdateUser(model.UserId, model.FirstName, model.LastName, model.Email, model.Phone, model.Photo);
            return this.CreateResult<User, UserViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToUserViewModel();
            });
        }
    }
}
