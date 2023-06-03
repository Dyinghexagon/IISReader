using Amazon.Runtime.Internal;
using Backend.Models.Backend.StockModel;
using Backend.Repository.StockRepository;
using Fiss.Extensions;
using Fiss.Request;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Backend.Services.ArchiveStockService
{
    public class ArchiveStockService : IArchiveStockService
    {
        private readonly IArchiveStocksRepository _archiveStocksRepository;

        public ArchiveStockService(IArchiveStocksRepository archiveStocksRepository)
        {
            _archiveStocksRepository = archiveStocksRepository;
        }

        public async Task CreateAsync(ArchiveStock item) => await _archiveStocksRepository.CreateAsync(item);

        public async Task DeleteAsync(String id) => await _archiveStocksRepository.DeleteAsync(id);

        public async Task<IList<ArchiveStock>> GetAllAsync() => await _archiveStocksRepository.GetAllAsync();

        public async Task<ArchiveStock> GetAsync(String id) => await _archiveStocksRepository.GetAsync(id);

        public async Task UpdateAsync(String id, ArchiveStock item) => await _archiveStocksRepository.UpdateAsync(id, item);

        public async Task UpdateDataAsync()
        {
            var secIds = await GetAllSecIdAsync();
            var archiveData = new List<ArchiveStock>();
            var yaers = GetYears();
            var volumnsByYaers = new List<Dictionary<string, double>>();

            foreach(var secId in secIds)
            {
                foreach (var year in yaers)
                {
                    var volumnsByYaer = await GetVolumnsByYearAsync(secId, year);
                    volumnsByYaers.Add(volumnsByYaer);
                }

                var stock = new ArchiveStock()
                {
                    Id = secId,
                    Volumes = volumnsByYaers.SelectMany(dict => dict)
                         .ToLookup(pair => pair.Key, pair => pair.Value)
                         .ToDictionary(group => group.Key, group => group.First())
                };

                var test = await GetAsync(secId);

                if (test is null)
                {
                    await CreateAsync(stock);
                } else
                {
                    await UpdateAsync(stock.Id, stock);
                }

            }
        }

        private async Task<List<String>> GetAllSecIdAsync()
        {
            var secIds = new List<String>();
            var request = new IssRequest();
            var path = "engines/stock/markets/shares/boards/TQBR/securities";
            request.FullPath(path);
            request.AddQuery(new KeyValuePair<string, string>("marketdata.columns", "SECID"));
            await request.Fetch();
            var respones = request.ToResponse();


            foreach (var row in respones["Marketdata"].Rows.ToList())
            {
                var secid = row.Values["Secid"].ToString() ?? "";
                secIds.Add(secid);
            }

            return secIds;
        }

        private List<StockChartData> GetStockChartDataByHalfYear(string secid, IDictionary<String, Fiss.Response.Table> respones)
        {
            var charData = new List<StockChartData>();

            foreach (var row in respones["Candles"].Rows.ToList())
            {
                var data = row.Values;
                var date = data["Begin"].ToString() ?? "";

                charData.Add(new StockChartData()
                {
                    Id = secid,
                    Open = Convert.ToDouble(data["Open"]),
                    Close = Convert.ToDouble(data["Close"]),
                    Hight = Convert.ToDouble(data["High"]),
                    Low = Convert.ToDouble(data["Low"]),
                    Time = date
                });
            }

            return charData;
        }

        private async Task<Dictionary<string, double>> GetVolumnsByYearAsync(string secid, string year)
        {
            var requesFormFirstHalfYear = GetRequest(secid, $"{year}-01-01", $"{year}-06-01");
            var requesFormSecondHalfYear = GetRequest(secid, $"{year}-06-01", $"{year}-12-31");
                        
            await requesFormFirstHalfYear.Fetch();
            var responesFormFirstHalfYear = requesFormFirstHalfYear.ToResponse();

            await requesFormSecondHalfYear.Fetch();
            var responesFormSecondHalfYear = requesFormSecondHalfYear.ToResponse();

            var volumesFormFirstHalfYear = new Dictionary<string, double>(GetVolumnsByHalfYear(responesFormFirstHalfYear));
            var volumesFormSecondHalfYear = new Dictionary<string, double>(GetVolumnsByHalfYear(responesFormSecondHalfYear));

            return volumesFormFirstHalfYear.Union(volumesFormSecondHalfYear)
                    .GroupBy(g => g.Key)
                    .ToDictionary(pair => pair.Key, pair => pair.First().Value);
        }

        private Dictionary<string, double> GetVolumnsByHalfYear(IDictionary<String, Fiss.Response.Table> respones)
        {
            var volumes = new Dictionary<string, double>();
            foreach (var row in respones["Candles"].Rows.ToList())
            {
                var data = row.Values;
                var date = data["Begin"].ToString() ?? "";
                var volume = Convert.ToDouble(data["Volume"]);
                volumes.Add(date, volume);
            }
            return volumes;
        }

        public async Task<List<StockChartData>> GetSecurityChartData(String secid)
        {
            var charData = new List<StockChartData>();
            var years = GetYears();

            foreach (var year in years)
            {
                var requesFormFirstHalfYear = GetRequest(secid, $"{year}-01-01", $"{year}-06-01");
                var requesFormSecondHalfYear = GetRequest(secid, $"{year}-06-01", $"{year}-12-31");

                await requesFormFirstHalfYear.Fetch();
                var responesFormFirstHalfYear = requesFormFirstHalfYear.ToResponse();

                await requesFormSecondHalfYear.Fetch();
                var responesFormSecondHalfYear = requesFormSecondHalfYear.ToResponse();

                charData.AddRange(GetStockChartDataByHalfYear(secid, responesFormFirstHalfYear));
                charData.AddRange(GetStockChartDataByHalfYear(secid, responesFormSecondHalfYear));
            }

            return charData;
        }

        private IssRequest GetRequest(string secid, string from, string till)
        {
            var request = new IssRequest();

            var path = $"engines/stock/markets/shares/securities/{secid}/candles";
            request.FullPath(path);
            request.AddQuery(new KeyValuePair<String, String>("interval", "24"));
            request.AddQuery(new KeyValuePair<String, String>("from", from));
            request.AddQuery(new KeyValuePair<String, String>("till", till));

            return request;
        }

        private List<string> GetYears()
        {
            var years = new List<string>();

            for (var year = 2010; year < DateTime.Now.Year; year++)
            {
                years.Add(year.ToString());
            }

            return years;
        }

    }

}
