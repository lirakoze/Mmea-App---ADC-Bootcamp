using MmeaAppADC.Models;
using MmeaAppADC.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace MmeaAppADC.ViewModels
{
    public class ContactVetViewModel : BaseViewModel
    {
        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; OnPropertyChanged(); }
        }


        private ObservableCollection<ApplicationUser> vets;
        private DBservice _dbService;
        public ObservableCollection<ApplicationUser> Vets
        {
            get { return vets; }
            set { vets = value; OnPropertyChanged(); }
        }

        public ContactVetViewModel()
        {
            var list = new ObservableCollection<ApplicationUser>()
            {
                new ApplicationUser
                {
                    FirstName="Joseph",LastName="Omondi", County="Nairobi", SubCounty="Umoja", PhoneNo="071232416"
                },
                new ApplicationUser
                {
                    FirstName="Rayan",LastName="Person", County="Nairobi", SubCounty="Embakasi", PhoneNo="071232416"
                },new ApplicationUser
                {
                    FirstName="Joseph",LastName="Omondi", County="Nairobi", SubCounty="Umoja", PhoneNo="071232416"
                },
                new ApplicationUser
                {
                    FirstName="Rayan",LastName="Person", County="Nairobi", SubCounty="Embakasi", PhoneNo="071232416"
                },new ApplicationUser
                {
                    FirstName="Joseph",LastName="Omondi", County="Nairobi", SubCounty="Umoja", PhoneNo="071232416"
                },
                new ApplicationUser
                {
                    FirstName="Rayan",LastName="Person", County="Nairobi", SubCounty="Embakasi", PhoneNo="071232416"
                }
            };
            _dbService = new DBservice();
            Vets = new ObservableCollection<ApplicationUser>();
            isVisible = false;
            GetVets();
        }

        private async void GetVets()
        {
            var subCounty = Preferences.Get("SubCounty", "");
            List<ApplicationUser> list = await _dbService.GetAgroVets(subCounty);
            if (list.Count == 0)
            {
                isVisible = true;
                return;
            }

            foreach (var user in list)
            {
                isVisible = false;
                Vets.Add(user);
            }
        }


    }
}
