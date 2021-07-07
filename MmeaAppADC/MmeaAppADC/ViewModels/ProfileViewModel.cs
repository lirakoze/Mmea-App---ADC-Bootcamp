using MmeaAppADC.Services;
using MmeaAppADC.Views;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public Command LogoutCommand { get; set; }
        private AuthService _auth;
        public ProfileViewModel()
        {
            LogoutCommand = new Command(() => LogoutAsync());
            _auth = new AuthService();
        }

        private void LogoutAsync()
        {
            _auth.LogoutUser();
            Application.Current.MainPage = new LoginView();
        }
    }
}
