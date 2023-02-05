using Backend.Helpers;

namespace Backend.Models.Backend
{
    public class Account : Entity
    {
        public String Login { get; set; }

        public Byte[] PasswordHash { get; set; }

        public Byte[] PasswordSalt { get; set; }

        public Account(Guid id, String login, String password)
        {
            Id = id;
            Login = login;
            PasswordHash = CryptoHelper.CreatePasswordHash(password);
            PasswordSalt = CryptoHelper.CreatePasswordSalt(password);
        }
    }
}
