using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Net.Http;
using ZumoAuth.DataObjects;
using System.IdentityModel.Tokens;
using System.Net;
using System;
using System.Security.Claims;
using Microsoft.Azure.Mobile.Server.Login;
using System.Configuration;

namespace ZumoAuth.Controllers
{
    public class SandboxAuthController : ApiController
    {
        public async Task<IHttpActionResult> Post([FromBody] JObject assertion)
        {

        }
    }
}

