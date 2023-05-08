namespace Backend.Models.Client
{
    public class StockModel : Entity
    {
        public String SecId { get; set; } = String.Empty;

        public String Name { get; set; } = String.Empty;

        public Double CurrentPrice { get; set; } = Double.MinValue;

        public Double ChangePerDay { get; set; } = Double.MinValue;

        public Int64 CurrentVolume { get; set; } = Int64.MinValue;

    }
}
