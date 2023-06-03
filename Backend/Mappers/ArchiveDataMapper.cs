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

        private List<ArchiveStock> MapData(List<ArchiveStockModel> archiveStockModels)
        {
            var data = new List<ArchiveStock>();

            foreach (var archiveStockModel in archiveStockModels)
            {
                data.Add(new ArchiveStock()
                {
                    Close = archiveStockModel.Close,
                    Hight = archiveStockModel.Hight,
                    Open = archiveStockModel.Open,
                    Low = archiveStockModel.Low,
                    Volumn = archiveStockModel.Volumn
                });
            }

            return data;
        }

        private List<ArchiveStockModel> MapData(List<ArchiveStock> archiveStockModels)
        {
            var data = new List<ArchiveStockModel>();

            foreach (var archiveStockModel in archiveStockModels)
            {
                data.Add(new ArchiveStockModel()
                {
                    Close = archiveStockModel.Close,
                    Hight = archiveStockModel.Hight,
                    Open = archiveStockModel.Open,
                    Low = archiveStockModel.Low,
                    Volumn = archiveStockModel.Volumn
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
