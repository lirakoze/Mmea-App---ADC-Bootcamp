using MmeaAppADC.Models;
using MmeaAppADC.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace MmeaAppADC.ViewModels
{
    public class NotificationsViewModel : BaseViewModel
    {
        private ObservableCollection<Message> messages;
        public ObservableCollection<Message> Messages
        {
            get { return messages; }
            set { messages = value; OnPropertyChanged(); }
        }
        private MessageService _mService;
        public NotificationsViewModel()
        {
            _mService = new MessageService();
            Messages = new ObservableCollection<Message>();
            GetMessages();
        }
        private async void GetMessages()
        {
            List<Message> list;

            var type = Preferences.Get("Type", "");
            if (type.Equals("Farmer"))
            {
                list = await _mService.GetFarmerMessages();
                foreach (var m in list)
                {
                    Messages.Add(m);
                }
            }
            else
            {
                list = await _mService.GetVetMessages();
                foreach (var m in list)
                {
                    Messages.Add(m);
                }
            }
        }
    }
}
