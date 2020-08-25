using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dtos.User
{
    public class UserForAuthDto
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}