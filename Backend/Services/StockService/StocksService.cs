using Fiss.Extensions;
using Fiss.Request;
using Backend.Models.Backend;
using Backend.Repository.StockRepository;
using Fiss.Response;

namespace Backend.Services.StockService
{
    public class StocksService : IStocksService
    {
        private readonly IStocksRepository _stockRepository;

        public StocksService(IStocksRepository accountRepository)
        {
            _stockRepository = accountRepository;
        }

        public async Task<IList<Stock>> GetAllAsync()
        {
            var stocks = new List<Stock>();

            var request = new IssRequest();
            var path = "engines/stock/markets/shares/boards/TQBR/securities";
            request.FullPath(path);
            request.AddQuery(new KeyValuePair<string, string>("marketdata.columns", "SECID, LAST, LASTTOPREVPRICE"));
            await request.Fetch();
            var respones = request.ToResponse();

            var secidName = GetPairsSecIdName(respones);

            foreach (var row in respones["Marketdata"].Rows.ToList())
            {
                var data = row.Values;
                var secid = data["Secid"].ToString() ?? "";
                stocks.Add(new Stock()
                {
                    Id = Guid.NewGuid(),
                    SecId = secid,
                    Name = secidName[secid] ?? "",
                    CurrentPrice = Convert.ToDouble(data["Last"]),
                    ChangePerDay = Convert.ToDouble(data["Lasttoprevprice"])
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

        public async Task<List<StockChartData>> GetSecurityChartData(String secid)
        {
            var charData = new List<StockChartData>();

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

                charData.Add(new StockChartData()
                {
                    Id = Guid.NewGuid(),
                    Open = Convert.ToDouble(data["Open"]),
                    Close = Convert.ToDouble(data["Close"]),
                    Hight = Convert.ToDouble(data["High"]),
                    Low = Convert.ToDouble(data["Low"]),
                    Time = date
                });

            }

            return charData;
        }

        public async Task<Stock?> GetStockBySecId(String secid)
        {
            var stocks = await GetAllAsync();
            return stocks.FirstOrDefault(x => x.SecId == secid);
        }

        public async Task<Stock> GetAsync(Guid id) => await _stockRepository.GetAsync(id);

        public async Task CreateAsync(Stock item) => await _stockRepository.CreateAsync(item);

        public async Task UpdateAsync(Guid id, Stock stock) => await _stockRepository.UpdateAsync(id, stock);

        public async Task DeleteAsync(Guid id) => await _stockRepository.DeleteAsync(id);
    }

}