﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace ITI.KDO.WebApp.Authentication
{
    public static class OAuthCreatingTicketContextExtensions
    {
        public static string GetEmail(this OAuthCreatingTicketContext @this)
        {
            return @this.Identity.FindFirst(c => c.Type == ClaimTypes.Email).Value;
        }

        public static string GetGoogleId(this OAuthCreatingTicketContext @this)
        {
            return @this.GetNameIdentifier();
        }

        public static string GetFacebookId(this OAuthCreatingTicketContext @this)
        {
            return @this.GetNameIdentifier();
        }

        static string GetNameIdentifier(this OAuthCreatingTicketContext @this)
        {
            return @this.Identity.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
