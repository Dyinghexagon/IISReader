namespace Backend.Models.Client
{
    public class StockListModel : Entity
    {
        public String Title { get; set; } = String.Empty;

        public List<StockModel>? Stocks { get; set; } = new List<StockModel>();

        public Boolean IsNotificated { get; set; } = true;

    }
}
