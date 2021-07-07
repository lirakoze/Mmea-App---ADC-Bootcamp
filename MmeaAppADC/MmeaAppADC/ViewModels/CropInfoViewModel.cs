using MmeaAppADC.Models;
using MmeaAppADC.Views;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class CropInfoViewModel : BaseViewModel
    {
        private string cropTitle;
        public string CropTitle
        {
            get { return cropTitle; }
            set { cropTitle = value; OnPropertyChanged(); }
        }
        private string confidence;

        public string Confidence
        {
            get { return confidence; }
            set { confidence = value; OnPropertyChanged(); }
        }


        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        private string rootcauses;
        public string RootCauses
        {
            get { return rootcauses; }
            set { rootcauses = value; OnPropertyChanged(); }
        }

        private string recommendation;
        public string Recommendation
        {
            get { return recommendation; }
            set { recommendation = value; OnPropertyChanged(); }
        }

        public Command ContactVetCommand { get; set; }

        public CropInfoViewModel(ClassificationResult result)
        {
            cropTitle = result.Tag;
            confidence = result.Confidence.ToString();

            ContactVetCommand = new Command(async () => await ContactVetAsync());
        }
        public CropInfoViewModel()
        {

        }
        private async Task ContactVetAsync()
        {
            //get user id from preferences
            var userId = Preferences.Get("UserId", "");

            await Application.Current.MainPage.Navigation.PushAsync(new ContactVetView(userId));
        }


    }
}
