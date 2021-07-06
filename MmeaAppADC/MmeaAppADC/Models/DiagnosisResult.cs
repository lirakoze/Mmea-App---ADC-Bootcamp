using System;

namespace MmeaAppADC.Models
{
    public class DiagnosisResult
    {
        private DateTime _diagnosisResult = DateTime.MinValue;

        public string Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string RootCause { get; set; }
        public string Recommendation { get; set; }
        public DateTime ResultDate
        {
            get
            {
                return (_diagnosisResult == DateTime.MinValue ? DateTime.Now : _diagnosisResult);
            }
            set
            {
                _diagnosisResult = value;
            }

        }
    }
}
