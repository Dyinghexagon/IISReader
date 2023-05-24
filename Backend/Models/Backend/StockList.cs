using Backend.Models.Backend.StockModel;

namespace Backend.Models.Backend
{
    public class StockList : Entity
    {
        public string Title { get; set; } = string.Empty;

        public List<ActualStock> Stocks { get; set; } = new List<ActualStock>();

        public bool IsNotificated { get; set; } = true;

        public CalculationType CalculationType { get; set; } = CalculationType.Hormonic;
    }
}
