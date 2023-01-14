namespace Backend.Models.Backend
{
    public class Account : Entity
    {
        public String Email { get; set; }

        public String Login { get; set; }

        public String Password { get; set; }

        public Account(Guid id, String email, String login, String password) { 
            Id = id;
            Email = email;
            Login = login;
            Password = password;
        }

    }
}
