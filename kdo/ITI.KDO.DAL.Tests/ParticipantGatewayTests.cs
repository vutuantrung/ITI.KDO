using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL.Tests
{
    [TestFixture]
    public class ParticipantGatewayTests
    {
        private static EventGateway _eventGateway;
        private static UserGateway _userGateway;
        private static ParticipantGateway _participantGateway;
        private static EventGateway EventGateway => _eventGateway ?? (_eventGateway = new EventGateway(TestHelpers.ConnectionString));
        private static UserGateway UserGateway => _userGateway ?? (_userGateway = new UserGateway(TestHelpers.ConnectionString));
        private static ParticipantGateway ParticipantGateway => _participantGateway ?? (_participantGateway = new ParticipantGateway(TestHelpers.ConnectionString));

        [Test]
        public void can_create_find_and_delete_Participant()
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

            var user1 = UserGateway.Create(firstName, lastName, birthDate, email);
            var user2 = UserGateway.Create(firstName, lastName, birthDate, email);

            var eventId = EventGateway.Create(eventName, descriptions, date, user1);

            ParticipantGateway.Create(user1, eventId, false, true);
            ParticipantGateway.Create(user2, eventId, true, false);

           

            {
                Participant participant = ParticipantGateway.FindByIds(user2, eventId);

                Assert.That(participant.UserId, Is.EqualTo(user2));
                Assert.That(participant.EventId, Is.EqualTo(eventId));
                Assert.That(participant.ParticipantType, Is.EqualTo(true));
            }

            {
                ParticipantGateway.SetEventInvitaion(user2, eventId);
                Participant participant = ParticipantGateway.FindByIds(user2, eventId);
                Assert.That(participant.Invitation, Is.EqualTo(true));
            }

            {
                ParticipantGateway.Delete(user2, eventId);
                Assert.That(ParticipantGateway.FindByIds(user2, eventId), Is.Null);
                ParticipantGateway.Delete(user1, eventId);
                Assert.That(ParticipantGateway.FindByIds(user1, eventId), Is.Null);
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
            }
        }
    }
}
