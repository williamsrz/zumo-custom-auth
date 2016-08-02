using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace ZumoAuth.Core
{
    public interface IZumoService
    {
        bool IsInitialized { get; }

        Task InitializeAsync();

        Task<MobileServiceUser> LoginAsync(string username, string password);
    }
}