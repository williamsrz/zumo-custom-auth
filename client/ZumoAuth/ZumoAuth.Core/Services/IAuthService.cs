using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace ZumoAuth.Core
{
    public interface IAuthService
    {
        Task<AccountResponse> LoginAsync(string username, string password);

        Task LogoutAsync();
    }
}

