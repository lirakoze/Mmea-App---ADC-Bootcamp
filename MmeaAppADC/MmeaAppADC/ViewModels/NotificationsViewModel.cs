using MmeaAppADC.Models;
using MmeaAppADC.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        public Command RefreshCommand { get; set; }
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(); }
        }
        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; OnPropertyChanged(); }
        }
        public NotificationsViewModel()
        {
            _mService = new MessageService();
            Messages = new ObservableCollection<Message>();
            RefreshCommand = new Command(async () => await RefreshAsync());
            _ = GetMessages();
            IsRefreshing = false;
            IsVisible = false;
        }

        private async Task RefreshAsync()
        {
            Messages.Clear();
            await GetMessages();
            IsRefreshing = false;
        }

        private async Task GetMessages()
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
            if (list.Count == 0)
                IsVisible = true;

        }
    }
}
