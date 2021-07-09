using System;

namespace MmeaAppADC.Models
{
    public class UserDiagnosis
    {
        private DateTime _diagnosisDate = DateTime.MinValue;

        public string UserId { get; set; }
        public string Tag { get; set; }
        public double Confidence { get; set; }
        public string County { get; set; }
        public string SubCounty { get; set; }
        public string ImageUrl { get; set; }
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
