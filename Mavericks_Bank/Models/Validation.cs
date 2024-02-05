using System.ComponentModel.DataAnnotations;

namespace Mavericks_Bank.Models
{
    public class Validation:IEquatable<Validation>
    {
        [Key]
        public string Email { get; set; } 
        public string Password { get; set; }
        public string UserType { get; set; }

        public Validation(string email, string password, string userType)
        {
            Email = email;
            Password = password;
            UserType = userType;
        }

        public bool Equals(Validation? other)
        {
            if(Email == other.Email && Password == other.Password)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
