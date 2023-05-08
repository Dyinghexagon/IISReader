namespace Backend.Models.Backend
{
    public class Stock : Entity, IEquatable<Stock>
    {
        public String SecId { get; set; } = String.Empty;

        public String Name { get; set; } = String.Empty;

        public Double CurrentPrice { get; set; } = Double.MinValue;

        public Double ChangePerDay { get; set; } = Double.MinValue;

        public Int64 CurrentVolume { get; set; } = Int64.MinValue;

        public bool Equals(Stock? other)
        {
            return SecId.Equals(other?.SecId);
        }
    }
}
