using System;
using Microsoft.AspNetCore.Http;

namespace NewsApp.Dtos.News
{
    public class NewsToUpdate
    {
        public Guid id {get; set;}
        public string title { get; set; }
        public string content { get; set; }
        public IFormFile image { get; set; }
        public IFormFile video { get; set; }
        public DateTime? expired_at { get; set; }
    }
}