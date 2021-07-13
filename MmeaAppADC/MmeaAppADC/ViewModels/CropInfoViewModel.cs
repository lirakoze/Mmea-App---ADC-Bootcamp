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

        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(); }
        }


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

        public CropInfoViewModel(ClassificationResult result, string imageUrl)
        {
            cropTitle = result.Tag;
            confidence = result.Confidence.ToString();
            image = imageUrl;

            ContactVetCommand = new Command(async () => await ContactVetAsync());
        }
        public CropInfoViewModel()
        {

        }
        private async Task ContactVetAsync()
        {
            Message message = new Message
            {
                Title = $"{CropTitle} With {Confidence} % ",
                FarmerId = Preferences.Get("UserId", ""),
                FarmerName = $"{Preferences.Get("Firstname", "")}, {Preferences.Get("Lastname", "")}",
                FarmerPhoneNo = Preferences.Get("PhoneNo", ""),
            };
            await Application.Current.MainPage.Navigation.PushAsync(new ContactVetView(message));
        }


    }
}
