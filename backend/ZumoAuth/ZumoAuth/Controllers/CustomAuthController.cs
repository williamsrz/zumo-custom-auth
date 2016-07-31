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
    [MobileAppController]
    public class CustomAuthController : ApiController
    {
        public HttpResponseMessage Post(AuthInfo authInfo)
        {
            // return error if password is not correct
            if (!this.IsPasswordValid(authInfo.UserName, authInfo.Password))
            {
                return this.Request.CreateUnauthorizedResponse();
            }

            JwtSecurityToken token = this.GetAuthenticationTokenForUser(authInfo.UserName);

            return this.Request.CreateResponse(HttpStatusCode.OK, new
            {
                Token = token.RawData,
                Username = authInfo.UserName
            });
        }
        private JwtSecurityToken GetAuthenticationTokenForUser(string username)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username)
            };

            var signingKey = this.GetSigningKey();
            var audience = this.GetSiteUrl(); // audience must match the url of the site
            var issuer = this.GetSiteUrl(); // audience must match the url of the site 

            JwtSecurityToken token = AppServiceLoginHandler.CreateToken(
                claims,
                signingKey,
                audience,
                issuer,
                TimeSpan.FromHours(24)
                );

            return token;
        }

        private bool IsPasswordValid(string username, string password)
        {
            return (username == "radek" && password == "madero");
        }

        private string GetSiteUrl()
        {
            var settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                return "http://localhost";
            }
            else
            {
                return "https://" + settings.HostName + "/";
            }
        }

        private string GetSigningKey()
        {
            var settings = this.Configuration.GetMobileAppSettingsProvider()
                .GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // this key is for debuggint and testing purposes only
                // this key should match the one supplied in Startup.MobileApp.cs
                return ConfigurationManager.AppSettings["SigningKey"];
            }
            else
            {
                var key = Environment.GetEnvironmentVariable("WEBSITE_AUTH_SIGNING_KEY");

                if (string.IsNullOrWhiteSpace(key))
                {
                    key = ConfigurationManager.AppSettings["SigningKey"];
                }

                return key;
            }
        }
    }
}
