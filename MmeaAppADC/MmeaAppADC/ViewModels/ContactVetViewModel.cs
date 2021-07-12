using MmeaAppADC.Models;
using MmeaAppADC.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

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

        private ApplicationUser selectedVet;
        public ApplicationUser SelectedVet
        {
            get { return selectedVet; }
            set
            {
                if (selectedVet != value)
                {
                    selectedVet = value; OnPropertyChanged();
                }
            }
        }
        public Command SendMessageCommand { get; set; }


        public ContactVetViewModel()
        {
            _dbService = new DBservice();
            Vets = new ObservableCollection<ApplicationUser>();
            GetVets();
            SendMessageCommand = new Command(async () => await SendMessageAsync());
        }

        private async Task SendMessageAsync()
        {

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
