using Backend.Models.Backend.StockModel;

namespace Backend.Models.Backend
{
    public class Notification : Entity
    {
        public String Title { get; set; } = String.Empty;

        public String Description { get; set; } = String.Empty;

        public String SecId { get; set; } = String.Empty;

        public Double Volume { get; set; } = Double.MinValue;

        public DateTime Date { get; set; } = DateTime.Now;

        public Boolean isReaded { get; set; } = false;
    }
}
