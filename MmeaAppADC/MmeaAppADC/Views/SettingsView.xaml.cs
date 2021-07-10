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
                    new SubCounty{Name="Dagoretti"},
                    new SubCounty{Name="Kamkunji"},
                    new SubCounty{Name="Kibra"},
                    new SubCounty{Name="Lang'ata"},
                    new SubCounty{Name="Makadara"},
                    new SubCounty{Name="Mathare"},
                    new SubCounty{Name="Njiru"},
                    new SubCounty{Name="Starehe"},
                    new SubCounty{Name="Westlands"},

                }
            };
            var Mombasa = new County()
            {
                Name = "Mombasa",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Changamwe"},
                    new SubCounty{Name="Jomvu"},
                    new SubCounty{Name="Kisauni"},
                    new SubCounty{Name="Likoni"},
                    new SubCounty{Name="Mvita"},
                    new SubCounty{Name="Nyali"},
                }
            };
            var baringo = new County()
            {
                Name = "Baringo",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Baringo Central"},
                    new SubCounty{Name="East Pokot"},
                    new SubCounty{Name="Koibatek"},
                    new SubCounty{Name="Marigat"},
                    new SubCounty{Name="Mogotio"},
                    new SubCounty{Name="Tiaty East"},
                }

            };
            var bomet = new County()
            {
                Name = "Bomet",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Bomet Central"},
                    new SubCounty{Name="Bomet East"},
                    new SubCounty{Name="Chepalungu"},
                    new SubCounty{Name="Marigat"},
                    new SubCounty{Name="Konoin"},
                    new SubCounty{Name="Sotik"},
                }
            };
            var bungoma = new County()
            {
                Name = "Bungoma",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Bumula"},
                    new SubCounty{Name="Bungoma Central"},
                    new SubCounty{Name="Bungoma East"},
                    new SubCounty{Name="Bungoma North"},
                    new SubCounty{Name="Bungoma South"},
                    new SubCounty{Name="Bungoma West"},
                    new SubCounty{Name="Cheptais"},
                    new SubCounty{Name="Kimilili"},
                    new SubCounty{Name="Mt Elgon"},
                    new SubCounty{Name="Tongaren"},
                    new SubCounty{Name="Webuye West"},
                }
            };
            var busia = new County()
            {
                Name = "Busia",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Busia"},
                    new SubCounty{Name="Bunyala"},
                    new SubCounty{Name="Butula"},
                    new SubCounty{Name="Nambale"},
                    new SubCounty{Name="Samia"},
                    new SubCounty{Name="Konoin"},
                    new SubCounty{Name="Teso North"},
                    new SubCounty{Name="Teso South"},
                }
            };
            var elgeyo = new County()
            {
                Name = "Elgeyo Marakwet",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Keiyo North"},
                    new SubCounty{Name="Keiyo Est"},
                    new SubCounty{Name="Marakwet East"},
                    new SubCounty{Name="Marakwet West"},
                }
            };
            var embu = new County()
            {
                Name = "Embu",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Embu East"},
                    new SubCounty{Name="Embu West"},
                    new SubCounty{Name="Mbeere North"},
                    new SubCounty{Name="Mbeere South"},
                    new SubCounty{Name="Mt Kenya Forest"},
                }
            };
            var garissa = new County()
            {
                Name = "Garissa",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Balambala"},
                    new SubCounty{Name="Dabaab"},
                    new SubCounty{Name="Fafi"},
                    new SubCounty{Name="Garissa"},
                    new SubCounty{Name="Hulugho"},
                    new SubCounty{Name="Ijara"},
                    new SubCounty{Name="Lagdera"},
                }
            };
            var homabay = new County()
            {
                Name = "Homabay",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Homabay"},
                    new SubCounty{Name="Ndhiwa"},
                    new SubCounty{Name="Rachuonyo East"},
                    new SubCounty{Name="Rachuonyo North"},
                    new SubCounty{Name="Rachuonyo South"},
                    new SubCounty{Name="Rangwe"},
                    new SubCounty{Name="Suba North"},
                    new SubCounty{Name="Suba South"},
                }
            };
            var kajiado = new County()
            {
                Name = "Kajiado",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Isinya"},
                    new SubCounty{Name="Kajiado Central"},
                    new SubCounty{Name="Kajiado North"},
                    new SubCounty{Name="Kajiado West"},
                    new SubCounty{Name="Loitokotok"},
                    new SubCounty{Name="Mashuuru"}
                }
            };
            var kakamega = new County()
            {
                Name = "Kakamega",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Butere"},
                    new SubCounty{Name="Kakamega Central"},
                    new SubCounty{Name="Kakamega East"},
                    new SubCounty{Name="Kakamega North"},
                    new SubCounty{Name="Kakamega South"},
                    new SubCounty{Name="Khwisero"},
                    new SubCounty{Name="Likuyani"},
                    new SubCounty{Name="Lugari"},
                    new SubCounty{Name="Matete"},
                    new SubCounty{Name="Matungu"},
                    new SubCounty{Name="Mumias East"},
                    new SubCounty{Name="Mumias West"},
                    new SubCounty{Name="Navakholo"},
                }
            };
            var kericho = new County()
            {
                Name = "Kericho",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Belgut"},
                    new SubCounty{Name="Bureti"},
                    new SubCounty{Name="Kericho Est"},
                    new SubCounty{Name="Kipkelion"},
                    new SubCounty{Name="Londiani"},
                    new SubCounty{Name="Soin Sigowet"},

                }
            };
            var kisumu = new County()
            {
                Name = "Kisumu",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Kisumu Central"},
                    new SubCounty{Name="Kisumu East"},
                    new SubCounty{Name="Kisumu West"},
                    new SubCounty{Name="Muhoroni"},
                    new SubCounty{Name="Nyakach"},
                    new SubCounty{Name="Nyando"},
                    new SubCounty{Name="Seme"},

                }
            };
            var kiambu = new County()
            {
                Name = "Kiambu",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Gatundu North"},
                    new SubCounty{Name="Gatundu South"},
                    new SubCounty{Name="Githunguri"},
                    new SubCounty{Name="Juja"},
                    new SubCounty{Name="Kabete"},
                    new SubCounty{Name="Kiambaa"},
                    new SubCounty{Name="Kiambu"},
                    new SubCounty{Name="Kikuyu"},
                    new SubCounty{Name="Lari"},
                    new SubCounty{Name="Limuru"},
                    new SubCounty{Name="Ruiru"},
                    new SubCounty{Name="Thika East"},
                    new SubCounty{Name="Thika West"},

                }
            };
            var kilifi = new County()
            {
                Name = "Kilifi",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Chonyi"},
                    new SubCounty{Name="Ganze"},
                    new SubCounty{Name="Kaloleni"},
                    new SubCounty{Name="Kauma"},
                    new SubCounty{Name="Kilifi North"},
                    new SubCounty{Name="Kilifi South"},
                    new SubCounty{Name="Magarini"},
                    new SubCounty{Name="Malindi"},
                    new SubCounty{Name="Rabai"},

                }
            };
            var kirinyaga = new County()
            {
                Name = "Kirinyaga",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Kirinyaga Central"},
                    new SubCounty{Name="Kirinyaga Est"},
                    new SubCounty{Name="Kirinyaga West"},
                    new SubCounty{Name="Mt Kenya Forest"},
                    new SubCounty{Name="Mwea East"},
                    new SubCounty{Name="Mwea West"}
                }
            };
            var kisii = new County()
            {
                Name = "Kisii",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Etago"},
                    new SubCounty{Name="Gucha South"},
                    new SubCounty{Name="Kenyenya"},
                    new SubCounty{Name="Kisii Central"},
                    new SubCounty{Name="Kisii South"},
                    new SubCounty{Name="Kitutu Central"},
                    new SubCounty{Name="Marani"},
                    new SubCounty{Name="Masaba South"},
                    new SubCounty{Name="Nyamache"},
                    new SubCounty{Name="Sameta"},
                }
            };
            var kitui = new County()
            {
                Name = "Kitui",
                SubCounties = new List<SubCounty>()
                {
                    new SubCounty{Name="Ikutha"},
                    new SubCounty{Name="Katulani"},
                    new SubCounty{Name="Kisasi"},
                    new SubCounty{Name="Kitui Central"},
                    new SubCounty{Name="Kitui West"},
                    new SubCounty{Name="Kyuso"},
                    new SubCounty{Name="Lower Yatta"},
                    new SubCounty{Name="Matinyani"},
                    new SubCounty{Name="Migwani"},
                    new SubCounty{Name="Mumoni"},
                    new SubCounty{Name="Mutitu"},
                    new SubCounty{Name="Mutitu North"},
                    new SubCounty{Name="Mutomo"},
                    new SubCounty{Name="Mwingi Central"},
                    new SubCounty{Name="Mwingi East"},
                    new SubCounty{Name="Nzambani"},
                    new SubCounty{Name="Thagicu"},
                    new SubCounty{Name="Tseikuru"},
                }
            };


            await _firebase.Child("COUNTIES").PostAsync(nairobi);
            await _firebase.Child("COUNTIES").PostAsync(baringo);
            await _firebase.Child("COUNTIES").PostAsync(Mombasa);
            await _firebase.Child("COUNTIES").PostAsync(bomet);
            await _firebase.Child("COUNTIES").PostAsync(bungoma);
            await _firebase.Child("COUNTIES").PostAsync(busia);
            await _firebase.Child("COUNTIES").PostAsync(elgeyo);
            await _firebase.Child("COUNTIES").PostAsync(embu);
            await _firebase.Child("COUNTIES").PostAsync(garissa);
            await _firebase.Child("COUNTIES").PostAsync(homabay);
            await _firebase.Child("COUNTIES").PostAsync(kajiado);
            await _firebase.Child("COUNTIES").PostAsync(kakamega);
            await _firebase.Child("COUNTIES").PostAsync(kericho);
            await _firebase.Child("COUNTIES").PostAsync(kiambu);
            await _firebase.Child("COUNTIES").PostAsync(kisumu);
            await _firebase.Child("COUNTIES").PostAsync(kilifi);
            await _firebase.Child("COUNTIES").PostAsync(kirinyaga);
            await _firebase.Child("COUNTIES").PostAsync(kisii);
            await _firebase.Child("COUNTIES").PostAsync(kitui);


            await Application.Current.MainPage.DisplayAlert("Success", "County Added Successfully.", "Ok");
            return;

        }
    }
}