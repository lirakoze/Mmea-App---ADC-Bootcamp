
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.VetArea.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VetDiagnoseView : ContentView
    {
        public VetDiagnoseView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.DiagnoseViewModel();
        }
    }
}