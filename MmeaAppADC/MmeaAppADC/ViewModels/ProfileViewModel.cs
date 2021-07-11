using MmeaAppADC.Services;
using MmeaAppADC.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private string firstName;
        public string Firstname
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(); }
        }
        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; OnPropertyChanged(); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }
        private string type;
        private string county;
        public string County
        {
            get { return county; }
            set { county = value; OnPropertyChanged(); }
        }
        private string subCounty;
        public string SubCounty
        {
            get { return subCounty; }
            set { subCounty = value; OnPropertyChanged(); }
        }

        public string Type
        {
            get { return type; }
            set { type = value; OnPropertyChanged(); }
        }
        private string phoneno;
        public string PhoneNo
        {
            get { return phoneno; }
            set { phoneno = value; OnPropertyChanged(); }
        }

        public Command LogoutCommand { get; set; }
        private AuthService _auth;
        public ProfileViewModel()
        {
            LogoutCommand = new Command(() => LogoutAsync());
            _auth = new AuthService();
            Firstname = Preferences.Get("Firstname", "");
            Lastname = Preferences.Get("Lastname", "");
            Email = Preferences.Get("Email", "");
            PhoneNo = Preferences.Get("PhoneNo", "");
            County = Preferences.Get("County", "");
            SubCounty = Preferences.Get("SubCounty", "");
            Type = Preferences.Get("Type", "");
        }

        private void LogoutAsync()
        {
            _auth.LogoutUser();
            Application.Current.MainPage = new LoginView();
        }
    }
}
