namespace Backend.Models.Client
{
    public class SecurityModel
    {
        public String Tiket { get; set; }
        public String Name { get; set; }
        public Double CurrentPrice { get; set; }
        public Double ChangePerDay { get; set; }

        public SecurityModel(string tiket, string name, double currentPrice, double changePerDay)
        {
            Tiket = tiket;
            Name = name;
            CurrentPrice = currentPrice;
            ChangePerDay = changePerDay;
        }
    }
}
