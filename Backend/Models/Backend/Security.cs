namespace Backend.Models.Backend
{
    public class Security : Entity
    {
        public String Secid { get; set; }

        public String Name { get; set; }

        public Double CurrentPrice { get; set; }

        public Double ChangePerDay { get; set; }

        public Security(Guid id, String secid, String name, Double currentPrice, Double changePerDay)
        {
            Id = id;
            Secid = secid;
            Name = name;
            CurrentPrice = currentPrice;
            ChangePerDay = changePerDay;
        }

    }
}
