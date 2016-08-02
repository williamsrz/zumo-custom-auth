using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace ZumoAuth.Core
{
    public class AuthService : IAuthService
    {
        private readonly IZumoService _zumoService;

        public AuthService(IZumoService zumoService)
        {
            _zumoService = zumoService;
        }

        public async Task<AccountResponse> LoginAsync(string username, string password)
        {
            MobileServiceUser user = await _zumoService.LoginAsync(username, password);

            return AccountFromMobileServiceUser(user);
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        private AccountResponse AccountFromMobileServiceUser(MobileServiceUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            IDictionary<string, string> claims = JwtUtility.GetClaims(user.MobileServiceAuthenticationToken);

            var account = new AccountResponse();
            account.Success = true;
            account.User = new User
            {
                Email = claims[JwtClaimNames.Subject],
                FirstName = claims[JwtClaimNames.GivenName],
                LastName = claims[JwtClaimNames.FamilyName]
            };

            return account;
        }
    }
}

