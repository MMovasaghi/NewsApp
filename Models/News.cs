using System;

namespace NewsApp.Models
{
    public class News
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string imageUrl { get; set; }
        public string videoUrl { get; set; }
        public string publicImageId { get; set; }
        public string publicVideoId { get; set; }
        public DateTime created_at { get; set; }
        public DateTime expired_at { get; set; }
    }
}