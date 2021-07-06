using System.Collections.Generic;

namespace MmeaAppADC.Models
{
    public class County
    {
        public string Name { get; set; }
        public List<SubCounty> SubCounties { get; set; }
    }
}
