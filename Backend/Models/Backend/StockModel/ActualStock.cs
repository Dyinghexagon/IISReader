namespace Backend.Models.Backend.StockModel
{
    public class ActualStock : StockBase
    {
        public string Name { get; set; } = string.Empty;

        public double CurrentPrice { get; set; } = double.MinValue;

        public double ChangePerDay { get; set; } = double.MinValue;

        public long CurrentVolume { get; set; } = long.MinValue;
    }
}
