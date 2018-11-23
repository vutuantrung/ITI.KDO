using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL.Tests
{
    [TestFixture]
    public class ContactGatewayTests
    {
        private static ContactGateway _contactGateway;
        private static ContactGateway ContactGateway => _contactGateway ?? (_contactGateway = new ContactGateway(TestHelpers.ConnectionString));


        [Test]
        public void can_add_find_delete_contact()
        {
            UserGateway sut = new UserGateway(TestHelpers.ConnectionString);

            string firstName = TestHelpers.RandomTestName();
            string lastName = TestHelpers.RandomTestName();
            DateTime birthDate = TestHelpers.RandomBirthDate(21);
            string email = TestHelpers.RandomEmail();
            string phone = TestHelpers.RandomPhone();
            string photo = TestHelpers.RandomPhoto();
            bool invitation = false;

            var userId = sut.Create(firstName, lastName, birthDate, email);
            var friendId = sut.Create(firstName, lastName, birthDate, email);

            ContactGateway.CreateContact(userId, friendId, invitation);

            User user = sut.FindById(userId);

            ContactData contact = ContactGateway.FindByIds(userId, friendId);

            {
                Assert.That(contact.UserId, Is.EqualTo(userId));
                Assert.That(contact.FriendId, Is.EqualTo(friendId));
                Assert.That(contact.Invitation, Is.EqualTo(invitation));
            }

            {
                ContactGateway.Delete(contact.ContactId);
                Assert.That(ContactGateway.FindByIds(userId, friendId), Is.Null);
            }

            {
                
                sut.Delete(userId);
                Assert.That(sut.FindById(contact.UserId), Is.Null);

                sut.Delete(friendId);
                Assert.That(sut.FindById(contact.FriendId), Is.Null);
            }

        }
    }
}
