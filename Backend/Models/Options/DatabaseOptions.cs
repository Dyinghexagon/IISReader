namespace Backend.Models.Options
{
    public class DatabaseOptions
    {
        public String AccountsCollectionName { get; set; } = String.Empty;
        public String ArchiveStocksCollectionName { get; set; } = String.Empty;
        public String ConnectionString { get; set; } = String.Empty;
        public String DatabaseName { get; set; } = String.Empty;
    }

}
