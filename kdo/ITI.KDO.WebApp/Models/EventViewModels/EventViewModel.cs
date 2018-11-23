using System;

namespace ITI.KDO.WebApp.Models.EventViewModels
{
    public class EventViewModel
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public string Descriptions { get; set; }

        public byte[] Picture { get; set; }

        public DateTime Dates { get; set; }

        public int UserId { get; set; }
    }
}
