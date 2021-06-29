using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CeseatUserManagement.LoginSpace
{
    class LoginService
    {
        Uri baseApiAddress;
        public LoginService()
        {
            this.baseApiAddress = new Uri("https://localhost:3030/");
        }

        public async Task<HttpStatusCode> login(string email, string password)
        {
            using (var httpClient = new HttpClient { BaseAddress = this.baseApiAddress })
            {
                var values = new Dictionary<string, string>
                {
                    { "email", email },
                    { "password", password }
                };

                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = await httpClient.PostAsync("users", content);

                var responseString = await response.Content.ReadAsStringAsync();
                
                return response.StatusCode;
            }
        }
    }
}
