using MongoDB.Bson;
using Backend.Models.Client;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Backend.Models.Backend
{
    public class Account
    {
        public Guid Id { get; set; }

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
