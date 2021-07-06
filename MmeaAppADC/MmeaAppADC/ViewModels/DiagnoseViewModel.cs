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
        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; OnPropertyChanged(); }
        }

        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(); }
        }
        private DiagnosisService _diagnosisService { get; set; }
        public Command BrowseGalleryCommand { get; set; }
        public Command TakePhotoCommand { get; set; }
        public Command DiagnoseCommand { get; set; }

        private Stream imageStream;

        public DiagnoseViewModel()
        {
            _diagnosisService = new DiagnosisService();
            BrowseGalleryCommand = new Command(async () => await BrowseGalleryAsync());
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            DiagnoseCommand = new Command(async () => await DiagnoseAsync());
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
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            imageStream = await photo.OpenReadAsync();
            Image = newFile;
        }


        private async Task DiagnoseAsync()
        {
            if (imageStream == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An Image is required for processing.", "Ok");
                return;
            }

            var result = await _diagnosisService.GetImageClassification(imageStream);
            Image = null;

            //Navigate to result page
            await Application.Current.MainPage.Navigation.PushAsync(new CropInfoView(result));
        }
    }
}
