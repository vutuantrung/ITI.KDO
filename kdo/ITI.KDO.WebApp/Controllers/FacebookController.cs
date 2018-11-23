using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Services;
using System.Security.Claims;
using ITI.KDO.DAL;
using ITI.KDO.WebApp.Models.UserViewModels;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class FacebookController : Controller
    {
        readonly FacebookServices _facebookServices;

        public FacebookController(FacebookServices facebookServices)
        {
            _facebookServices = facebookServices;
        }

        [HttpGet("getFriends")]
        public async Task<IActionResult> GetFacebookFriends()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<IEnumerable<FacebookContact>> result = await _facebookServices.GetFacebookContacts(userId);
            return this.CreateResult<IEnumerable<FacebookContact>, IEnumerable<FacebookContactViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToFacebookContactViewModel());
            });
        }
    }
}