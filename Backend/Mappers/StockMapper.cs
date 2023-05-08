using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class StockMapper : IModelMapper
    {
        public StockModel Map(Stock security)
        {
            return new StockModel()
            {
                Id = security.Id,
                SecId = security.SecId,
                Name = security.Name,
                ChangePerDay = security.ChangePerDay,
                CurrentPrice = security.CurrentPrice,
                CurrentVolume = security.CurrentVolume
            };
        }

        public Stock Map(StockModel security)
        {
            return new Stock()
            {
                Id = security.Id,
                SecId = security.SecId,
                Name = security.Name,
                CurrentPrice = security.CurrentPrice,
                ChangePerDay = security.ChangePerDay,
                CurrentVolume = security.CurrentVolume
            };
        }

        public StockChartData MapChat(StockChartDataModel stockChartDataModel)
        {
            return new StockChartData()
            {
                Id = stockChartDataModel.Id,
                Open = stockChartDataModel.Open,
                Close = stockChartDataModel.Close,
                Hight = stockChartDataModel.Hight,
                Low = stockChartDataModel.Low,
                Time = stockChartDataModel.Time
            };
        }

        public StockChartDataModel MapChat(StockChartData stockChartDataModel)
        {
            return new StockChartDataModel()
            {
                Id = stockChartDataModel.Id,
                Open = stockChartDataModel.Open,
                Close = stockChartDataModel.Close,
                Hight = stockChartDataModel.Hight,
                Low = stockChartDataModel.Low,
                Time = stockChartDataModel.Time
            };
        }

        public List<StockModel> MapToStockListModel(List<Stock> stockList)
        {
            var stocks = new List<StockModel>();

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

        public List<Stock> MapToStockList(List<StockModel> stockList)
        {
            var stocks = new List<Stock>();

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
