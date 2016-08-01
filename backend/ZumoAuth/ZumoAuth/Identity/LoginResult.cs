using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZumoAuth.Identity
{
    public sealed class LoginResult
    {
        [JsonProperty("authenticationToken")]
        public string RawToken { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}