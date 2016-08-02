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
        private string TokenSigningKey => Environment.GetEnvironmentVariable(AuthSigningKeyVariableName) ?? ConfigurationManager.AppSettings["SigningKey"];
        public string WebsiteHostName => Environment.GetEnvironmentVariable(HostNameVariableName) ?? "localhost";
       
        public async Task<IHttpActionResult> Post([FromBody] JObject authCredentials)
        {
            SandBoxAuthResponse authResult = AuthenticateCredentials(authCredentials.ToObject<SandboxAuthCredentials>());

            if (!authResult.Success)
            {
                return Unauthorized();
            }

            var token = GetJwtSecurityToken(authResult.User);
           
            return Ok(new LoginResult { RawToken = token.RawData, User = authResult.User });
        }

        private JwtSecurityToken GetJwtSecurityToken(User user) 
        {
            IEnumerable<Claim> claims = GetAccountClaims(user);
            string websiteUri = $"https://{WebsiteHostName}/";

            return AppServiceLoginHandler
                .CreateToken(claims, TokenSigningKey, websiteUri, websiteUri, TimeSpan.FromDays(30));
        }
        
        private SandBoxAuthResponse AuthenticateCredentials(SandboxAuthCredentials credentials)
        {
            //validate user against db, or service here

            var user = new User { UserId = Guid.NewGuid().ToString(), Email = "sandbox@email.com", FirstName = "Sandbox", LastName = "User" };

            var sucess = (credentials.Email == user.Email && credentials.Password == user.Password); //dummy validation

            var authResponse = new SandBoxAuthResponse { User = user, Success = sucess };

            return authResponse;
        }

        private IEnumerable<Claim> GetAccountClaims(User user) => new Claim[]
         {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.NameId, user.Email)
         };
    }
}

