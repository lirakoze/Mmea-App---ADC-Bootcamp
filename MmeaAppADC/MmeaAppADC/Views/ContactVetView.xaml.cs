
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactVetView : ContentPage
    {
        public ContactVetView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.ContactVetViewModel();
        }
    }
}