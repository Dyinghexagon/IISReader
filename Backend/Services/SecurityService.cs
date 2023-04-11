using Fiss.Extensions;
using Fiss.Request;
using Backend.Models.Backend;

namespace Backend.Services
{
    public class SecurityService
    {
        public async Task<List<Security>> GetAllAsync()
        {
            var securitys = new List<Security>();


            var request = new IssRequest();
            var path = "engines/stock/markets/shares/boards/TQBR/securities";
            request.FullPath(path);
            request.AddQuery(new KeyValuePair<String, String>("marketdata.columns", "SECID, LAST, LASTTOPREVPRICE"));
            await request.Fetch();
            var respones = request.ToResponse();

            var secidName = GetPairsSecIdName(respones);

            foreach (var row in respones["Marketdata"].Rows.ToList())
            {
                var data = row.Values;
                var secid = data["Secid"].ToString() ?? "";
                securitys.Add(new Security(
                        Guid.NewGuid(),
                        secid,
                        secidName[secid] ?? "",
                        Convert.ToDouble(data["Last"]),
                        Convert.ToDouble(data["Lasttoprevprice"])
                    ));
            }

            return securitys;
        }

        private Dictionary<String, String?> GetPairsSecIdName(IDictionary<String, Fiss.Response.Table> respones)
        {
            var secidName = new Dictionary<String, String?>();

            foreach(var row in respones["Securities"].Rows.ToList())
            {
                var data = row.Values;

                secidName.Add(data["Secid"].ToString() ?? "", data["Secname"].ToString());
            }

            return secidName;
        }

        public async Task<List<SecurityChartData>> GetSecurityChartData(String secid)
        {
            var res = new List<SecurityChartData>();

            var request = new IssRequest();
            var path = $"engines/stock/markets/shares/securities/{secid}/candles";
            request.FullPath(path);
            request.AddQuery(new KeyValuePair<String, String>("interval", "24"));

            await request.Fetch();
            var respones = request.ToResponse();

            foreach (var row in respones["Candles"].Rows.ToList())
            {
                var data = row.Values;
                var date = data["Begin"].ToString() ?? "";

                res.Add(new SecurityChartData(
                    Convert.ToDouble(data["Open"]),
                    Convert.ToDouble(data["Close"]),
                    Convert.ToDouble(data["High"]),
                    Convert.ToDouble(data["Low"]),
                    date,
                    Guid.NewGuid()));

            }

            return res;
        }

    }

}