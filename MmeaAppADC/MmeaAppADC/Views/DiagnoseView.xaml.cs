
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiagnoseView : ContentView
    {
        public DiagnoseView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.DiagnoseViewModel();
        }


    }
}