using Acr.UserDialogs;
using MmeaAppADC.Models;
using MmeaAppADC.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class UserPostViewModel : BaseViewModel
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; OnPropertyChanged(); }
        }
        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(); }
        }
        private DBservice _dBservice { get; set; }
        public Command AddPhotoCommand { get; set; }
        public Command PostCommand { get; set; }
        public Command RemovePhotoCommand { get; set; }
        private FileResult PhotoFile { get; set; }

        public UserPostViewModel()
        {
            AddPhotoCommand = new Command(async () => await AddPhotoAsync());
            PostCommand = new Command(async () => await PostAsync());
            RemovePhotoCommand = new Command(() => RemovePhotoAsync());
            PhotoFile = null;
            IsVisible = false;
            _dBservice = new DBservice();
            username = $"{Preferences.Get("Firstname", "")} {Preferences.Get("Lastname", "")}";
        }


        private async Task AddPhotoAsync()
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

            IsVisible = true;
            Image = newFile;
        }


        private async Task PostAsync()
        {

            Post post = new Post
            {
                UserId = Preferences.Get("UserId", ""),
                PostDate = DateTime.Now
            };
            UserDialogs.Instance.ShowLoading("Posting....");
            if (Content == null && PhotoFile == null)
            {
                UserDialogs.Instance.HideLoading();
                await Application.Current.MainPage.DisplayAlert("Post", "Empty Post. Please provide some content", "Ok");
                return;
            }
            else if (Content != null && PhotoFile == null)
            {
                post.ImageUrl = "";
                post.Content = Content;
                var isSuccess = await _dBservice.Post(post);

                UserDialogs.Instance.HideLoading();
                if (isSuccess)
                {
                    Clear();
                    await Application.Current.MainPage.DisplayAlert("Post", "Post was successfull", "Ok");
                    return;
                }
            }
            else if (Content == null && PhotoFile != null)
            {
                var url = await _dBservice.UploadPostPhoto(await PhotoFile.OpenReadAsync(), PhotoFile.FileName);
                post.Content = "";
                post.ImageUrl = url;
                var isSuccess = await _dBservice.Post(post);
                UserDialogs.Instance.HideLoading();
                if (isSuccess)
                {
                    Clear();
                    await Application.Current.MainPage.DisplayAlert("Post", "Post was successfull", "Ok");
                    return;
                }
            }
            else
            {
                var url = await _dBservice.UploadPostPhoto(await PhotoFile.OpenReadAsync(), PhotoFile.FileName);
                post.Content = Content;
                post.ImageUrl = url;
                var isSuccess = await _dBservice.Post(post);
                UserDialogs.Instance.HideLoading();
                if (isSuccess)
                {
                    Clear();
                    await Application.Current.MainPage.DisplayAlert("Post", "Post was successfull", "Ok");
                    return;
                }

            }
        }

        private void RemovePhotoAsync()
        {
            Image = null;
            PhotoFile = null;
            IsVisible = false;

        }

        private void Clear()
        {
            PhotoFile = null;
            Image = null;
            Content = null;
            IsVisible = false;
        }
    }
}
