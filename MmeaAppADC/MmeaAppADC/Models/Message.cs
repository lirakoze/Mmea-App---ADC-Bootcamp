using System;

namespace MmeaAppADC.Models
{
    public class Message
    {
        public string FarmerId { get; set; }
        public string VetId { get; set; }
        public string FarmerPhoneNo { get; set; }
        public string VetName { get; set; }
        public string FarmerName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        private DateTime timesent = DateTime.MinValue;
        public DateTime TimeSent
        {
            get
            {
                return (timesent == DateTime.MinValue ? DateTime.Now : timesent);
            }
            set
            {
                timesent = value;
            }

        }
    }
}
