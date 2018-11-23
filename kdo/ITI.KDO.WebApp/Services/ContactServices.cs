using ITI.KDO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class ContactServices
    {
        readonly ContactGateway _contactGateway;
        readonly UserGateway _userGateway;

        public ContactServices(ContactGateway contactGateway, UserGateway userGateway)
        {
            _contactGateway = contactGateway;
            _userGateway = userGateway;
        }

        public Result<IEnumerable<Contact>> GetAllByUserId(int userId)
        {
            return Result.Success(Status.Ok, GetContactList(_contactGateway.FindAllByUserId(userId)));
        }

        public Result<IEnumerable<User>> GetFriendsByUserId(int userId)
        {
            return Result.Success(Status.Ok, GetFriendsByUserIdaux(userId));
        }

        public Result<ContactData> FindByIds(int userId, int friendId)
        {
            ContactData contact;
            if ((contact = _contactGateway.FindByIds(userId, friendId)) == null) return Result.Failure<ContactData>(Status.NotFound, "Contact not found.");
            return Result.Success(Status.Ok, contact);
        }

        public Result Delete(int contactId)
        {
            if (_contactGateway.FindByContactId(contactId) == null) return Result.Failure(Status.NotFound, "Contact not found.");
            _contactGateway.Delete(contactId);
            return Result.Success(Status.Ok);
        }

        public Result CreateContact(int userId, int friendId, bool invitation)
        {
            if (_userGateway.FindById(userId) == null) return Result.Failure(Status.NotFound, "User not found.");
            if (_userGateway.FindById(friendId) == null) return Result.Failure(Status.NotFound, "User not found.");
            if (_contactGateway.FindByIds(userId, friendId) != null) return Result.Failure(Status.BadRequest, "Contact existed.");

            _contactGateway.CreateContact(userId, friendId, false);
            return Result.Success(Status.Ok);
        }

        public Result SetContactInvitation(int userId, int friendId)
        {
            if (_userGateway.FindById(userId) == null) return Result.Failure(Status.NotFound, "User not found.");
            if (_userGateway.FindById(friendId) == null) return Result.Failure(Status.NotFound, "User not found.");
            if (_contactGateway.FindByIds(userId, friendId) == null) return Result.Failure(Status.BadRequest, "Contact invitation not found.");
            if (_contactGateway.FindByIds(userId, friendId) != null && _contactGateway.FindByIds(userId, friendId).Invitation == true)
                return Result.Failure(Status.BadRequest, "Contact existed.");

            _contactGateway.SetContactInvitation(userId, friendId);
            return Result.Success(Status.Ok);
        }

        public IEnumerable<Contact> GetContactList(IEnumerable<ContactData> listContactData)
        {
            List<Contact> listContact = new List<Contact>();
            foreach(ContactData contactData in listContactData)
            {
                Contact contact = new Contact();
                contact.ContactId = contactData.ContactId;
                contact.UserEmail = _userGateway.FindById(contactData.UserId).Email; ;
                contact.FriendEmail = _userGateway.FindById(contactData.FriendId).Email; ;
                listContact.Add(contact);
            }
            return listContact;
        }

        public IEnumerable<User> GetFriendsByUserIdaux(int userId)
        {
            IEnumerable<ContactData> listContact = _contactGateway.FindAllByUserId(userId);
            List<User> listFriends = new List<User>();
            foreach (ContactData couple in listContact)
            {
                if (couple.Invitation != false)
                {
                    if (couple.FriendId != userId)
                    {
                        listFriends.Add(_userGateway.FindById(couple.FriendId));
                    }
                    else
                    {
                        listFriends.Add(_userGateway.FindById(couple.UserId));
                    }
                }
            }
            return listFriends;
        }
    }
}
