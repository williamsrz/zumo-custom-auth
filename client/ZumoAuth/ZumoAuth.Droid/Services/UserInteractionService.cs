using System;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using ZumoAuth.Core;

namespace ZumoAuth.Droid
{
    public class UserInteractionService : IUserInteractionService
    {
        internal static void Initialize()
            => Mvx.RegisterSingleton<IUserInteractionService>(new UserInteractionService());

        private UserInteractionService() { }

        private static Activity Activity => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

        public void ShowToast(string message)
            => Toast.MakeText(Activity, message, ToastLength.Short).Show();

        public Task<bool> ConfirmAsync(string message, string title)
        {
            var tcs = new TaskCompletionSource<bool>();

            MvxAndroidMainThreadDispatcher.Instance.RequestMainThreadAction(() =>
            {
                new AlertDialog.Builder(Activity)
                    .SetMessage(message)
                    .SetTitle(title)
                    .SetPositiveButton("Ok", (s, e) => tcs.SetResult(true))
                    .SetNegativeButton("Cancelar", (s, e) => tcs.SetResult(false))
                    .Show();
            });

            return tcs.Task;
        }
    }
}

