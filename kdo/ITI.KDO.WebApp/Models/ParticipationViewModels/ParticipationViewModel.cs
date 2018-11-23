using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Models.ParticipationViewModels
{
    public class ParticipationViewModel
    {
        public int QuantityId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public int AmountUserPrice { get; set; }
    }
}