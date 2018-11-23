using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Models.NotificationViewModels
{
    public class ContactNotificationViewModel
    {
        public int ContactId { get; set; }

        public string RecipientsEmail { get; set; }

        public string SenderEmail { get; set; }
    }
}
