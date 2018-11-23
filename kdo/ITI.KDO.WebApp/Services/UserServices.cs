using ITI.KDO.DAL;
using ITI.KDO.WebApp.Models.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class UserServices
    {
        readonly UserGateway _userGateway;
        readonly PasswordHasher _passwordHasher;

        public UserServices(UserGateway userGateway, PasswordHasher passwordHasher)
        {
            _userGateway = userGateway;
            _passwordHasher = passwordHasher;
        }

        public bool CreatePasswordUser(RegisterViewModel model)
        {
            if (_userGateway.FindByEmail(model.Email) != null) return false;
            _userGateway.CreateUserWithPassword(model.Email, model.FirstName, model.LastName, _passwordHasher.HashPassword(model.Password));

            return true;
        }
        
        public IEnumerable<string> GetAuthenticationProviders(string userId)
        {
            return _userGateway.GetAuthenticationProviders(userId);
        }

        public User FindUserPasswordHashed(string email)
        {
            User user = _userGateway.FindByEmail(email);
            user.Password = _userGateway.FindUserPasswordHashed(user.UserId).Password;
            return user;
        }

        public bool VerifyPasswordUser(int userId)
        {
            int userIdVerified = 0;
            userIdVerified = _userGateway.FindPasswordUserId(userId);
            if(userIdVerified == 0) { return false; }
            return true;
        }

        public User FindUser(string email, string password)
        {
            User user = _userGateway.FindByEmail(email);
            if (user != null)
            {
                user.Password = _userGateway.FindUserPasswordHashed(user.UserId).Password;
                if(_passwordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Success)
                    return user;
            } 
            return null;
        }

        public User FindUser(string email)
        {
            return _userGateway.FindByEmail(email);
        }

        public User FindUserByEmail(string email) => _userGateway.FindByEmail(email);

        public User FindUserById(int userId) => _userGateway.FindById(userId);

        public Result<int> Delete(int userId)
        {
            if (_userGateway.FindById(userId) == null) return Result.Failure<int>(Status.BadRequest, "User not found.");
            _userGateway.Delete(userId);
            return Result.Success(Status.Ok, userId);
        }

        public Result<IEnumerable<User>> GetAll()
        {
            return Result.Success(Status.Ok, _userGateway.GetAll());
        }

        public Result<User> CreateUser(string firstName, string lastName, string email, DateTime birthdate)
        {
            if (!IsNameValid(firstName)) return Result.Failure<User>(Status.BadRequest, "The first name is invalid.");
            if (!IsNameValid(lastName)) return Result.Failure<User>(Status.BadRequest, "The last name is invalid.");
            if (!IsNameValid(email)) return Result.Failure<User>(Status.BadRequest, "The email is invalid.");
            if (!IsDateTimeValid(birthdate)) return Result.Failure<User>(Status.BadRequest, "The birthdate is invalid.");

            _userGateway.Create(firstName, lastName, birthdate, email);
            User user = _userGateway.FindByEmail(email);
            return Result.Success(Status.Ok, user);
        }

        public Result<User> CreatePasswordIdUser(ModifyPasswordViewModel model)
        {
            User user = _userGateway.FindByEmail(model.Email);
            _userGateway.CreatePasswordIdUser(user.UserId, _passwordHasher.HashPassword(model.OldPassword));
            user = _userGateway.FindByEmail(model.Email);
            return Result.Success(Status.Ok, user);
        }

        public Result<User> UpdateUser(int userId, string firstName, string lastName, string email, string phone, byte[] photo)
        {
            if (!IsNameValid(firstName)) return Result.Failure<User>(Status.BadRequest, "The first name is invalid.");
            if (!IsNameValid(lastName)) return Result.Failure<User>(Status.BadRequest, "The last name is invalid.");
            if (!IsNameValid(email)) return Result.Failure<User>(Status.BadRequest, "The email is invalid.");
           
            if (phone != null && !IsPhoneTelValid(phone)) return Result.Failure<User>(Status.BadRequest, "The phone number is invalid.");

            _userGateway.Update(userId, firstName, lastName, email, phone, photo);
            User user = _userGateway.FindById(userId);
            return Result.Success(Status.Ok, user);
        }

        public Result<User> UpdateUserPassword(int userId, string password)
        {
            _userGateway.UpdatePassword(userId, _passwordHasher.HashPassword(password));
            User user = _userGateway.FindById(userId);
            return Result.Success(Status.Ok, user);
        }

        bool IsNameValid(string nameString) => !string.IsNullOrEmpty(nameString);

        bool IsPhoneTelValid(string phoneTel) => !Regex.IsMatch(phoneTel, @"^[a-zA-Z]+$");

        bool IsPhotoValid(string photo) => !string.IsNullOrEmpty(photo);

        bool IsDateTimeValid(DateTime birthDate)
        {
            if((birthDate >= (DateTime)SqlDateTime.MinValue) && (birthDate <= (DateTime)SqlDateTime.MaxValue))
            {
                return true;
            }
            return false;
        }

        public bool CreateOrUpdateGoogleUser(string email, string googleId, string refreshToken, JObject userInformation)
        {
            User user = _userGateway.FindByEmail(email);
            if (user == null)
            {
                _userGateway.CreateGoogleUser(
                    email, 
                    googleId, 
                    refreshToken, 
                    userInformation["name"]["givenName"].ToString(), 
                    userInformation["name"]["familyName"].ToString()
                    );
                return true;
            }
            if (user.GoogleRefreshToken == string.Empty)
            {
                _userGateway.AddGoogleToken(user.UserId, refreshToken);
            }
            else
            {
                _userGateway.UpdateGoogleToken(user.UserId, refreshToken);
            }
            return false;
        }

        public bool CreateOrUpdateFacebookUser(string email, string facebookId, string accessToken, JObject userInfo)
        {
            User user = _userGateway.FindByEmail(email);
            if(user == null)
            {
                _userGateway.CreateFacebookUser(email, facebookId, accessToken, userInfo["first_name"].ToString(), userInfo["last_name"].ToString());
                return true;
            }
            if(user.FacebookAccessToken == string.Empty)
            {
                _userGateway.AddFacebookToken(user.UserId, facebookId, accessToken);
            }
            else
            {
                _userGateway.UpdateFacebookToken(user.UserId, user.FacebookId, accessToken);
            }
            return false;
        }
    }
}