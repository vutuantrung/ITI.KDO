using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class FacebookContact
    {
        public int ContactId { get; set; }

        public int UserId { get; set; }

        public int FacebookId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }
    }
}
