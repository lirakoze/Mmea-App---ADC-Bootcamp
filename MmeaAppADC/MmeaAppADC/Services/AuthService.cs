using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using MmeaAppADC.KeysAndEndpoints;
using MmeaAppADC.Models;
using Newtonsoft.Json;
using System;
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
                Preferences.Set("MyFirebaseFreshToken", serializedContent);
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
            Preferences.Remove("MyFirebaseRefreshToken");
        }
    }
}
