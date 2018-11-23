﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ITI.KDO.WebApp.Services;
using ITI.KDO.WebApp.Authentification;
using System.Collections.Generic;


namespace ITI.KDO.WebApp.Controllers
{
    public class HomeController : Controller
    {
        readonly TokenService _tokenService;
        readonly UserServices _userServices;

        public HomeController(TokenService tokenService, UserServices userService)
        {
            _tokenService = tokenService;
            _userServices = userService;
        }

        public IActionResult Index()
        {
            ClaimsIdentity identity = User.Identities.SingleOrDefault(i => i.AuthenticationType == CookieAuthentication.AuthenticationType);
            if (identity != null)
            {
                string userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                string email = identity.FindFirst(ClaimTypes.Email).Value;
                Token token = _tokenService.GenerateToken(userId, email);
                IEnumerable<string> providers = _userServices.GetAuthenticationProviders(userId);
                ViewData["Token"] = token;
                ViewData["Email"] = email;
                ViewData["Providers"] = providers;
            }
            else
            {
                ViewData["Token"] = null;
                ViewData["Email"] = null;
                ViewData["Providers"] = null;
            }

            ViewData["NoLayout"] = true;
            return View();
        }
        
    }
}
