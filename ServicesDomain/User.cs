using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDomain
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public enum UserRole
        {
            Admin = 0,
            Customer = 1
        }
    }
}
