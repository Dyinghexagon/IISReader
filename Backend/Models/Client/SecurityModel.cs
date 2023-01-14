namespace Backend.Models.Client
{
    public class SecurityModel : Entity
    {
        public String Tiket { get; set; }
        public String Name { get; set; }
        public Double CurrentPrice { get; set; }
        public Double ChangePerDay { get; set; }

        public SecurityModel(Guid id, String tiket, String name, Double currentPrice, Double changePerDay)
        {
            Id = id;
            Tiket = tiket;
            Name = name;
            CurrentPrice = currentPrice;
            ChangePerDay = changePerDay;
        }
    }
}
