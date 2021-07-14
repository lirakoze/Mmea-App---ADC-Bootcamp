using MmeaAppADC.Models;
using MmeaAppADC.Services;
using MmeaAppADC.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class SendMessageViewModel : BaseViewModel
    {
        public Message _message { get; set; }
        public MessageService _mService;
        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

        public Command SendMessageCommand { get; set; }

        public SendMessageViewModel(Message message)
        {
            _message = new Message();
            _message = message;
            content = "";
            SendMessageCommand = new Command(async () => await SendMessageAsync());
            _mService = new MessageService();
        }

        private async Task SendMessageAsync()
        {
            if (content.Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Empty", "Please, write something before sending", "Okay");
                return;
            }
            else
            {
                _message.Content = Content;
                _message.TimeSent = System.DateTime.UtcNow;
                var isSent = await _mService.SendMessage(_message);
                if (isSent)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", $"The Message was successfully sent to {_message.VetName}", "Okay");

                    Application.Current.MainPage = new HomeView();
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Failed", "Failed to send Message", "Try Again");
            }
        }

        public SendMessageViewModel()
        {

        }
    }
}
