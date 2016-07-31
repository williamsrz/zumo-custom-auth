using System;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;
using ZumoAuth.Core;

namespace ZumoAuth.Droid
{
    public class ZumoClientService : IZumoClientService
    {
        internal static void Initialize()
           => Mvx.RegisterSingleton<IZumoClientService>(new ZumoClientService());

        public void Setup()
        {
            CurrentPlatform.Init();
        }
    }
}

