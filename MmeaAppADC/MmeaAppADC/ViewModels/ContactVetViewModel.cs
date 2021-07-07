using MmeaAppADC.Models;
using System.Collections.ObjectModel;

namespace MmeaAppADC.ViewModels
{
    public class ContactVetViewModel : BaseViewModel
    {
        private ObservableCollection<ApplicationUser> vets;

        public ObservableCollection<ApplicationUser> Vets
        {
            get { return vets; }
            set { vets = value; OnPropertyChanged(); }
        }

        public ContactVetViewModel()
        {
            Vets = new ObservableCollection<ApplicationUser>()
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
        }
    }
}
