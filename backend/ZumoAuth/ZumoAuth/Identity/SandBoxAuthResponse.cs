using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZumoAuth.Identity
{
    public class SandBoxAuthResponse
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public User User { get; set; }
    }
}