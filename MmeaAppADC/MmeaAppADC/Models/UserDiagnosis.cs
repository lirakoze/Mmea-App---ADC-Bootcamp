using System;

namespace MmeaAppADC.Models
{
    public class UserDiagnosis
    {
        private DateTime _diagnosisDate = DateTime.MinValue;

        public string Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid UserId { get; set; }

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
