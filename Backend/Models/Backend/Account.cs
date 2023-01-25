namespace Backend.Models.Backend
{
    public class Account : Entity
    {
        public String Email { get; set; }

        public String Login { get; set; }

        public Byte[] PasswordHash { get; set; }

        public Byte[] PasswordSalt { get; set; }
    }
}
