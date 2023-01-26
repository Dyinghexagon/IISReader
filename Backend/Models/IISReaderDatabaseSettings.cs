namespace Backend.Models
{
    public class IISReaderDatabaseOption
    {
        public string AccountsCollectionName { get; set; } = null!;
        public string SecuritysCollectionName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }

}
