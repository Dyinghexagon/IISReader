using Backend.Models.Client.StockModel;

namespace Backend.Models.Client
{
    public class ArhiveStockModel : StockBase
    {
        public Dictionary<string, ArchiveDataModel> Data { get; set; } = new Dictionary<string, ArchiveDataModel>();

    }
}
