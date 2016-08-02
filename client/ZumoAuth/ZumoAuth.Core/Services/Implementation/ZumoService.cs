using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace ZumoAuth.Core
{
    public class ZumoService : IZumoService
    {
        private const string MobileServiceClientUrl = "https://zumo-auth.azurewebsites.net/";
        private object locker = new object();

        public static MobileServiceClient MobileService { get; set; }

        public bool IsInitialized { get; private set; }

        public async Task InitializeAsync()
        {
            lock (locker)
            {
                if (IsInitialized)
                    return;

                IsInitialized = true;

                MobileService = new MobileServiceClient(MobileServiceClientUrl);
            }
        }

        public async Task<MobileServiceUser> LoginAsync(string username, string password)
        {
            if (!IsInitialized)
            {
                await InitializeAsync();
            }

            var credentials = new JObject();
            credentials["email"] = username;
            credentials["password"] = password;

            MobileServiceUser user = await MobileService.LoginAsync("Sandbox", credentials);
            return user;
        }
    }
}

