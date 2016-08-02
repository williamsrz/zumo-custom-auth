using System;
using Newtonsoft.Json;

namespace ZumoAuth.Core
{
    public class AccountResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
    }
}

