namespace Backend.Models.Backend
{
    public class Security : Entity
    {
        public String SecId { get; set; } = String.Empty;

        public String Name { get; set; } = String.Empty;

        public Double CurrentPrice { get; set; } = Double.MinValue;

        public Double ChangePerDay { get; set; } = Double.MinValue;

    }
}
