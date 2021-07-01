using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;

namespace CeseatUserManagement.UserManagementSpace
{
    class UserService
    {
        private List<IUser> users;
        Uri baseApiAddress;
        public UserService()
        {
            this.baseApiAddress = new Uri("http://localhost:3000/");

            this.users = new List<IUser>();
            users.Add(new User("1", "dylan.lafarge@viacesi.fr", "Lafarge", "Dylan", "Client"));
            users.Add(new User("2", "dylan.lafarge@viacesi.fr", "Didier", "Léo", "Restaurant"));
            users.Add(new User("3", "dylan.lafarge@viacesi.fr", "Kauffmann", "Romain", "Technicien"));
            users.Add(new User("4", "dylan.lafarge@viacesi.fr", "Doignon", "Guillaume", "Commercial"));
        }

        public async Task<List<User>> getUsers()
        {
            List<User> _users = new List<User>();
            //_users = this.users;

            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/app/user");
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "GET";
            //httpWebRequest.PreAuthenticate = true;
            //httpWebRequest.Headers.Add("Authorization", "Bearer " + AccesToken.GetInstance().AccesTokenValue);
            //httpWebRequest.Accept = "application/json";

            //LoginResponse responseObject;

            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    string json = "{\"email\":\"" + email + "\"," +
            //                  "\"password\":\"" + password + "\"}";

            //    streamWriter.Write(json);
            //}

            //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    var result = streamReader.ReadToEnd();
            //    _users = JsonConvert.DeserializeObject<List<IUser>>(result);
            //    if(_users != null && _users.Count > 0)
            //    {
            //        IUser user = _users[0];
            //    }
            //}

            using (var httpClient = new HttpClient { BaseAddress = this.baseApiAddress })
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccesToken.GetInstance().AccesTokenValue);

                HttpResponseMessage response = await httpClient.GetAsync("app/user");

                if (response.IsSuccessStatusCode)
                {
                    string usersString = await response.Content.ReadAsStringAsync();

                    _users = JsonConvert.DeserializeObject<List<User>>(usersString);
                }
            }

            return _users;
            //return this.users;
        }

        public async Task<HttpStatusCode> deleteUser(IUser user)
        {
            using (var httpClient = new HttpClient { BaseAddress = this.baseApiAddress })
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccesToken.GetInstance().AccesTokenValue);

                HttpResponseMessage response = await httpClient.DeleteAsync($"user/{user.Id}");
                return response.StatusCode;
            }
        }

        public async void updateUser(IUser user)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/app/user/" + user.Id);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";
            httpWebRequest.PreAuthenticate = true;
            httpWebRequest.Headers.Add("Authorization", "Bearer " + AccesToken.GetInstance().AccesTokenValue);

            //LoginResponse responseObject;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"userType\":\"" + user.Role + "\"," +
                              "\"isSuspended\":\"" + user.IsSuspended + "\"}";

                streamWriter.Write(json);
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            }
            catch
            {

            }
        }
    }
}
