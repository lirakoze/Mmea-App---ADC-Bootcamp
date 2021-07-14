
using MmeaAppADC.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactVetView : ContentPage
    {
        public Message _message { get; set; }
        public ApplicationUser _vet { get; set; }
        public ContactVetView(Message message)
        {
            InitializeComponent();
            BindingContext = new ViewModels.ContactVetViewModel();
            _message = message;
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
            _message.VetId = _vet.Id;
            _message.VetName = $"{_vet.FirstName}, {_vet.LastName}";

            await Navigation.PushModalAsync(new SendMessageView(_message));
        }
    }
}