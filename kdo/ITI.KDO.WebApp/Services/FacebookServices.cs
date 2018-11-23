using ITI.KDO.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class FacebookServices
    {
        readonly FacebookClient _facebookClient;
        readonly UserGateway _userGateway;
        readonly FacebookContactGateway _facebookContactGateway;

        public FacebookServices(FacebookClient facebookClient, UserGateway userGateway, FacebookContactGateway facebookContactGateway)
        {
            _facebookClient = facebookClient;
            _userGateway = userGateway;
            _facebookContactGateway = facebookContactGateway;
        }

        public async Task<JObject> GetFacebookInformation(string accessToken)
        {
            return await _facebookClient.GetUserInformation(accessToken);
        }

        public async Task<Result<IEnumerable<FacebookContact>>> GetFacebookContacts(int userId)
        {
            User user = _userGateway.FindById(userId);
            if (user == null) return Result.Failure<IEnumerable<FacebookContact>>(Status.NotFound, "User not found.");
            if (user.FacebookAccessToken == string.Empty) return Result.Failure<IEnumerable<FacebookContact>>(Status.BadRequest, "This user has not facebook account.");
            if (user.FacebookAccessToken == null) return Result.Failure<IEnumerable<FacebookContact>>(Status.BadRequest, "FacebookAccessToken not found...");

            IEnumerable<FacebookContact> friends = await _facebookClient.GetFacebookFriends(user.FacebookAccessToken, userId);
            foreach(FacebookContact fbContact in friends)
            {
                CreateFacebookContact(fbContact);
            }
            return Result.Success(Status.Ok, friends);
        }

        public Result CreateFacebookContact(FacebookContact facebookContact)
        {
            if(_facebookContactGateway.FindByUserId(facebookContact.UserId) == null) return Result.Failure(Status.NotFound, "User not found.");
            FacebookContact fbContact = _facebookContactGateway.FindByFacebookId(facebookContact.FacebookId);
            if (fbContact != null && fbContact.FacebookId == facebookContact.FacebookId) return Result.Failure(Status.BadRequest, "This contact existed");
            if (!IsNameValid(facebookContact.FirstName)) return Result.Failure(Status.BadRequest, "Invalid first name.");
            if (!IsNameValid(facebookContact.LastName)) return Result.Failure(Status.BadRequest, "Invalid last name.");

            _facebookContactGateway.CreateFacebookContact(facebookContact.UserId, facebookContact.FacebookId, facebookContact.Email, facebookContact.FirstName, facebookContact.LastName, facebookContact.BirthDate, facebookContact.Phone);
            return Result.Success(Status.Ok);
        }

        public Result Delete(int facebookId)
        {
            if (_facebookContactGateway.FindByFacebookId(facebookId) == null) return Result.Failure(Status.NotFound, "Contact not found.");
            _facebookContactGateway.Delete(facebookId);
            return Result.Success(Status.Ok);
        }

        bool IsNameValid(string nameString) => !string.IsNullOrEmpty(nameString);

        bool IsPhoneTelValid(string phoneTel) => !Regex.IsMatch(phoneTel, @"^[a-zA-Z]+$");

        bool IsDateTimeValid(DateTime birthDate)
        {
            if ((birthDate >= (DateTime)SqlDateTime.MinValue) && (birthDate <= (DateTime)SqlDateTime.MaxValue))
            {
                return true;
            }
            return false;
        }
    }
}
