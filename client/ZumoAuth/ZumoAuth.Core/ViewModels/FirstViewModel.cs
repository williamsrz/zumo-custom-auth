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

        private readonly IAuthService _authService;


        public FirstViewModel(IAuthService authService)
        {
            _authService = authService;

            AuthenticateCommand = new MvxAsyncCommand(AuthenticateCommandExecuteAsync);
        }


        public string Username { get; set; }

        public string Password { get; set; }

        public IMvxAsyncCommand AuthenticateCommand { get; }

        private async Task AuthenticateCommandExecuteAsync()
        {

            AccountResponse accountResponse = await _authService.LoginAsync(Username, Password);

            if (accountResponse == null)
                return;

            if (!accountResponse.Success)
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

