using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Newtonsoft.Json.Linq;
using PropertyChanged;

namespace ZumoAuth.Core.ViewModels
{
    [ImplementPropertyChanged]
    public class FirstViewModel
        : MvxViewModel
    {

        private IUserInteractionService _userInteractionService;
        protected IUserInteractionService UserInteractionService
            => _userInteractionService ?? (_userInteractionService = Mvx.Resolve<IUserInteractionService>());

        private const string _applicationURL = @"https://zumo-auth.azurewebsites.net";
        private static MobileServiceClient _mobileServiceClient;
        private static MobileServiceUser _user;

        public static MobileServiceClient Client
        {
            get
            {
                return _mobileServiceClient ?? (_mobileServiceClient = new MobileServiceClient(_applicationURL));
            }
        }

        public static MobileServiceUser CurrentUser
        {
            get { return _user; }
            set { _user = value; }
        }

        public FirstViewModel()
        {
            AuthenticateCommand = new MvxAsyncCommand(AuthenticateCommandExecuteAsync);
        }


        public string Username { get; set; }

        public string Password { get; set; }

        public IMvxAsyncCommand AuthenticateCommand { get; }

        private async Task AuthenticateCommandExecuteAsync()
        {

            var jObject = JObject.FromObject(new
            {
                email = "sandbox@email.com",
                password = "sandbox"
            });

            var result = await Client.LoginAsync("Sandbox", jObject);

            if (result == null)
                return;

            if (string.IsNullOrWhiteSpace(result.MobileServiceAuthenticationToken))
            {
                UserInteractionService.ShowToast("Casa caiu!");
            }
            else
            {
                UserInteractionService.ShowToast("Autenticado...");
            }
        }
    }
}

