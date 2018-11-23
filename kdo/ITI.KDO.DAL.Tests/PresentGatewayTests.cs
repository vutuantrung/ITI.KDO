using NUnit.Framework;
using System;

namespace ITI.KDO.DAL.Tests
{
    [TestFixture]
    public class PresentGatewayTests
    {
        private static UserGateway _userGateway;
        private static PresentGateway _presentGateway;
        private static UserGateway UserGateway => _userGateway ?? (_userGateway = new UserGateway(TestHelpers.ConnectionString));
        private static PresentGateway PresentGateway => _presentGateway ?? (_presentGateway = new PresentGateway(TestHelpers.ConnectionString));
   
        [Test]
        public void can_create_find_update_and_delete_present()
        {
            string firstName = TestHelpers.RandomTestName();
            string lastName = TestHelpers.RandomTestName();
            DateTime birthDate = TestHelpers.RandomBirthDate(21);
            string email = TestHelpers.RandomEmail();
            string phone = TestHelpers.RandomPhone();
            string photo = TestHelpers.RandomPhoto();
            byte[] picture = TestHelpers.GetBytesArray(12);
            string presentName = TestHelpers.RandomPresentName();
            float price = TestHelpers.RandomPrice();
            string linkPresent = TestHelpers.RandomLink();
            int categoryPresentId = 0;

            var userId = UserGateway.Create(firstName, lastName, birthDate, email);

            var presentId = PresentGateway.AddToUser(presentName, price, linkPresent, picture, categoryPresentId, userId);
            Present present = PresentGateway.FindByPresentId(presentId);

            {
                Assert.That(present.PresentName, Is.EqualTo(presentName));
                Assert.That(present.UserId, Is.EqualTo(userId));
                Assert.That(present.Price, Is.EqualTo(price));
                Assert.That(present.LinkPresent, Is.EqualTo(linkPresent));
                Assert.That(present.CategoryPresentId, Is.EqualTo(categoryPresentId));
            }

            {
                categoryPresentId = 1;
                linkPresent = TestHelpers.RandomLink();
                presentName = TestHelpers.RandomPresentName();
                price = TestHelpers.RandomPrice();
                PresentGateway.Update(presentId, presentName, price, linkPresent, picture, categoryPresentId, userId);
       
            }

            {
                present = PresentGateway.FindByPresentId(presentId);
                Assert.That(present.LinkPresent, Is.EqualTo(linkPresent));
                Assert.That(present.PresentName, Is.EqualTo(presentName));
                Assert.That(present.Price, Is.EqualTo(price));
                Assert.That(present.CategoryPresentId, Is.EqualTo(categoryPresentId));
            }

            {
                PresentGateway.Delete(presentId);
                Assert.That(PresentGateway.FindByPresentId(presentId), Is.Null);   
            }

            {
                UserGateway.Delete(userId);
                Assert.That(UserGateway.FindById(userId), Is.Null);
            }
        }
    }
}
