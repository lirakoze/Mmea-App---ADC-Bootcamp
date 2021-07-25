using System;

namespace MmeaAppADC.Models
{
    public class Post
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
        private DateTime postDate = DateTime.MinValue;
        public DateTime PostDate
        {
            get
            {
                return (postDate == DateTime.MinValue ? DateTime.Now : postDate);
            }
            set
            {
                postDate = value;
            }

        }
    }
}
