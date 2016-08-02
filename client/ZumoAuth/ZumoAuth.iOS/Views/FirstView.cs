using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using ZumoAuth.Core.ViewModels;

namespace ZumoAuth.iOS.Views
{
    [MvxFromStoryboard]
    public partial class FirstView : MvxViewController
    {
        public FirstView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(EmailTextField).To(vm => vm.Username);
            set.Bind(PasswordTextField).To(vm => vm.Password);
            set.Bind(AuthenticateButton).For("Tap").To(vm => vm.AuthenticateCommand);
            set.Apply();
        }
    }
}
