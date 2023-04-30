namespace Backend.Models.Backend
{
    public class StockList : Entity
    {
        public String Title { get; set; } = String.Empty;

        public List<Stock>? Stocks { get; set; } = new List<Stock>();

        public Boolean IsNotificated { get; set; } = true;
    }
}
