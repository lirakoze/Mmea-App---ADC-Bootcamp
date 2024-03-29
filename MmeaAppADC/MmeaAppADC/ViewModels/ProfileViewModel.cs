﻿using MmeaAppADC.Models;
using MmeaAppADC.Services;
using MmeaAppADC.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private string firstName;
        public string Firstname
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(); }
        }
        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; OnPropertyChanged(); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }
        private string type;
        private string county;
        public string County
        {
            get { return county; }
            set { county = value; OnPropertyChanged(); }
        }
        private string subCounty;
        public string SubCounty
        {
            get { return subCounty; }
            set { subCounty = value; OnPropertyChanged(); }
        }

        private string profileUrl;
        public string ProfileUrl
        {
            get { return profileUrl; }
            set { profileUrl = value; OnPropertyChanged(); }
        }
        public string Type
        {
            get { return type; }
            set { type = value; OnPropertyChanged(); }
        }
        private string phoneno;

        public string PhoneNo
        {
            get { return phoneno; }
            set { phoneno = value; OnPropertyChanged(); }
        }
        private FileResult PhotoFile { get; set; }
        public Command BrowseGalleryCommand { get; set; }
        public Command TakePhotoCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public Command DiagnosisHistoryCommand { get; set; }
        public Command PostHistoryCommand { get; set; }
        private AuthService _auth;
        private DBservice _dbService;
        private ObservableCollection<UserDiagnosis> diagnoses;
        public ObservableCollection<UserDiagnosis> Diagnoses
        {
            get { return diagnoses; }
            set { diagnoses = value; OnPropertyChanged(); }
        }
        public ProfileViewModel()
        {
            LogoutCommand = new Command(() => LogoutAsync());
            BrowseGalleryCommand = new Command(async () => await BrowseGalleryAsync());
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            DiagnosisHistoryCommand = new Command(async () => await DiagnosisHistoryAsync());
            PostHistoryCommand = new Command(async () => await PostHistoryAsync());
            _auth = new AuthService();
            _dbService = new DBservice();
            PhotoFile = null;
            Diagnoses = new ObservableCollection<UserDiagnosis>();
            GetDiagnoses();
            Firstname = Preferences.Get("Firstname", "");
            Lastname = Preferences.Get("Lastname", "");
            Email = Preferences.Get("Email", "");
            PhoneNo = Preferences.Get("PhoneNo", "");
            County = Preferences.Get("County", "");
            SubCounty = Preferences.Get("SubCounty", "");
            ProfileUrl = Preferences.Get("ProfileUrl", "");
            Type = Preferences.Get("Type", "");
            PhotoFile = null;
        }

        private async Task PostHistoryAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new PostHistoryView());
        }

        private async Task DiagnosisHistoryAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new DiagnosisHistoryView());
        }

        private async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {ProfileUrl}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {fnsEx.Message}");
            }
            catch (PermissionException pEx)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {pEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        private async Task BrowseGalleryAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {ProfileUrl}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {fnsEx.Message}");
            }
            catch (PermissionException pEx)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {pEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        private async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                ProfileUrl = null;
                PhotoFile = null;
                return;
            }
            PhotoFile = photo;
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            ProfileUrl = newFile;
        }
        private void LogoutAsync()
        {
            _auth.LogoutUser();
            Application.Current.MainPage = new LoginView();
        }
        private async void GetDiagnoses()
        {
            List<UserDiagnosis> list = await _dbService.GetUserDiagnosis();
            foreach (var user in list)
            {
                Diagnoses.Add(user);
            }
        }
    }
}
