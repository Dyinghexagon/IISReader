using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class SecurityMapper : IModelMapper
    {

        public SecurityModel? MapSecurity(Security? security)
        {
            return security == null
                ? null
                : new SecurityModel() { 
                    Id = security.Id,
                    SecId = security.SecId,
                    Name = security.Name,
                    ChangePerDay = security.ChangePerDay,
                    CurrentPrice = security.CurrentPrice
                };
        }

        public Security? MapSecurity(SecurityModel? security)
        {
            return security == null
                ? null
                : new Security() {
                    Id = security.Id,
                    SecId = security.SecId,
                    Name = security.Name,
                    CurrentPrice = security.CurrentPrice,
                    ChangePerDay = security.ChangePerDay
                };
        }

        public SecurityChartData? MapSecurityChartData(SecurityChartDataModel securityChartDataModel)
        {
            return securityChartDataModel == null
                ? null
                : new SecurityChartData() {
                    Id = securityChartDataModel.Id,
                    Open = securityChartDataModel.Open,
                    Close = securityChartDataModel.Close,
                    Hight = securityChartDataModel.Hight,
                    Low = securityChartDataModel.Low,
                    Time = securityChartDataModel.Time
                };
        }

        public SecurityChartDataModel? MapSecurityChartData(SecurityChartData securityChartDataModel)
        {
            return securityChartDataModel == null
                ? null
                : new SecurityChartDataModel() {
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
