using Acr.UserDialogs;
using MmeaAppADC.Models;
using MmeaAppADC.Services;
using MmeaAppADC.Views;
using System;
using System.Collections.ObjectModel;
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

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; OnPropertyChanged(); }
        }
        public string PhoneNo
        {
            get { return phoneno; }
            set { phoneno = value; OnPropertyChanged(); }
        }
        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; OnPropertyChanged(); }
        }


        public ObservableCollection<County> Counties { get; set; }
        public ObservableCollection<SubCounty> SubCounties { get; set; }
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
                    _ = GetSubCounties();
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
            Counties = new ObservableCollection<County>();
            SubCounties = new ObservableCollection<SubCounty>();
            IsVisible = false;
            IsChecked = false;
            GetCounties();
        }

        private async Task RegisterAsync()
        {
            UserDialogs.Instance.ShowLoading("Loading...");
            try
            {
                var isvalid = isInputsValid();
                if (isvalid)
                {
                    if (!Password.Equals(ConfirmPassword) || password == null || confirmPassword == null)
                    {
                        UserDialogs.Instance.HideLoading();
                        UserDialogs.Instance.Alert("Password don't match. try again", "Registration", "Okay");
                        return;
                    }
                    else
                    {
                        var user = new ApplicationUser
                        {
                            FirstName = Firstname,
                            LastName = Lastname,
                            Email = Email,
                            PhoneNo = PhoneNo,
                            County = SelectedCounty.Name,
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
                }
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Please, Fill in the required information", "Error", "Okay");
                return;

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Please, Provide the required Information", "Registration", "Okay");
                Console.WriteLine("Error", ex.Message);

            }

        }

        private void LoginAsync()
        {
            Application.Current.MainPage = new NavigationPage(new LoginView());
        }
        private async void GetCounties()
        {
            var list = await _dbService.GetCounties();
            foreach (var county in list)
            {
                Counties.Add(county);
            }
        }
        private async Task GetSubCounties()
        {
            var county = await _dbService.GetCounty(SelectedCounty.Name);
            if (county.SubCounties.Count == 0)
            {
                IsVisible = false;
                SubCounties.Clear();
                return;
            }

            IsVisible = true;
            SubCounties.Clear();
            foreach (var sub in county.SubCounties)
            {
                SubCounties.Add(sub);
            }

        }

        private bool isInputsValid()
        {
            if (Firstname == null || Lastname == null)
                return false;
            if (Email == null || PhoneNo == null)
                return false;
            if (SelectedCounty == null || SubCounty == null)
                return false;
            if (Password == null || ConfirmPassword == null)
                return false;
            else
                return true;
        }
    }
}