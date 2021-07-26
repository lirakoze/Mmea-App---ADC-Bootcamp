using MmeaAppADC.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostDetailView : ContentPage
    {

        public PostDetailView(Post post)
        {
            InitializeComponent();
            BindingContext = new ViewModels.PostDetailViewModel(post);
        }
    }
}