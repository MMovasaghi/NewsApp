using System;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dtos.User
{
    public class UserForRegisterDto
    {
        [Required]
        public string email { get; set; }
        [Required]
        [StringLength(maximumLength:20,MinimumLength=8,ErrorMessage="password should be between 8 to 20 characters")]
        public string password { get; set; }

    }
}