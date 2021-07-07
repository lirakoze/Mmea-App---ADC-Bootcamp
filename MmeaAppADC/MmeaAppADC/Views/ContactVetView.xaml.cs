﻿
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactVetView : ContentPage
    {
        public ContactVetView(string userId)
        {
            InitializeComponent();
            BindingContext = new ViewModels.ContactVetViewModel(userId);
        }
    }
}