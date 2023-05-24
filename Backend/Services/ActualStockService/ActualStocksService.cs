using Fiss.Extensions;
using Fiss.Request;
using Fiss.Response;
using Backend.Models.Backend.StockModel;

namespace Backend.Services.StockService
{
    public class ActualStocksService : IActualStocksService
    {

        public async Task<IList<ActualStock>> GetAllAsync()
        {
            var stocks = new List<ActualStock>();

            var request = new IssRequest();
            var path = "engines/stock/markets/shares/boards/TQBR/securities";
            request.FullPath(path);
            request.AddQuery(new KeyValuePair<string, string>("marketdata.columns", "SECID, LAST, LASTTOPREVPRICE, VOLTODAY"));
            await request.Fetch();
            var respones = request.ToResponse();

            var secidName = GetPairsSecIdName(respones);

            foreach (var row in respones["Marketdata"].Rows.ToList())
            {
                var data = row.Values;
                var secid = data["Secid"].ToString() ?? "";
                stocks.Add(new ActualStock()
                {
                    Id = secid,
                    Name = secidName[secid] ?? "",
                    CurrentPrice = Convert.ToDouble(data["Last"]),
                    ChangePerDay = Convert.ToDouble(data["Lasttoprevprice"]),
                    CurrentVolume = Convert.ToInt64(data["Voltoday"])
                });
            }

            return stocks;
        }

        private static Dictionary<String, String?> GetPairsSecIdName(IDictionary<String, Table> respones)
        {
            var secidName = new Dictionary<String, String?>();

            foreach (var row in respones["Securities"].Rows.ToList())
            {
                var data = row.Values;

                secidName.Add(data["Secid"].ToString() ?? "", data["Secname"].ToString());
            }

            return secidName;
        }

        public async Task<ActualStock?> GetAsync(String secid)
        {
            var stocks = await GetAllAsync();
            return stocks.FirstOrDefault(x => x.Id == secid);
        }
    }

}