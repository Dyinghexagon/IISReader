using Backend.Models.Client.StockModel;

namespace Backend.Models.Client
{
    public class ArhiveDataModel : StockBase
    {
        public Dictionary<string, ArchiveStockModel> Data { get; set; } = new Dictionary<string, ArchiveStockModel>();

    }
}
