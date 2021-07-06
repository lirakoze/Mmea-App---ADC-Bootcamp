using MmeaAppADC.Models;

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

        public CropInfoViewModel(ClassificationResult result)
        {
            cropTitle = result.Tag;
            confidence = result.Confidence.ToString();
        }

        public CropInfoViewModel()
        {

        }

    }
}
