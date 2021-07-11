
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentView
    {
        public ProfileView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.ProfileViewModel();
        }
    }
}