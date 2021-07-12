using MmeaAppADC.Models;
using MmeaAppADC.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace MmeaAppADC.ViewModels
{
    public class ContactVetViewModel : BaseViewModel
    {
        public DBservice _dbService;
        private ObservableCollection<ApplicationUser> vets;
        public ObservableCollection<ApplicationUser> Vets
        {
            get { return vets; }
            set { vets = value; OnPropertyChanged(); }
        }

        public ContactVetViewModel()
        {
            _dbService = new DBservice();
            Vets = new ObservableCollection<ApplicationUser>();
            GetVets();
        }

        private async void GetVets()
        {
            var subCounty = Preferences.Get("SubCounty", "");
            List<ApplicationUser> list = await _dbService.GetAgroVets(subCounty);
            foreach (var user in list)
            {
                Vets.Add(user);
            }
        }


    }
}
