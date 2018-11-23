using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class Participant
    {
        public int UserId { get; set; }

        public int EventId { get; set; }

        public bool ParticipantType { get; set; }

        public bool Invitation { get; set; }
    }
}
