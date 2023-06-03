using Backend.Models.Backend;
using Backend.Models.Backend.StockModel;
using Backend.Models.Client;
using Backend.Models.Client.StockModel;

namespace Backend.Mappers
{
    public class ArchiveDataMapper : IModelMapper
    {
        public ArchiveData Map(ArhiveDataModel arhiveDataModel)
        {
            return new ArchiveData
            {
                Id = arhiveDataModel.Id,
                Data = MapData(arhiveDataModel.Data)
            };
        }

        private Dictionary<string, ArchiveStock> MapData(Dictionary<string, ArchiveStockModel> archiveStockModels)
        {
            var data = new Dictionary<string, ArchiveStock>();

            foreach(var archiveStock in archiveStockModels)
            {
                data.Add(archiveStock.Key, new()
                {
                    Close = archiveStock.Value.Close,
                    Open = archiveStock.Value.Open,
                    Low = archiveStock.Value.Low,
                    Hight = archiveStock.Value.Hight,
                    Volumn = archiveStock.Value.Volumn
                });
            }

            return data;
        }

        private Dictionary<string, ArchiveStockModel> MapData(Dictionary<string, ArchiveStock> archiveStockModels)
        {
            var data = new Dictionary<string, ArchiveStockModel>();

            foreach (var archiveStock in archiveStockModels)
            {
                data.Add(archiveStock.Key, new()
                {
                    Close = archiveStock.Value.Close,
                    Open = archiveStock.Value.Open,
                    Low = archiveStock.Value.Low,
                    Hight = archiveStock.Value.Hight,
                    Volumn = archiveStock.Value.Volumn
                });
            }


            return data;
        }

        public ArhiveDataModel Map(ArchiveData arhiveDataModel)
        {
            return new ArhiveDataModel
            {
                Id = arhiveDataModel.Id,
                Data = MapData(arhiveDataModel.Data)
            };
        }
    }
}
