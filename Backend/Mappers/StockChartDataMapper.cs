using Backend.Models.Backend.StockModel;
using Backend.Models.Client.StockModel;

namespace Backend.Mappers
{
    public class StockChartDataMapper : IModelMapper
    {
        public StockChartData Map(StockChartDataModel stockChartDataModel)
        {
            return new StockChartData()
            {
                Id = stockChartDataModel.Id,
                Open = stockChartDataModel.Open,
                Close = stockChartDataModel.Close,
                Hight = stockChartDataModel.Hight,
                Low = stockChartDataModel.Low,
                Time = stockChartDataModel.Time,
            };
        }

        public StockChartDataModel Map(StockChartData stockChartDataModel)
        {
            return new StockChartDataModel()
            {
                Id = stockChartDataModel.Id,
                Open = stockChartDataModel.Open,
                Close = stockChartDataModel.Close,
                Hight = stockChartDataModel.Hight,
                Low = stockChartDataModel.Low,
                Time = stockChartDataModel.Time,
            };
        }
    }
}
