using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace CeseatUserManagement.LoginSpace
{
    class LoginService
    {
        Uri baseApiAddress;
        public LoginService()
        {
            this.baseApiAddress = new Uri("http://172.16.44.43:3000/");
        }

        public async Task login(string email, string password)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://172.16.44.43:3000/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"email\":\"" + email + "\"," +
                              "\"password\":\"" + password + "\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Dictionary<object, object> responseDict = JsonConvert.DeserializeObject<Dictionary<object, object>>(result);
                if (!String.IsNullOrEmpty(responseDict["token"].ToString()))
                {
                    AccesToken.GetInstance().AccesTokenValue = responseDict["token"].ToString();
                }
            }
        }
    }
}
