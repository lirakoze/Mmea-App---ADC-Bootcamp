using Acr.UserDialogs;
using MmeaAppADC.Models;
using MmeaAppADC.Services;
using MmeaAppADC.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; OnPropertyChanged(); }
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
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }
        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; OnPropertyChanged(); }
        }
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
        private string type;
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

        public IList<County> Counties { get; set; }

        private County selectedCounty;
        public County SelectedCounty
        {
            get
            {
                return selectedCounty;
            }
            set
            {
                if (selectedCounty != value)
                {
                    selectedCounty = value; OnPropertyChanged();
                }
            }
        }

        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }
        private AuthService _authService;
        private DBservice _dbService;
        public RegisterViewModel()
        {
            LoginCommand = new Command(() => LoginAsync());
            RegisterCommand = new Command(async () => await RegisterAsync());
            _authService = new AuthService();
            _dbService = new DBservice();

            List<County> list = new List<County> {
            new County{Name="Bungoma"},
            new County{Name="Busia"}
            };

            Counties = list;
            // Counties = _dbService.GetCounties().Result;

            //GetCounties();
        }

        private async Task RegisterAsync()
        {
            UserDialogs.Instance.ShowLoading("Loading...");
            try
            {
                if (!Password.Equals(ConfirmPassword) || password == null || confirmPassword == null)
                {
                    //await Application.Current.MainPage.DisplayAlert("Register", "Password don't match. try again", "Okay");
                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.Alert("Password don't match. try again", "Registration", "Okay");
                    return;
                }

                var user = new ApplicationUser
                {
                    FirstName = Firstname,
                    LastName = Lastname,
                    Email = Email,
                    PhoneNo = PhoneNo,
                    County = County,
                    SubCounty = SubCounty,
                    Type = Type
                };
                //Auth service
                await _authService.RegisterUser(user, Password);
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Registration is successful", "Registration", "Okay");
                //Navigate to login
                //await Application.Current.MainPage.Navigation.PushAsync(new LoginView());
                Application.Current.MainPage = new NavigationPage(new LoginView());
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Please, Provide the required Information", "Registration", "Okay");

            }

        }

        private void LoginAsync()
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new LoginView());
            Application.Current.MainPage = new NavigationPage(new LoginView());
        }
        private async Task GetCounties()
        {
        }
    }
}