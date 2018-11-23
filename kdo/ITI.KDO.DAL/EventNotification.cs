using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class EventNotification
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }
    }
}
