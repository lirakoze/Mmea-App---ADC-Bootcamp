using Acr.UserDialogs;
using MmeaAppADC.VetArea.Views;
using MmeaAppADC.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MmeaAppADC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Preferences.Clear();
            if (!string.IsNullOrEmpty(Preferences.Get("UserId", "")))
            {
                if (Preferences.Get("Type", "") == "Farmer")
                {
                    UserDialogs.Instance.HideLoading();
                    Application.Current.MainPage = new NavigationPage(new HomeView());
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    MainPage = new NavigationPage(new VetHomeView());
                }
            }
            else
            {
                MainPage = new NavigationPage(new LoginView());
            }


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
