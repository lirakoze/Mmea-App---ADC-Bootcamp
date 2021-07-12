using System;

namespace MmeaAppADC.Models
{
    public class Message
    {
        public string FarmerId { get; set; }
        public string VetId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        private DateTime _diagnosisDate = DateTime.MinValue;
        public DateTime DiagnosisDate
        {
            get
            {
                return (_diagnosisDate == DateTime.MinValue ? DateTime.Now : _diagnosisDate);
            }
            set
            {
                _diagnosisDate = value;
            }

        }
    }
}
