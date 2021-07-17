
using MmeaAppADC.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.VetArea.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VetNotificationView : ContentView
    {
        public Message _message;
        public VetNotificationView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.NotificationsViewModel();
            _message = new Message();
        }

        private void MessagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var messages = e.CurrentSelection;
            for (int i = 0; i < messages.Count; i++)
            {
                _message = messages[i] as Message;
            }
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            _message.Content = "";

            //await Navigation.PushModalAsync(new SendMessageView(_message));
        }
    }
}