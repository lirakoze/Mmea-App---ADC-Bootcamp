using Firebase.Database;
using Firebase.Database.Query;
using MmeaAppADC.KeysAndEndpoints;
using MmeaAppADC.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        private FirebaseClient _firebase;
        public SettingsView()
        {
            InitializeComponent();
            _firebase = new FirebaseClient(KeysAndUrls.FirebaseDatabaseKey);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            var nairobi = new County()
            {
                Name = "Nairobi",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Embakasi"},
                    new SubCounty{Name="Kasarani"},
                    new SubCounty{Name="Dagoretti"}
                }
            };
            var Mombasa = new County()
            {
                Name = "Mombasa"
            };
            var Kisumu = new County()
            {
                Name = "Kisumu"
            };

            //await _firebase.Child("COUNTIES").PostAsync(nairobi);
            await _firebase.Child("COUNTIES").PostAsync(Kisumu);
            await _firebase.Child("COUNTIES").PostAsync(Mombasa);
            await Application.Current.MainPage.DisplayAlert("Success", "County Added Successfully.", "Ok");
            return;

        }
    }
}