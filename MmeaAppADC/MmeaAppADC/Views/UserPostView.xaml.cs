
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPostView : ContentPage
    {
        public UserPostView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.UserPostViewModel();
        }
    }
}