using MongoDB.Bson;
using Backend.Models.Client;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Backend.Models.Backend
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        [BsonElement("Email")]
        [JsonPropertyName("Email")]
        public String Email { get; set; }

        [BsonElement("Login")]
        public String Login { get; set; }

        [BsonElement("Password")]
        public String Password { get; set; }

        public User(String id, String email, String login, String password) { 
            Id = id;
            Email = email;
            Login = login;
            Password = password;
        }

        public static User From(UserModel user)
        {
            return new User(user.Id, user.Email, user.Login, user.Password);
        }
    }
}
