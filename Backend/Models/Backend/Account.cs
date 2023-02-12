using Backend.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models.Backend
{
    public class Account : Entity
    {
        public String Login { get; set; }

        public String PasswordHash { get; set; }

        public String PasswordSalt { get; set; }

        public Account(Guid id, String login, String password)
        {
            Id = id;
            Login = login;
            Byte[] passwordHash, passwordSalt;
            CryptoUtils.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            PasswordHash = Convert.ToBase64String(passwordHash);
            PasswordSalt = Convert.ToBase64String(passwordSalt);
        }
    }
}
