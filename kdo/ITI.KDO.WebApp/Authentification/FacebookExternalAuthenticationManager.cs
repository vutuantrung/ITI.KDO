using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentication;
using ITI.KDO.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Authentification
{
    public class FacebookExternalAuthenticationManager : IExternalAuthenticationManager
    {
        readonly UserServices _userServices;
        readonly FacebookServices _facebookServices;

        public FacebookExternalAuthenticationManager(UserServices userServices, FacebookServices facebookServices)
        {
            _userServices = userServices;
            _facebookServices = facebookServices;
        }

        public void CreateOrUpdateUser(OAuthCreatingTicketContext context)
        {
            if(context.AccessToken != null)
            {
                JObject jsonFile = (_facebookServices.GetFacebookInformation(context.AccessToken)).Result;
                _userServices.CreateOrUpdateFacebookUser(context.GetEmail(), context.GetFacebookId(), context.AccessToken, jsonFile);
            }
        }

        public User FindUser(OAuthCreatingTicketContext context)
        {
            return _userServices.FindUser(context.GetEmail());
        }
    }
}
