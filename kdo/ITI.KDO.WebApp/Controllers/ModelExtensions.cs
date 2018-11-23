using ITI.KDO.DAL;
using ITI.KDO.WebApp.Models.CategoryPresentViewModels;
using ITI.KDO.WebApp.Models.EmailViewModels;
using ITI.KDO.WebApp.Models.NotificationViewModels;
using ITI.KDO.WebApp.Models.PresentViewModels;
using ITI.KDO.WebApp.Models.QuantityViewModels;
using ITI.KDO.WebApp.Models.UserViewModels;
using ITI.KDO.WebApp.Models.EventViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.KDO.WebApp.Models.ParticipantViewModels;
using ITI.KDO.WebApp.Models.ParticipationViewModels;

namespace ITI.KDO.WebApp.Controllers
{
    public static class ModelExtensions
    {
        public static UserViewModel ToUserViewModel(this User @this)
        {
            return new UserViewModel
            {
                UserId = @this.UserId,
                Email = @this.Email,
                FirstName = @this.FirstName,
                LastName = @this.LastName,
                Birthdate = @this.Birthdate,
                Phone = @this.Phone,
                Photo = @this.Photo
            };
        }

        public static PresentViewModel ToPresentViewModel(this Present @this)
        {
            return new PresentViewModel
            {
                PresentId = @this.PresentId,
                PresentName = @this.PresentName,
                Picture = @this.Picture,
                Price = @this.Price,
                LinkPresent = @this.LinkPresent,
                CategoryPresentId = @this.CategoryPresentId,
                UserId = @this.UserId
            };
        }

        public static ItemQuantityViewModel ToQuantityViewModel(this ItemQuantity @this)
        {
            return new ItemQuantityViewModel
            {
                QuantityId = @this.QuantityId,
                Quantity = @this.Quantity,
                RecipientId = @this.RecipientId,
                NominatorId = @this.NominatorId,
                EventId = @this.EventId,
                PresentId = @this.PresentId
            };
         }

        public static ItemQuantityPresentViewModel ToQuantityPresentViewModel(this ItemPresentQuantity @this)
        {
            return new ItemQuantityPresentViewModel
            {
                QuantityId = @this.QuantityId,
                Quantity = @this.Quantity,
                Price = @this.Price,
                RecipientId = @this.RecipientId,
                NominatorId = @this.NominatorId,
                PresentId = @this.PresentId,
                PresentName = @this.PresentName,
                LinkPresent = @this.LinkPresent,
                CategoryPresentId = @this.CategoryPresentId,
                ParticipantType = @this.ParticipantType,
                Invitation = @this.Invitation,
                EventId = @this.EventId,
            };
        }

        public static ParticipationViewModel ToParticipationViewModel(this Participation @this)
        {
            return new ParticipationViewModel
            {
                QuantityId = @this.QuantityId,
                UserId = @this.UserId,
                EventId = @this.EventId,
                AmountUserPrice = @this.AmountUserPrice
            };
        }

        public static CategoryPresentViewModel ToCategoryPresentViewModel(this CategoryPresent @this)
        {
            return new CategoryPresentViewModel
            {
                CategoryPresentId = @this.CategoryPresentId,
                CategoryName = @this.CategoryName,
                Link = @this.Link
            };
        }

        public static ContactNotificationViewModel ToContactNotificationViewModel(this ContactNotification @this)
        {
            return new ContactNotificationViewModel
            {
                ContactId = @this.ContactId,
                RecipientsEmail = @this.RecipientsEmail,
                SenderEmail = @this.SenderEmail
            };
        }

        public static EventNotificationViewModel ToEventNotificationViewModel(this EventNotification @this)
        {
            return new EventNotificationViewModel
            {
                EventId = @this.EventId,
                EventName = @this.EventName,
                EventDate = @this.EventDate,
                Description = @this.Description
            };
        }

        public static ContactDataViewModel ToContactDataViewModel(this ContactData @this)
        {
            return new ContactDataViewModel
            {
                ContactId = @this.ContactId,
                UserId = @this.UserId,
                FriendId = @this.FriendId,
                Invitation = @this.Invitation
            };
        }

        public static ContactViewModel ToContactViewModel(this Contact @this)
        {
            return new ContactViewModel
            {
                ContactId = @this.ContactId,
                UserEmail = @this.UserEmail,
                FriendEmail = @this.FriendEmail
            };
        }

        public static FacebookContactViewModel ToFacebookContactViewModel(this FacebookContact @this)
        {
            return new FacebookContactViewModel
            {
                ContactId = @this.ContactId,
                UserId = @this.UserId,
                FacebookId = @this.FacebookId,
                Email = @this.Email,
                FirstName = @this.FirstName,
                LastName = @this.LastName,
                BirthDate = @this.BirthDate,
                Phone = @this.Phone
            };
        }
        
        public static EventViewModel ToEventViewModel(this Event @this)
        {
            return new EventViewModel
            {
                EventId = @this.EventId,
                EventName = @this.EventName,
                Descriptions = @this.Descriptions,
                Picture = @this.Picture,
                Dates = @this.Dates,
                UserId = @this.UserId
            };
        }

        public static ParticipantViewModel ToParticipantViewModel(this Participant @this)
        {
            return new ParticipantViewModel
            {
                UserId = @this.UserId,
                EventId = @this.EventId,
                ParticipantType = @this.ParticipantType,
                Invitation = @this.Invitation
            };
        }

        public static EventSuggestViewModel ToEventSuggestViewModel(this EventSuggest @this)
        {
            return new EventSuggestViewModel
            {
                EventId = @this.EventId,
                UserId = @this.UserId,
                DateSuggest = @this.DateSuggest,
                Descriptions = @this.Descriptions
            };
        }

        //public static CategoryPresentViewModel ToCategoryPresentViewModel(this CategoryPresent @this)
        //{
        //    return new CategoryPresentViewModel
        //    {
        //        CategoryPresentId = @this.CategoryPresentId,
        //        CategoryName = @this.CategoryName,
        //        Link = @this.Link
        //    };
        //}
    }
}

        
