using Backend.Helpers;

namespace Backend.Models.Backend
{
    public class Account : Entity
    {
        public String Login { get; set; }

        public String PasswordHash { get; set; }

        public String PasswordSalt { get; set; }

        public List<StockList> StockList { get; set; } = new List<StockList>();

        public Account(Guid id, String login, String password, List<StockList> stockList)
        {
            Id = id;
            Login = login;
            CryptoUtils.CreatePasswordHash(password, out Byte[] passwordHash, out Byte[] passwordSalt);
            PasswordHash = Convert.ToBase64String(passwordHash);
            PasswordSalt = Convert.ToBase64String(passwordSalt);
            StockList = stockList;
        }
    }
}
