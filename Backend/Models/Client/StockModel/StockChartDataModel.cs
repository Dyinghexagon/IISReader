namespace Backend.Models.Client.StockModel
{
    public class StockChartDataModel : StockBase
    {
        public double Open { get; set; }

        public double Close { get; set; }

        public double Hight { get; set; }

        public double Low { get; set; }

        public string Time { get; set; } = string.Empty;
    }
}
