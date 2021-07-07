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

        public void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }



    }
}
