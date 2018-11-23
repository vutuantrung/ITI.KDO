using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Models.EventViewModels
{
    public class EventSuggestViewModel
    {
        public int EventId { get; set; }

        public int UserId { get; set; }

        public DateTime DateSuggest { get; set; }

        public string Descriptions { get; set; }
    }
}
