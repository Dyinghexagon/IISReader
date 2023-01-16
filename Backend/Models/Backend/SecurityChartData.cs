namespace Backend.Models.Backend
{
    public class SecurityChartData
    {
        public Guid Id { get; set; }
        public Double Open { get; set; }
        public Double Close { get; set; }
        public Double Hight { get; set; }
        public Double Low { get; set; }
        public String Time { get; set; }

        public SecurityChartData(Double open, Double close, Double high, Double low, String time, Guid id)
        {
            Open = open;
            Close = close;
            Hight = high;
            Low = low;
            Time = time;
            Id = id;
        }
    }
}
