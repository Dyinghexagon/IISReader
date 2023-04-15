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
    }
}
