using ITI.KDO.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class FacebookClient
    {
        public string urlBase = "https://graph.facebook.com/v2.11/";

        public async Task<JObject> GetUserInformation(string facebookAccessToken)
        {
            //JObject jsonFile;
            JObject _json;
            using (HttpClient client = new HttpClient())
            {
                HttpRequestHeaders headers = client.DefaultRequestHeaders;
                headers.Add("Authorization", string.Format("Bearer {0}", facebookAccessToken));
                headers.Add("User-Agent", "KDO");
                HttpResponseMessage response = await client.GetAsync(urlBase + "me?fields=id,first_name,last_name");

                using (TextReader tr = new StreamReader(await response.Content.ReadAsStreamAsync()))
                using (JsonTextReader jsonReader = new JsonTextReader(tr))
                {
                    _json = JObject.Load(jsonReader);
                }
                return _json;
            }
        }


        public async Task<IEnumerable<FacebookContact>> GetFacebookFriends(string facebookAccessToken, int userId)
        {
            JObject jsonFile;
            using (HttpClient client = new HttpClient())
            {
                HttpRequestHeaders headers = client.DefaultRequestHeaders;
                headers.Add("Authorization", string.Format("Bearer {0}", facebookAccessToken));
                headers.Add("User-Agent", "KDO");
                HttpResponseMessage response = await client.GetAsync(urlBase + "me?fields=friends{id,first_name,last_name}");

                using (TextReader tr = new StreamReader(await response.Content.ReadAsStreamAsync()))
                using (JsonTextReader jsonReader = new JsonTextReader(tr))
                {
                    JToken json = JToken.Load(jsonReader);
                    jsonFile = json[json["friends"]].Value<JObject>();
                }
                string[] id = jsonFile["data"].Select(m => (string)m.SelectToken("id")).ToArray();
                string[] firstName = jsonFile["data"].Select(m => (string)m.SelectToken("id")).ToArray();
                string[] lastName = jsonFile["data"].Select(m => (string)m.SelectToken("id")).ToArray();

                List<FacebookContact> listFacebookContact = new List<FacebookContact>();
                for(int i = 0; i < id.Length; i++)
                {
                    FacebookContact facebookContact = new FacebookContact();
                    facebookContact.FacebookId = Int32.Parse(id[i]);
                    facebookContact.FirstName = firstName[i];
                    facebookContact.LastName = lastName[i];
                    facebookContact.UserId = userId;
                    facebookContact.Email = "N";
                    facebookContact.Phone = "0123456789";
                    facebookContact.BirthDate = DateTime.UtcNow.AddDays(new Random().Next(90));
                    listFacebookContact.Add(facebookContact);
                }
                return listFacebookContact;
            }
        }
    }
}
