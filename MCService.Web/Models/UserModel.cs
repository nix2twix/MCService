namespace MCService.Web.Models
{
    public class UserModel 
    {
        public string GUID { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }

        public enum UserRole
        {
            Admin = 0,
            Customer = 1
        }

    }
}
