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
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ZumoAuth.Identity;
using System.Collections.Generic;

namespace ZumoAuth.Controllers
{
    public class SandboxAuthController : ApiController
    {
        private const string AuthSigningKeyVariableName = "WEBSITE_AUTH_SIGNING_KEY";
        private const string HostNameVariableName = "WEBSITE_HOSTNAME";

        public string WebsiteHostName => Environment.GetEnvironmentVariable(HostNameVariableName) ?? "localhost";
        private string TokenSigningKey => Environment.GetEnvironmentVariable(AuthSigningKeyVariableName) ?? ConfigurationManager.AppSettings["SigningKey"];

        public async Task<IHttpActionResult> Post([FromBody] JObject assertion)
        {
            SandBoxAuthResponse authResult = AuthenticateCredentials(assertion.ToObject<SandboxCredentials>());

            if (!authResult.Success)
            {
                return Unauthorized();
            }

            IEnumerable<Claim> claims = GetAccountClaims(authResult.User);
            string websiteUri = $"https://{WebsiteHostName}/";

            JwtSecurityToken token = AppServiceLoginHandler
                .CreateToken(claims, TokenSigningKey, websiteUri, websiteUri, TimeSpan.FromDays(30));

            return Ok(new LoginResult { RawToken = token.RawData, User = authResult.User });
        }

        private SandBoxAuthResponse AuthenticateCredentials(SandboxCredentials credentials)
        {
            //validate user against db, or service here

            var user = new User { UserId = Guid.NewGuid().ToString(), Email = "sandbox@email.com", FirstName = "Sandbox", LastName = "User" };
            var sucess = (credentials.Email == user.Email); //dummy validation

            var authResponse = new SandBoxAuthResponse { User = user, Success = sucess };

            return authResponse;
        }

        private IEnumerable<Claim> GetAccountClaims(User user) => new Claim[]
         {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName)
         };
    }
}

