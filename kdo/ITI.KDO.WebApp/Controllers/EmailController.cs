using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Models.EmailViewModels;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using ITI.KDO.WebApp.Services;
using ITI.KDO.DAL;

namespace ITI.KDO.WebApp.Controllers
{
    public class EmailController : Controller
    {
        UserServices _userServices;

        public EmailController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public IActionResult FriendInvitation()
        {
            return View();
        }

        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public async Task<IActionResult> FriendInvitation([FromForm] FriendInvitationViewModel model)
        {
            string _subject = "Friends invitation";

            var emailMessage = new MimeMessage();

            if (ModelState.IsValid)
            {
                if (model.RecipientsEmail == null)
                {
                    ModelState.AddModelError(string.Empty, "Please fill Recipients's email.");
                    return View(model);
                }

                //The MimeMessage has a “from” address list and a “to” address list that we can populate with our sender and recipient(s). 
                //The basic constructor for the MailboxAddress takes in a display name and the email address for the mailbox. 
                emailMessage.From.Add(new MailboxAddress(model.SenderEmail));
                emailMessage.To.Add(new MailboxAddress(model.RecipientsEmail));
                emailMessage.Subject = _subject;
                emailMessage.Body = new TextPart("plain") { Text = model.Descriptions };


                //The final step is to send the message and to do that we use a SmtpClient. 
                //This isn’t the SmtpClient from system.net.mail, it is part of the MailKit library. 

                //Create an instance of the SmtpClient wrapped with a using statement to ensure that it is disposed of when we’re done with it. 
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    //You can if required set the LocalDomain used when communicating with the SMTP server. 
                    //This will be presented as the origin of the emails. 
                    //In this case I needed to supply the domain so that our internal testing SMTP server would accept and relay my emails. 
                    //client.LocalDomain = "some.domain.com";

                    //The ConnectAsync method can take just the uri of the SMTP server or as I’ve done here be overloaded with a port and SSL option. 
                    //In this case, when testing with our local test SMTP server no SSL was required so I specified this explicitly to make it work. 
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("webappkdoiti@gmail.com", "webappkdo2017");
                    //Finally we can send the message asynchronously and then close the connection. 
                    //At this point the email should have been fired off via the SMTP server. 
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            return View();
        }
    }
}