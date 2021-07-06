using System;

namespace MmeaAppADC.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AlertDate { get; set; }
        public int SubCountyId { get; set; }
    }
}
