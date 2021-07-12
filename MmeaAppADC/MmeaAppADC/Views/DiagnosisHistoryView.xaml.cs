
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiagnosisHistoryView : ContentPage
    {
        public DiagnosisHistoryView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.DiagnosisHistoryViewModel();
        }
    }
}