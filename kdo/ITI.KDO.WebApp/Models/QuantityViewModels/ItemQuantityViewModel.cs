using System;

namespace ITI.KDO.WebApp.Models.QuantityViewModels
{
    public class ItemQuantityViewModel
    {
        public int QuantityId { get; set; }

        public int Quantity { get; set; }

        public int RecipientId { get; set; }

        public int NominatorId { get; set; }

        public int EventId { get; set; }

        public int PresentId { get; set; }
    }

    public class ItemQuantityPresentViewModel
    {
        public int QuantityId { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public int RecipientId { get; set; }

        public int NominatorId { get; set; }

        public int PresentId { get; set; }

        public string PresentName { get; set; }

        public string LinkPresent { get; set; }

        public int CategoryPresentId { get; set; }

        public int ParticipantType { get; set; }

        public int Invitation { get; set; }

        public int EventId { get; set; }
    }
}