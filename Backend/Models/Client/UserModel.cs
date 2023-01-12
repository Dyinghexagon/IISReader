using Backend.Models.Backend;

namespace Backend.Models.Client
{
    public class UserModel
    {
        public String Id { get; set; }
        public String Email { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }

        public UserModel(String id, String email, String login, String password)
        {
            Id = id;
            Email = email;
            Login = login;
            Password = password;
        }

        public static UserModel From(User user)
        {
            return new UserModel(user.Id, user.Email, user.Login, user.Password);
        }
    }
}
