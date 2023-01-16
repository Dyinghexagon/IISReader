using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class SecurityMapper : IModelMapper
    {

        public SecurityMapper() { }

        public SecurityModel? MapSecurity(Security? security)
        {
            return security == null 
                ? null 
                : new SecurityModel(security.Id, security.Secid, security.Name, security.CurrentPrice, security.ChangePerDay);
        }

        public Security? MapSecurity(SecurityModel? security)
        {
            return security == null
                ? null
                : new Security(security.Id, security.Secid, security.Name, security.CurrentPrice, security.ChangePerDay);
        }

        public SecurityChartData? MapSecurityChartData(SecurityChartDataModel securityChartDataModel)
        {
            return securityChartDataModel == null
                ? null
                : new SecurityChartData(
                    securityChartDataModel.Open,
                    securityChartDataModel.Close,
                    securityChartDataModel.Hight,
                    securityChartDataModel.Low,
                    securityChartDataModel.Time,
                    securityChartDataModel.Id);
        }

        public SecurityChartDataModel? MapSecurityChartData(SecurityChartData securityChartDataModel)
        {
            return securityChartDataModel == null
                ? null
                : new SecurityChartDataModel(
                    securityChartDataModel.Open,
                    securityChartDataModel.Close,
                    securityChartDataModel.Hight,
                    securityChartDataModel.Low,
                    securityChartDataModel.Time,
                    securityChartDataModel.Id);
        }
    }
}
