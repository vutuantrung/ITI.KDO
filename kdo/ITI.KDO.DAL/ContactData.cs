using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class ContactData
    {
        public int ContactId { get; set; }

        public int UserId { get; set; }

        public int FriendId { get; set; }

        public bool Invitation { get; set; }
    }
}
