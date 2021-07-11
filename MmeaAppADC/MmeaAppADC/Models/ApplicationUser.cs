namespace MmeaAppADC.Models
{
    public class ApplicationUser
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public string County { get; set; }
        public string SubCounty { get; set; }
        public string Type { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
