using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CeseatUserManagement.UserManagementSpace
{
    interface IUser : IObservable
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
