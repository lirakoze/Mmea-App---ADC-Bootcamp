
using MmeaAppADC.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactVetView : ContentPage
    {
        private SMSAndCallService _smsAndCallService;
        private Message _message { get; set; }
        private ApplicationUser _vet { get; set; }
        public ContactVetView(Message message)
        {
            InitializeComponent();
            BindingContext = new ViewModels.ContactVetViewModel();
            _message = message;
            _smsAndCallService = new SMSAndCallService();
            VetList.SelectionChanged += VetList_SelectionChanged;
        }

        private void VetList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vets = e.CurrentSelection;
            for (int i = 0; i < vets.Count; i++)
            {
                _vet = vets[i] as ApplicationUser;
                //DisplayAlert("Vet", $"{_vet.FirstName}, {_vet.SubCounty}", "Okay");
            }
        }

        private async void Send_Clicked(object sender, System.EventArgs e)
        {
            if (_vet == null)
                return;

            _message.VetId = _vet.Id;
            _message.VetPhoneNo = _vet.PhoneNo;
            _message.VetName = $"{_vet.FirstName}, {_vet.LastName}";
            _message.Content = $"Ref: {_message.Title}\n ";
            await _smsAndCallService.SendSMS(_message);


        }
        private void Call_Clicked(object sender, System.EventArgs e)
        {
            if (_vet == null)
                return;
            _smsAndCallService.PhoneDial(_message.VetPhoneNo);
            //await Navigation.PushModalAsync(new SendMessageView(_message));
        }

    }
}