using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL.Tests
{
    [TestFixture]
    public class ItemQuantityGatewayTests
    {
        private static EventGateway _eventGateway;
        private static UserGateway _userGateway;
        private static ParticipantGateway _participantGateway;
        private static ItemQuantityGateway _itemQuantityGateway;
        private static PresentGateway _presentGateway;

        private static EventGateway EventGateway => _eventGateway ?? (_eventGateway = new EventGateway(TestHelpers.ConnectionString));
        private static UserGateway UserGateway => _userGateway ?? (_userGateway = new UserGateway(TestHelpers.ConnectionString));
        private static ParticipantGateway ParticipantGateway => _participantGateway ?? (_participantGateway = new ParticipantGateway(TestHelpers.ConnectionString));
        private static ItemQuantityGateway ItemQuantityGateway => _itemQuantityGateway ?? (_itemQuantityGateway = new ItemQuantityGateway(TestHelpers.ConnectionString));
        private static PresentGateway PresentGateway => _presentGateway ?? (_presentGateway = new PresentGateway(TestHelpers.ConnectionString));
        [Test]
        public void can_create_find_update_and_delete_Quantity()
        {
            string firstName = TestHelpers.RandomTestName();
            string lastName = TestHelpers.RandomTestName();
            DateTime birthDate = TestHelpers.RandomBirthDate(21);
            string email = TestHelpers.RandomEmail();
            string phone = TestHelpers.RandomPhone();
            string photo = TestHelpers.RandomPhoto();

            byte[] picture = TestHelpers.GetBytesArray(12);

            string eventName = TestHelpers.RandomTestName();
            string descriptions = TestHelpers.RandomTestName();
            DateTime date = TestHelpers.RandomBirthDate(10);

            string presentName = TestHelpers.RandomPresentName();
            float price = TestHelpers.RandomPrice();
            string linkPresent = TestHelpers.RandomLink();
            int categoryPresentId = 0;
            int quantity = 1;

            var user1 = UserGateway.Create(firstName, lastName, birthDate, email);
            var user2 = UserGateway.Create(firstName, lastName, birthDate, email);
            var user3 = UserGateway.Create(firstName, lastName, birthDate, email);

            var presentId = PresentGateway.AddToUser(presentName, price, linkPresent, picture, categoryPresentId, user2);

            var eventId = EventGateway.Create(eventName, descriptions, date, user1);

            ParticipantGateway.Create(user1, eventId, true, true);
            ParticipantGateway.Create(user2, eventId, false, true);
            ParticipantGateway.Create(user3, eventId, false, true);

            var quantityId = ItemQuantityGateway.Create(quantity, user1, user2, eventId, presentId );

            {
                ItemQuantity itemQuantity = ItemQuantityGateway.FindById(quantityId);
                Assert.That(itemQuantity.QuantityId, Is.EqualTo(quantityId));
                Assert.That(itemQuantity.Quantity, Is.EqualTo(quantity));
                Assert.That(itemQuantity.RecipientId, Is.EqualTo(user1));
                Assert.That(itemQuantity.NominatorId, Is.EqualTo(user2));
                Assert.That(itemQuantity.EventId, Is.EqualTo(eventId));
                Assert.That(itemQuantity.PresentId, Is.EqualTo(presentId));
            }

            {
                int quantitys = 3;
                ItemQuantityGateway.Update(quantityId, quantitys, user3, user2, eventId, presentId);
                ItemQuantity itemQuantity = ItemQuantityGateway.FindById(quantityId);
                Assert.That(itemQuantity.Quantity, Is.EqualTo(quantitys));
                Assert.That(itemQuantity.RecipientId, Is.EqualTo(user3));
                Assert.That(itemQuantity.NominatorId, Is.EqualTo(user2));
                Assert.That(itemQuantity.EventId, Is.EqualTo(eventId));
            }

            {
                ItemQuantityGateway.Delete(quantityId);
                Assert.That(ItemQuantityGateway.FindById(quantityId), Is.Null);
            }

            {
                PresentGateway.Delete(presentId);
                Assert.That(PresentGateway.FindByPresentId(presentId), Is.Null);
            }

            {
                ParticipantGateway.Delete(user2, eventId);
                Assert.That(ParticipantGateway.FindByIds(user2, eventId), Is.Null);
                ParticipantGateway.Delete(user1, eventId);
                Assert.That(ParticipantGateway.FindByIds(user2, eventId), Is.Null);
                ParticipantGateway.Delete(user3, eventId);
                Assert.That(ParticipantGateway.FindByIds(user3, eventId), Is.Null);
            }

            {
                EventGateway.Delete(eventId);
                Assert.That(EventGateway.FindById(eventId), Is.Null);
            }

            {
                UserGateway.Delete(user1);
                Assert.That(UserGateway.FindById(user1), Is.Null);
                UserGateway.Delete(user2);
                Assert.That(UserGateway.FindById(user2), Is.Null);
                UserGateway.Delete(user3);
                Assert.That(UserGateway.FindById(user3), Is.Null);
            }
        }
    }
}
