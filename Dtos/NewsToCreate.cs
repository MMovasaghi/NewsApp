using System;
using Microsoft.AspNetCore.Http;

namespace NewsApp.Dtos
{
    public class NewsToCreate
    {
        public string title { get; set; }
        public string content { get; set; }
        public IFormFile image { get; set; }
        public IFormFile video { get; set; }
        public DateTime created_at { get; set; }
        public DateTime expired_at { get; set; }
        public NewsToCreate()
        {
            created_at = DateTime.Now;
            expired_at = DateTime.Now.AddDays(7);
        }
    }
}