namespace Backend.Models.Client
{
    public class AccountModel : Entity
    {
        public String Login { get; set; } = String.Empty;

        public String Password { get; set; } = String.Empty;

        public List<StockListModel> StockList { get; set; } = new List<StockListModel>();

    }
}
