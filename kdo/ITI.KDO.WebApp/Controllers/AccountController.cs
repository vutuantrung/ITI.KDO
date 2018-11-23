﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ITI.KDO.WebApp.Services;
using ITI.KDO.WebApp.Models.AccountViewModels;
using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using Mvc.Client.Extensions;
using Microsoft.AspNetCore.Http.Authentication;

namespace ITI.KDO.WebApp.Controllers
{
   
    public class AccountController : Controller
    {
        readonly UserServices _userService;
        readonly TokenService _tokenService;
        readonly Random _random;

        public AccountController(UserServices userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _random = new Random();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ModifyPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ModifyPassword(ModifyPasswordViewModel model)
        {
            Console.WriteLine("Modify Password");
            if (ModelState.IsValid)
            {
                User user = _userService.FindUserByEmail(model.Email);
                if(user != null && !_userService.VerifyPasswordUser(user.UserId))
                {
                    if (model.NewPassword != model.NewPasswordConfirm)
                    {
                        ModelState.AddModelError(string.Empty, "New passwords are not match.");
                        return View(model);
                    }
                    _userService.CreatePasswordIdUser(model);
                    return RedirectToAction(nameof(Authenticated));
                }

                user = _userService.FindUser(model.Email, model.OldPassword);
                Console.WriteLine("User is authenticated {0}", user != null);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password attempt.");
                    return View(model);
                }
                if (model.NewPassword != model.NewPasswordConfirm)
                {
                    ModelState.AddModelError(string.Empty, "New passwords are not match.");
                    return View(model);
                }
                _userService.UpdateUserPassword(user.UserId, model.NewPassword);
                return RedirectToAction(nameof(Authenticated));
            }
            return View();
        }


        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.Authentication.SignOutAsync(CookieAuthentication.AuthenticationScheme);
            ViewData["NoLayout"] = true;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ExternalLogin([FromQuery] string provider)
        {
            // Note: the "provider" parameter corresponds to the external
            // authentication provider choosen by the user agent.
            if (string.IsNullOrWhiteSpace(provider))
            {
                return BadRequest();
            }

            if (!HttpContext.IsProviderSupported(provider))
            {
                return BadRequest();
            }

            // Instruct the middleware corresponding to the requested external identity
            // provider to redirect the user agent to its own authorization endpoint.
            // Note: the authenticationScheme parameter must match the value configured in Startup.cs
            string redirectUri = Url.Action(nameof(ExternalLoginCallback), "Account");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUri }, provider);
        }


        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public IActionResult ExternalLoginCallback()
        {
            return RedirectToAction(nameof(Authenticated));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login( LoginViewModel model )
        {
            Console.WriteLine("Login");
            if (ModelState.IsValid)
            {
                User user = _userService.FindUser(model.Email,model.Password);
                Console.WriteLine("User is authenticated {0}", user != null);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
                await SignIn(user.Email, user.UserId.ToString());
                return RedirectToAction(nameof(Authenticated));
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userService.FindUserByEmail(model.Email) != null )
                {
                    ModelState.AddModelError(string.Empty, "An account with this email already exists.");
                    return View(model);
                }
                _userService.CreatePasswordUser(model);
                User user = _userService.FindUserByEmail(model.Email);
                await SignIn(user.Email, user.UserId.ToString());
                return RedirectToAction(nameof(Authenticated));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public IActionResult Authenticated()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string email = User.FindFirst(ClaimTypes.Email).Value;
            Token token = _tokenService.GenerateToken(userId, email);
            IEnumerable<string> providers = _userService.GetAuthenticationProviders(userId);
            ViewData["BreachPadding"] = GetBreachPadding(); // Mitigate BREACH attack. See http://www.breachattack.com/
            ViewData["Token"] = token;
            ViewData["Email"] = email;
            ViewData["NoLayout"] = true;
            ViewData["Providers"] = providers;
            return View();
        }



        async Task SignIn(string email, string userId)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim( ClaimTypes.Email, email, ClaimValueTypes.String ),
                new Claim( ClaimTypes.NameIdentifier, userId.ToString(), ClaimValueTypes.String )
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthentication.AuthenticationType, ClaimTypes.Email, string.Empty);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.Authentication.SignInAsync(CookieAuthentication.AuthenticationScheme, principal);
        }

        string GetBreachPadding()
        {
            byte[] data = new byte[_random.Next(64, 256)];
            _random.NextBytes(data);
            return Convert.ToBase64String(data);
        }

    }
}