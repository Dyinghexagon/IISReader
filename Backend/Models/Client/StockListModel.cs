using Backend.Models.Client.StockModel;

namespace Backend.Models.Client
{
    public class StockListModel : Entity
    {
        public string Title { get; set; } = string.Empty;

        public List<ActualStockModel> Stocks { get; set; } = new List<ActualStockModel>();

        public bool IsNotificated { get; set; } = true;

    }
}
