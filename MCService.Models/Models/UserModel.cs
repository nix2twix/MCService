namespace MCService.Models
{
    public class UserModel 
    {

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public enum UserRole
        {
            Admin = 0,
            Customer = 1
        }

    }
}
