using Firebase.Database;
using MmeaAppADC.KeysAndEndpoints;
using MmeaAppADC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MmeaAppADC.Services
{
    public class DBservice
    {
        private FirebaseClient _firebase;

        public DBservice()
        {
            _firebase = new FirebaseClient(KeysAndUrls.FirebaseDatabaseKey);
        }

        public async Task<List<County>> GetCounties()
        {
            var list = new List<County>();

            list = (await _firebase.Child("COUNTIES").OnceAsync<County>()).Select(county => new County
            {
                Name = county.Object.Name
            }).ToList();
            return list;

        }
    }
}
