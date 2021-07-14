using Firebase.Database;
using Firebase.Database.Query;
using MmeaAppADC.KeysAndEndpoints;
using MmeaAppADC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MmeaAppADC.Services
{
    public class MessageService
    {
        private FirebaseClient _firebase;
        public MessageService()
        {
            _firebase = new FirebaseClient(KeysAndUrls.FirebaseDatabaseKey);
        }
        public async Task<bool> SendMessage(Message message)
        {
            try
            {
                await _firebase.Child("MESSAGES").PostAsync(message);
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Failed to Save Message", ex.Message);
                return false;
            }

        }
        public async Task<List<Message>> GetFarmerMessages()
        {
            var list = new List<Message>();

            list = (await _firebase.Child("MESSAGES").OnceAsync<Message>()).Select(m => new Message
            {
                Content = m.Object.Content,
                FarmerId = m.Object.FarmerId,
                VetId = m.Object.VetId,
                VetName = m.Object.VetName,
                FarmerName = m.Object.FarmerName,
                FarmerPhoneNo = m.Object.FarmerPhoneNo,
                TimeSent = m.Object.TimeSent,
                Title = m.Object.Title

            }).Where(m => m.FarmerId == Preferences.Get("UserId", "")).OrderByDescending(o => o.TimeSent).ToList();

            return list;
        }
        public async Task<List<Message>> GetVetMessages()
        {
            var list = new List<Message>();

            list = (await _firebase.Child("MESSAGES").OnceAsync<Message>()).Select(m => new Message
            {
                Content = m.Object.Content,
                FarmerId = m.Object.FarmerId,
                VetId = m.Object.VetId,
                VetName = m.Object.VetName,
                FarmerName = m.Object.FarmerName,
                FarmerPhoneNo = m.Object.FarmerPhoneNo,
                TimeSent = m.Object.TimeSent,
                Title = m.Object.Title

            }).Where(m => m.VetId == Preferences.Get("UserId", "")).OrderByDescending(o => o.TimeSent).ToList();

            return list;
        }

    }
}
