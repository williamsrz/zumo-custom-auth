using System;
using System.Threading.Tasks;

namespace ZumoAuth.Core
{
    public interface IUserInteractionService
    {
        void ShowToast(string message);

        Task<bool> ConfirmAsync(string message, string title = "");
    }
}

