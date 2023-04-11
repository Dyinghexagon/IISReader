namespace Backend.Models.Backend
{
    public class Notification : Entity
    {
        public String SecId { get; set; } = String.Empty;

        public Double Volume { get; set; } = Double.MinValue;

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
