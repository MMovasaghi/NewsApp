using System;

namespace NewsApp.Dtos.News
{
    public class NewsToShow
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string imageUrl { get; set; }
        public string videoUrl { get; set; }
        public string publicImageId { get; set; }
        public string publicVideoId { get; set; }
        public DateTime created_at { get; set; }
    }
}