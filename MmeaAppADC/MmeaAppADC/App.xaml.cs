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
            if (!string.IsNullOrEmpty(Preferences.Get("UserId", "")))
            {
                MainPage = new NavigationPage(new HomeView());
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
