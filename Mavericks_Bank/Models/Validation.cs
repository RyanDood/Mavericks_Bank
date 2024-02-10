using System.ComponentModel.DataAnnotations;

namespace Mavericks_Bank.Models
{
    public class Validation:IEquatable<Validation>
    {
        [Key]
        public string Email { get; set; } 
        public byte[] Password { get; set; }
        public string UserType { get; set; }
        public byte[] Key { get; set; }

        public Validation()
        {

        }

        public Validation(string email, byte[] password, string userType, byte[] key)
        {
            Email = email;
            Password = password;
            UserType = userType;
            Key = key;
        }

        public bool Equals(Validation? other)
        {
            return Email == other.Email;
        }
    }
}
