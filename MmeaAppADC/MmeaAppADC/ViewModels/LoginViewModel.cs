using Acr.UserDialogs;
using MmeaAppADC.Services;
using MmeaAppADC.VetArea.Views;
using MmeaAppADC.Views;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public AuthService _authService;
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await RegisterAsync());
            _authService = new AuthService();
        }

        private async Task RegisterAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterView());
        }

        //Method to Login Users
        private async Task LoginAsync()
        {
            UserDialogs.Instance.ShowLoading("Loading...");
            //Auth Service
            var isvalid = await _authService.LoginUser(Username, Password);
            if (isvalid)
            {
                if (Preferences.Get("Type", "") == "Farmer")
                {
                    UserDialogs.Instance.HideLoading();
                    Application.Current.MainPage = new NavigationPage(new HomeView());
                }
                //await Application.Current.MainPage.Navigation.PushAsync(new HomeView());
                UserDialogs.Instance.HideLoading();
                Application.Current.MainPage = new NavigationPage(new VetHomeView());
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await Application.Current.MainPage.DisplayAlert("Failed", "Incorrect Username or Password", "Ok");
                return;
                //await Application.Current.MainPage.Navigation.PushAsync(new HomeView());

                //Application.Current.MainPage = new NavigationPage(new HomeView());
            }


            //Navigate to Home page
        }
    }
}
