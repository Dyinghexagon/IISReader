﻿namespace Backend.Models
{
    public class IISReaderDatabaseOptions
    {
        public string AccountsCollectionName { get; set; } = null!;
        public string StocksCollectionName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }

}
