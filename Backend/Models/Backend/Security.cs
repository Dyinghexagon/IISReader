namespace Backend.Models.Backend
{
    public class Security
    {
        public String Tiket { get; set; }
        public String Name { get; set; }
        public Double CurrentPrice { get; set; }
        public Double ChangePerDay { get; set; }

        public Security(string tiket, string name, double currentPrice, double changePerDay)
        {
            Tiket = tiket;
            Name = name;
            CurrentPrice = currentPrice;
            ChangePerDay = changePerDay;
        }
    }
}
