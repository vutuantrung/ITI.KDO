using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class EventSuggest
    {
        public int EventId { get; set; }

        public int UserId { get; set; }

        public DateTime DateSuggest { get; set; }

        public string Descriptions { get; set; }
    }
}
