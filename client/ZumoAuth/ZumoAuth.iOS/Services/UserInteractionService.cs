using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MvvmCross.Platform.iOS.Views;
using UIKit;
using ToastIOS;
using ZumoAuth.Core;

namespace ZumoAuth.iOS
{
    public class UserInteractionService : IUserInteractionService
    {
        internal static void Initialize()
            => Mvx.RegisterSingleton<IUserInteractionService>(new UserInteractionService());

        private UserInteractionService() { }

        public void ShowToast(string message)
            => Toast.MakeText(message, Toast.LENGTH_LONG).Show();

        public Task<bool> ConfirmAsync(string message, string title = "")
        {
            var tcs = new TaskCompletionSource<bool>();

            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, action => tcs.SetResult(true)));
            alert.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Default, action => tcs.SetResult(false)));

            var modalHost = Mvx.Resolve<IMvxIosModalHost>();
            modalHost.PresentModalViewController(alert, true);

            return tcs.Task;
        }
    }
}

