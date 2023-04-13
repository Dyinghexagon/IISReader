using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class SecurityMapper : IModelMapper
    {

        public StockModel? MapSecurity(Stock? security)
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

        public Stock? MapSecurity(StockModel? security)
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

        public StockChartData? MapSecurityChartData(StockChartDataModel securityChartDataModel)
        {
            return securityChartDataModel == null
                ? null
                : new StockChartData() {
                    Id = securityChartDataModel.Id,
                    Open = securityChartDataModel.Open,
                    Close = securityChartDataModel.Close,
                    Hight = securityChartDataModel.Hight,
                    Low = securityChartDataModel.Low,
                    Time = securityChartDataModel.Time
                };
        }

        public StockChartDataModel? MapSecurityChartData(StockChartData securityChartDataModel)
        {
            return securityChartDataModel == null
                ? null
                : new StockChartDataModel() {
                    Id = securityChartDataModel.Id,
                    Open = securityChartDataModel.Open,
                    Close = securityChartDataModel.Close,
                    Hight = securityChartDataModel.Hight,
                    Low = securityChartDataModel.Low,
                    Time = securityChartDataModel.Time
                };
        }
    }
}
