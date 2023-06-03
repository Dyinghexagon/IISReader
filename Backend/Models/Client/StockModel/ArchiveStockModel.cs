namespace Backend.Models.Client.StockModel
{
    public class ArchiveStockModel
    {
        public double Open { get; set; }

        public double Close { get; set; }

        public double Hight { get; set; }

        public double Low { get; set; }

        public string Time { get; set; } = string.Empty;

        public double Volumn { get; set; }
    }
}
