using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Models.EmailViewModels
{
    public class FriendInvitationViewModel
    {
        public string RecipientsEmail { get; set; }

        public string SenderEmail { get; set; }

        public string Descriptions { get; set; }
    }
}
