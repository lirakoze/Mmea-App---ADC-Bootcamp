using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using MmeaAppADC.KeysAndEndpoints;
using MmeaAppADC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MmeaAppADC.Services
{
    public class AuthService
    {
        private FirebaseAuthProvider _authProvider;
        private FirebaseClient _firebase;

        public AuthService()
        {
            _authProvider = new FirebaseAuthProvider(new FirebaseConfig(KeysAndUrls.FirebaseWebAPIKey));
            _firebase = new FirebaseClient(KeysAndUrls.FirebaseDatabaseKey);
        }

        //Creating users
        public async Task<bool> RegisterUser(ApplicationUser user, string password)
        {
            try
            {
                var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(user.Email, password);

                string getToken = auth.FirebaseToken;

                var authUser = await _authProvider.GetUserAsync(getToken);
                user.Id = authUser.LocalId;
                await _firebase.Child("USERS").PostAsync(user);

                return true;
            }
            catch (Exception ex)
            {

                System.Console.WriteLine($"Failed to create user.\n  {ex}");
                return false;
            }


        }

        //Signing in Users
        public async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
                var content = auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                var uid = auth.User.LocalId;

                var userInfo = await GetUserInfo(uid);

                Preferences.Set("MyFirebaseFreshToken", serializedContent);
                Preferences.Set("UserId", uid);
                Preferences.Set("Firstname", userInfo.FirstName);
                Preferences.Set("Lastname", userInfo.LastName);
                Preferences.Set("County", userInfo.County);
                Preferences.Set("SubCounty", userInfo.SubCounty);
                Preferences.Set("Email", userInfo.Email);
                Preferences.Set("PhoneNo", userInfo.PhoneNo);
                Preferences.Set("ProfileUrl", "");
                Preferences.Set("Type", userInfo.Type);

                return true;

            }
            catch (Exception ex)
            {

                System.Console.WriteLine($"failed to log in user.\n {ex}");
                return false;
            }

        }

        //Get Profile Information
        public async void GetProfileInfo()
        {
            try
            {
                //This is the saved firebaseauthentication that was saved during the time of login
                var savedfirebaseauth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                //Here we are Refreshing the token
                var RefreshedContent = await _authProvider.RefreshAuthAsync(savedfirebaseauth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));

                //retireve user info
                //savedfirebaseauth.User.DisplayName;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Logout User
        public void LogoutUser()
        {
            Preferences.Clear();
        }
        private async Task<ApplicationUser> GetUserInfo(string userId)
        {
            var list = await GetUsers();

            return list.Where(u => u.Id == userId).FirstOrDefault();
        }
        private async Task<List<ApplicationUser>> GetUsers()
        {
            return (await _firebase.Child("USERS").OnceAsync<ApplicationUser>()).Select(u => new ApplicationUser
            {
                Id = u.Object.Id,
                FirstName = u.Object.FirstName,
                LastName = u.Object.LastName,
                Email = u.Object.Email,
                County = u.Object.County,
                SubCounty = u.Object.SubCounty,
                PhoneNo = u.Object.PhoneNo,
                ProfileImageUrl = u.Object.ProfileImageUrl,
                Type = u.Object.Type
            }).ToList();
        }
    }
}