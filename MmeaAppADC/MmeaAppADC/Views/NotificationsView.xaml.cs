
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsView : ContentView
    {
        public NotificationsView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.NotificationsViewModel();
        }
    }
}