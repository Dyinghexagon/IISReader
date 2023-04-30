using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class StockMapper : IModelMapper
    {
        public StockModel? Map(Stock? security)
        {
            return security == null
                ? null
                : new StockModel() { 
                    Id = security.Id,
                    SecId = security.SecId,
                    Name = security.Name,
                    ChangePerDay = security.ChangePerDay,
                    CurrentPrice = security.CurrentPrice
                };
        }

        public Stock? Map(StockModel? security)
        {
            return security == null
                ? null
                : new Stock() {
                    Id = security.Id,
                    SecId = security.SecId,
                    Name = security.Name,
                    CurrentPrice = security.CurrentPrice,
                    ChangePerDay = security.ChangePerDay
                };
        }

        public StockChartData? MapChat(StockChartDataModel stockChartDataModel)
        {
            return stockChartDataModel == null
                ? null
                : new StockChartData() {
                    Id = stockChartDataModel.Id,
                    Open = stockChartDataModel.Open,
                    Close = stockChartDataModel.Close,
                    Hight = stockChartDataModel.Hight,
                    Low = stockChartDataModel.Low,
                    Time = stockChartDataModel.Time
                };
        }

        public StockChartDataModel? MapChat(StockChartData stockChartDataModel)
        {
            return stockChartDataModel == null
                ? null
                : new StockChartDataModel() {
                    Id = stockChartDataModel.Id,
                    Open = stockChartDataModel.Open,
                    Close = stockChartDataModel.Close,
                    Hight = stockChartDataModel.Hight,
                    Low = stockChartDataModel.Low,
                    Time = stockChartDataModel.Time
                };
        }

        public List<StockModel>? MapToStockListModel(List<Stock>? stockList)
        {
            if (stockList is null)
            {
                return null;
            }

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

        public List<Stock>? MapToStockList(List<StockModel>? stockList)
        {
            if (stockList is null)
            {
                return null;
            }

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
