using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL.Tests
{
    [TestFixture]
    public class EventGatewayTest
    {
        private static EventGateway _eventGateway;
        private static UserGateway _userGateway;
        private static EventGateway EventGateway => _eventGateway ?? (_eventGateway = new EventGateway(TestHelpers.ConnectionString));
        private static UserGateway UserGateway => _userGateway ?? (_userGateway = new UserGateway(TestHelpers.ConnectionString));

        [Test]
        public void can_create_find_update_and_delete_Event()
        {

            string firstName = TestHelpers.RandomTestName();
            string lastName = TestHelpers.RandomTestName();
            DateTime birthDate = TestHelpers.RandomBirthDate(21);
            string email = TestHelpers.RandomEmail();
            string phone = TestHelpers.RandomPhone();
            string photo = TestHelpers.RandomPhoto();


            string eventName = TestHelpers.RandomTestName();
            string descriptions = TestHelpers.RandomTestName();
            DateTime date = TestHelpers.RandomBirthDate(10);
            
            var userId = UserGateway.Create(firstName, lastName, birthDate, email);
            var eventId = EventGateway.Create(eventName, descriptions, date, userId);

            Event events = EventGateway.FindById(eventId);

            {
                Assert.That(events.EventName, Is.EqualTo(eventName));
                Assert.That(events.Descriptions, Is.EqualTo(descriptions));
                Assert.That(events.Dates, Is.EqualTo(date));
                Assert.That(events.EventId, Is.EqualTo(eventId));
                Assert.That(events.UserId, Is.EqualTo(userId));
            }

            {
                firstName = TestHelpers.RandomTestName();
                descriptions = TestHelpers.RandomTestName();
                date = TestHelpers.RandomBirthDate(2);
                EventGateway.Update(eventId, eventName, descriptions, date);
            }

            {
                events = EventGateway.FindById(eventId);
                Assert.That(events.EventName, Is.EqualTo(eventName));
                Assert.That(events.Descriptions, Is.EqualTo(descriptions));
                Assert.That(events.Dates, Is.EqualTo(date));
                Assert.That(events.EventId, Is.EqualTo(eventId));

            }

            {
                EventGateway.Delete(eventId);
                Assert.That(EventGateway.FindById(eventId), Is.Null);
            }

            {
                UserGateway.Delete(userId);
                Assert.That(UserGateway.FindById(userId), Is.Null);
            }
        }
    }
}
