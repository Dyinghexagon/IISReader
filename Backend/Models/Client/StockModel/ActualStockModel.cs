namespace Backend.Models.Client.StockModel
{
    public class ActualStockModel : StockBase
    {

        public string Name { get; set; } = string.Empty;

        public double CurrentPrice { get; set; } = double.MinValue;

        public double ChangePerDay { get; set; } = double.MinValue;

        public long CurrentVolume { get; set; } = long.MinValue;

    }
}
