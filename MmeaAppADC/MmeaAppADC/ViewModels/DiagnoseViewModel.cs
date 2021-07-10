using Acr.UserDialogs;
using MmeaAppADC.Models;
using MmeaAppADC.Services;
using MmeaAppADC.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class DiagnoseViewModel : BaseViewModel
    {

        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(); }
        }
        private DiagnosisService _diagnosisService { get; set; }
        private DBservice _dBservice { get; set; }
        public Command BrowseGalleryCommand { get; set; }
        public Command TakePhotoCommand { get; set; }
        public Command DiagnoseCommand { get; set; }
        private FileResult PhotoFile { get; set; }
        public DiagnoseViewModel()
        {
            _diagnosisService = new DiagnosisService();
            _dBservice = new DBservice();
            BrowseGalleryCommand = new Command(async () => await BrowseGalleryAsync());
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            DiagnoseCommand = new Command(async () => await DiagnoseAsync());
            PhotoFile = null;

        }

        private async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {Image}");
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
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {Image}");
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
                Image = null;
                PhotoFile = null;
                return;
            }
            PhotoFile = photo;
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            Image = newFile;
        }
        private async Task DiagnoseAsync()
        {
            UserDialogs.Instance.ShowLoading("Diagnosis Underway....");

            if (PhotoFile == null)
            {
                UserDialogs.Instance.HideLoading();
                await Application.Current.MainPage.DisplayAlert("Error", "An Image is required for processing.", "Ok");
                return;
            }
            else
            {
                var result = await _diagnosisService.GetImageClassification(await PhotoFile.OpenReadAsync());
                var url = await _dBservice.UploadDiagnosisPhoto(await PhotoFile.OpenReadAsync(), PhotoFile.FileName);

                UserDiagnosis diagnosis = new UserDiagnosis
                {
                    UserId = Preferences.Get("UserId", ""),
                    Tag = result.Tag,
                    Confidence = result.Confidence,
                    County = Preferences.Get("County", ""),
                    SubCounty = Preferences.Get("SubCounty", ""),
                    ImageUrl = url,
                    DiagnosisDate = DateTime.Now
                };

                var isSuccess = await _dBservice.SaveUserDiagnosis(diagnosis, Preferences.Get("County", ""));

                UserDialogs.Instance.HideLoading();

                if (isSuccess)
                {
                    PhotoFile = null;
                    Image = null;
                    //Navigate to result page
                    await Application.Current.MainPage.Navigation.PushAsync(new CropInfoView(result, url));
                }
                return;

            }

        }
    }
}
