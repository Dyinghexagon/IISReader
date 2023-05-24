using Backend.Models.Backend.StockModel;
using Backend.Models.Client.StockModel;

namespace Backend.Mappers
{
    public class ActualStockMapper : IModelMapper
    {
        public ActualStockModel Map(ActualStock security)
        {
            return new ActualStockModel()
            {
                Id = security.Id,
                Name = security.Name,
                ChangePerDay = security.ChangePerDay,
                CurrentPrice = security.CurrentPrice,
                CurrentVolume = security.CurrentVolume
            };
        }

        public ActualStock Map(ActualStockModel security)
        {
            return new ActualStock()
            {
                Id = security.Id,
                Name = security.Name,
                CurrentPrice = security.CurrentPrice,
                ChangePerDay = security.ChangePerDay,
                CurrentVolume = security.CurrentVolume
            };
        }

        public List<ActualStockModel> MapToStockListModel(List<ActualStock> stockList)
        {
            var stocks = new List<ActualStockModel>();

            foreach (var stock in stockList)
            {
                var stockModel = Map(stock);
                if (stockModel is not null)
                {
                    stocks.Add(stockModel);
                }
            }

            return stocks;
        }

        public List<ActualStock> MapToStockList(List<ActualStockModel> stockList)
        {
            var stocks = new List<ActualStock>();

            foreach (var stockModel in stockList)
            {
                var stock = Map(stockModel);
                if (stock is not null)
                {
                    stocks.Add(stock);
                }
            }

            return stocks;
        }
    }
}
