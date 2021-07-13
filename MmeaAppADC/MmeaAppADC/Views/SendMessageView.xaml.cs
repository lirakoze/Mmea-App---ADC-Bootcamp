using MmeaAppADC.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendMessageView : ContentPage
    {
        public SendMessageView(Message message)
        {
            InitializeComponent();
            BindingContext = new ViewModels.SendMessageViewModel(message);
        }
    }
}