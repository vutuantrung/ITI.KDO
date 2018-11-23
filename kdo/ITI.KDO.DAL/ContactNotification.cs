using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class ContactNotification
    {
        public int ContactId { get; set; }

        public string RecipientsEmail { get; set; }

        public string SenderEmail { get; set; }
    }
}
