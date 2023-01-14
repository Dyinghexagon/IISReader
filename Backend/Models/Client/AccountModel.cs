namespace Backend.Models.Client
{
    public class AccountModel : Entity
    {
        public String Email { get; set; }

        public String Login { get; set; }

        public String Password { get; set; }

        public AccountModel(Guid id, String email, String login, String password)
        {
            Id = id;
            Email = email;
            Login = login;
            Password = password;
        }

    }
}
