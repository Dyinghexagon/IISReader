using Backend.Models.Backend;
using Backend.Models.Backend.StockModel;
using Backend.Models.Client;
using Backend.Models.Client.StockModel;

namespace Backend.Mappers
{
    public class ArchiveDataMapper : IModelMapper
    {
        public ArchiveStock Map(ArhiveStockModel arhiveDataModel)
        {
            return new ArchiveStock
            {
                Id = arhiveDataModel.Id,
                Data = MapData(arhiveDataModel.Data)
            };
        }

        private Dictionary<string, ArchiveData> MapData(Dictionary<string, ArchiveDataModel> archiveStockModels)
        {
            var data = new Dictionary<string, ArchiveData>();

            foreach(var archiveStock in archiveStockModels)
            {
                data.Add(archiveStock.Key, new()
                {
                    Close = archiveStock.Value.Close,
                    Open = archiveStock.Value.Open,
                    Low = archiveStock.Value.Low,
                    Hight = archiveStock.Value.Hight,
                    Volume = archiveStock.Value.Volume
                });
            }

            return data;
        }

        private Dictionary<string, ArchiveDataModel> MapData(Dictionary<string, ArchiveData> archiveStockModels)
        {
            var data = new Dictionary<string, ArchiveDataModel>();

            foreach (var archiveStock in archiveStockModels)
            {
                data.Add(archiveStock.Key, new()
                {
                    Close = archiveStock.Value.Close,
                    Open = archiveStock.Value.Open,
                    Low = archiveStock.Value.Low,
                    Hight = archiveStock.Value.Hight,
                    Volume = archiveStock.Value.Volume
                });
            }


            return data;
        }

        public ArhiveStockModel Map(ArchiveStock arhiveDataModel)
        {
            return new ArhiveStockModel
            {
                Id = arhiveDataModel.Id,
                Data = MapData(arhiveDataModel.Data)
            };
        }
    }
}
