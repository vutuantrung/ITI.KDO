using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Models.UserViewModels
{
    public class ContactViewModel
    {
        public int ContactId { get; set; }

        public string UserEmail { get; set; }

        public string FriendEmail { get; set; }
    }
}
