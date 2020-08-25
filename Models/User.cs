using System;

namespace NewsApp.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }        
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}