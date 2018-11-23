using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Models.ParticipantViewModels
{
    public class ParticipantViewModel
    {
        public int UserId { get; set; }

        public int EventId { get; set; }

        public bool ParticipantType { get; set; }

        public bool Invitation { get; set; }
    }
}
