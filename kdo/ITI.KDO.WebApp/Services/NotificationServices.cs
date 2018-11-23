using ITI.KDO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class NotificationServices
    {
        readonly UserGateway _userGateway;
        readonly ContactGateway _contactGateway;
        readonly ParticipantGateway _participantGateway;
        readonly EventGateway _eventGateway;

        public NotificationServices(UserGateway userGateway, ContactGateway contactGateway, ParticipantGateway participantGateway, EventGateway eventGateway)
        {
            _userGateway = userGateway;
            _contactGateway = contactGateway;
            _participantGateway = participantGateway;
            _eventGateway = eventGateway;
        }

        public Result<IEnumerable<ContactNotification>> GetContactNotification(int userId)
        {
            IEnumerable<ContactNotification> listNotification = GetContactNotificationList(_contactGateway.GetContactNotification(userId));
            return Result.Success(Status.Ok, listNotification);
        }

        public Result<IEnumerable<EventNotification>> GetEventNotification(int userId)
        {
            IEnumerable<EventNotification> listNotification = GetEventNotificationList(_participantGateway.GetInviteNotification(userId));
            return Result.Success(Status.Ok, listNotification);
        }

        public IEnumerable<EventNotification> GetEventNotificationList(IEnumerable<Participant> listParticipant)
        {
            List<EventNotification> listNotification = new List<EventNotification>();
            foreach (Participant participant in listParticipant)
            {
                EventNotification notification = new EventNotification();
                notification.EventId = participant.EventId;
                notification.EventName = _eventGateway.FindById(participant.EventId).EventName;
                notification.EventDate = _eventGateway.FindById(participant.EventId).Dates;
                notification.Description = _eventGateway.FindById(participant.EventId).Descriptions;
                listNotification.Add(notification);
            }
            return listNotification;
        }

        public IEnumerable<ContactNotification> GetContactNotificationList(IEnumerable<ContactData> listContactData)
        {
            List<ContactNotification> listNotification = new List<ContactNotification>();
            foreach(ContactData contactData in listContactData)
            {
                ContactNotification notification = new ContactNotification();
                notification.ContactId = contactData.ContactId;
                notification.SenderEmail = _userGateway.FindById(contactData.UserId).Email;
                notification.RecipientsEmail = _userGateway.FindById(contactData.FriendId).Email;
                listNotification.Add(notification);
            }
            return listNotification;
        }
    }
}
