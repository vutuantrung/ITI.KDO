using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Models.UserViewModels
{
    public class ContactDataViewModel
    {
        public int ContactId { get; set; }

        public int UserId { get; set; }

        public int FriendId { get; set; }

        public bool Invitation { get; set; }
    }
}
