using System;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;
using ZumoAuth.Core;

namespace ZumoAuth.Droid
{
    public class AzureMobileAppClientService : IAzureMobileAppClientService
    {
        internal static void Initialize()
           => Mvx.RegisterSingleton<IAzureMobileAppClientService>(new AzureMobileAppClientService());

        public void Setup()
        {
            CurrentPlatform.Init();
        }
    }
}

