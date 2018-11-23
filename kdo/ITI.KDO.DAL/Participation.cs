using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class Participation
    {
        public int QuantityId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public int AmountUserPrice { get; set; }
    }
}
