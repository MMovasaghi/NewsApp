namespace NewsApp.Dtos.User
{
    public class UserForUpdateDto
    {
        public string oldUsername { get; set; }
        public string oldPassword { get; set; }
        public string newUsername { get; set; }
        public string newPassword { get; set; }
    }
}