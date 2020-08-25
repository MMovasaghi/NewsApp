namespace NewsApp.Dtos.User
{
    public class UserForUpdateDto
    {
        public string oldEmail { get; set; }
        public string oldPassword { get; set; }
        public string newEmail { get; set; }
        public string newPassword { get; set; }
    }
}