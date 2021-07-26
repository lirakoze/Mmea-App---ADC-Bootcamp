using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using MmeaAppADC.KeysAndEndpoints;
using MmeaAppADC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MmeaAppADC.Services
{
    public class DBservice
    {
        private FirebaseClient _firebase;
        private FirebaseStorage _firebaseStorage;
        public DBservice()
        {
            _firebase = new FirebaseClient(KeysAndUrls.FirebaseDatabaseKey);
            _firebaseStorage = new FirebaseStorage(KeysAndUrls.FirebaseStorageKey);
        }

        //Getting Counties
        public async Task<List<County>> GetCounties()
        {
            var list = new List<County>();

            list = (await _firebase.Child("COUNTIES").OnceAsync<County>()).Select(county => new County
            {
                Name = county.Object.Name
            }).ToList();
            return list;

        }

        //Getting agrovets from a given region
        public async Task<List<ApplicationUser>> GetAgroVets(string subcounty)
        {

            var list = new List<ApplicationUser>();

            list = (await _firebase.Child("USERS").OnceAsync<ApplicationUser>()).Select(vet => new ApplicationUser
            {
                FirstName = vet.Object.FirstName,
                LastName = vet.Object.LastName,
                Id = vet.Object.Id,
                County = vet.Object.County,
                SubCounty = vet.Object.SubCounty,
                PhoneNo = vet.Object.PhoneNo,
                Type = vet.Object.Type
            }).Where(u => u.SubCounty == subcounty && u.Type == "Agro-Vet").ToList();

            return list;

        }

        //Getting agrovets from a given region
        public async Task<County> GetCounty(string county)
        {

            var list = new List<County>();

            list = (await _firebase.Child("COUNTIES").OnceAsync<County>()).Select(ct => new County
            {
                Name = ct.Object.Name,
                SubCounties = ct.Object.SubCounties
            }).ToList();

            return list.Where(u => u.Name == county).FirstOrDefault();

        }
        public async Task<bool> SaveUserDiagnosis(UserDiagnosis userDiagnosis, string county)
        {
            try
            {
                await _firebase.Child("DIAGNOSIS").PostAsync(userDiagnosis);
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Failed to Save User Registration", ex.Message);
                return false;
            }

        }


        //Uploading User Diagnosis Image
        public async Task<string> UploadDiagnosisPhoto(Stream stream, string filename)
        {
            await _firebaseStorage
                .Child("IMAGES").Child(filename).PutAsync(stream);
            var url = await GetUrl(filename);

            return url;
        }
        private async Task<string> GetUrl(string fileName)
        {
            return await _firebaseStorage
                .Child("IMAGES")
                .Child(fileName)
                .GetDownloadUrlAsync();
        }
        //end

        //Getting user diagnosis
        public async Task<List<UserDiagnosis>> GetUserDiagnosis()
        {
            var id = Preferences.Get("UserId", "");
            List<UserDiagnosis> list = new List<UserDiagnosis>();
            list = (await _firebase.Child("DIAGNOSIS").OnceAsync<UserDiagnosis>()).Select(diag => new UserDiagnosis
            {
                DiagnosisDate = diag.Object.DiagnosisDate,
                Tag = diag.Object.Tag,
                Confidence = diag.Object.Confidence,
                UserId = diag.Object.UserId,
                ImageUrl = diag.Object.ImageUrl,
                SubCounty = diag.Object.SubCounty,
            }).Where(diag => diag.UserId == id).OrderByDescending(o => o.DiagnosisDate).ToList();
            return list;
        }


        //POSTS
        public async Task<bool> Post(Post post)
        {
            try
            {
                await _firebase.Child("POSTS").PostAsync(post);
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Failed to Post", ex.Message);
                return false;
            }

        }
        public async Task<List<Post>> GetPosts()
        {
            List<Post> list = new List<Post>();
            list = (await _firebase.Child("POSTS").OnceAsync<Post>()).Select(p => new Post
            {
                Username = p.Object.Username,
                Content = p.Object.Content,
                PostDate = p.Object.PostDate,
                UserId = p.Object.UserId,
                ImageUrl = p.Object.ImageUrl,

            }).OrderByDescending(o => o.PostDate).ToList();
            return list;
        }
        public async Task<string> UploadPostPhoto(Stream stream, string filename)
        {
            await _firebaseStorage
                .Child("POST_IMAGES").Child(filename).PutAsync(stream);
            var url = await GetPostPhotoUrl(filename);

            return url;
        }
        private async Task<string> GetPostPhotoUrl(string fileName)
        {
            return await _firebaseStorage
                .Child("POST_IMAGES")
                .Child(fileName)
                .GetDownloadUrlAsync();
        }
        public async Task<List<Post>> GetUserPosts()
        {
            List<Post> list = new List<Post>();
            list = (await _firebase.Child("POSTS").OnceAsync<Post>()).Select(p => new Post
            {
                Username = p.Object.Username,
                Content = p.Object.Content,
                PostDate = p.Object.PostDate,
                UserId = p.Object.UserId,
                ImageUrl = p.Object.ImageUrl,

            }).Where(u => u.UserId == Preferences.Get("UserId", "")).OrderByDescending(o => o.PostDate).ToList();
            return list;
        }
    }
}
