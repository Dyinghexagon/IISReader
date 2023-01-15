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

            var secidName = await GetPairsSecIdName();

            var request = new IssRequest();
            var path = "engines/stock/markets/shares/boards/TQBR/securities";
            request.FullPath(path);
            request.AddQuery(new KeyValuePair<String, String>("marketdata.columns", "SECID, LAST, LASTTOPREVPRICE"));
            await request.Fetch();
            var respones = request.ToResponse();

            foreach (var data in respones["Marketdata"].Rows.ToList())
            {
                var secid = data.Values["Secid"].ToString() ?? "";
                securitys.Add(new Security(
                        Guid.NewGuid(),
                        secid,
                        secidName[secid] ?? "",
                        Convert.ToDouble(data.Values["Last"]),
                        Convert.ToDouble(data.Values["Lasttoprevprice"])
                    ));
            }

            return securitys;
        }

        private async Task<Dictionary<String, String?>> GetPairsSecIdName()
        {
            var secidName = new Dictionary<String, String?>();

            var request = new IssRequest();
            var path = "engines/stock/markets/shares/boards/TQBR/securities";
            request.FullPath(path);
            request.AddQuery(new KeyValuePair<String, String>("securities.columns", "SECID,SECNAME"));
            await request.Fetch();
            var respones = request.ToResponse();

            foreach(var secur in respones["Securities"].Rows.ToList())
            {
                secidName.Add(secur.Values["Secid"].ToString(), secur.Values["Secname"].ToString());
            }

            return secidName;
        }

    }
}