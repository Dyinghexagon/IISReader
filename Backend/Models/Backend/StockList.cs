using Backend.Models.Backend.StockModel;

namespace Backend.Models.Backend
{
    public class StockList : Entity, IEquatable<StockList>
    {
        public string Title { get; set; } = string.Empty;

        public List<ActualStock> Stocks { get; set; } = new List<ActualStock>();

        public bool IsNotificated { get; set; } = true;

        public CalculationType CalculationType { get; set; } = CalculationType.Hormonic;

        public int Ratio { get; set; } = 2;

        public bool Equals(StockList? other)
        {
            return other?.Id.Equals(Id) ?? false;
        }
    }
}
