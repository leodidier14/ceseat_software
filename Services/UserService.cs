using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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

        public async Task<List<IUser>> getUsers()
        {
            List<IUser> _users = null;
            _users = this.users;

            //using (var httpClient = new HttpClient { BaseAddress = this.baseApiAddress })
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token");

            //    HttpResponseMessage response = await httpClient.GetAsync("users/");

            //    if (response.IsSuccessStatusCode)
            //    {
            //        string usersString = await response.Content.ReadAsStringAsync();

            //        _users = JsonConvert.DeserializeObject<List<IUser>>(usersString);
            //    }
            //}

            return _users;
        }

        public async Task<HttpStatusCode> deleteUser(IUser user)
        {
            using (var httpClient = new HttpClient { BaseAddress = this.baseApiAddress })
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"api/users/{user.Id}");
                return response.StatusCode;
            }
        }

        public async void updateUser(IUser user)
        {
            Console.WriteLine(user.Role);
        }
    }
}
