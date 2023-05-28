using Backend.Models.Backend.StockModel;
using Backend.Models.Client.StockModel;

namespace Backend.Mappers
{
    public class ActualStockMapper : IModelMapper
    {
        public ActualStockModel Map(ActualStock actualStock)
        {
            return new ActualStockModel()
            {
                Id = actualStock.Id,
                Name = actualStock.Name,
                ChangePerDay = actualStock.ChangePerDay,
                CurrentPrice = actualStock.CurrentPrice,
                CurrentVolume = actualStock.CurrentVolume,
                CalculationType = actualStock.CalculationType
            };
        }

        public ActualStock Map(ActualStockModel actualStockModel)
        {
            return new ActualStock()
            {
                Id = actualStockModel.Id,
                Name = actualStockModel.Name,
                CurrentPrice = actualStockModel.CurrentPrice,
                ChangePerDay = actualStockModel.ChangePerDay,
                CurrentVolume = actualStockModel.CurrentVolume,
                CalculationType = actualStockModel.CalculationType
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
