using Backend.Models.Backend;
using Backend.Models.Backend.StockModel;
using Backend.Repository.StockRepository;
using Fiss.Extensions;
using Fiss.Request;

namespace Backend.Services.ArchiveStockService
{
    public class ArchiveStockService : IArchiveStockService
    {
        private readonly IArchiveStocksRepository _archiveStocksRepository;
        private readonly ILogger<ArchiveStockService> _logger;

        public ArchiveStockService(
            IArchiveStocksRepository archiveStocksRepository,
            ILogger<ArchiveStockService> logger
        )
        {
            _archiveStocksRepository = archiveStocksRepository;
            _logger = logger;
        }

        public async Task CreateAsync(ArchiveStock item) => await _archiveStocksRepository.CreateAsync(item);

        public async Task DeleteAsync(String id) => await _archiveStocksRepository.DeleteAsync(id);

        public async Task<IList<ArchiveStock>> GetAllAsync() => await _archiveStocksRepository.GetAllAsync();

        public async Task<ArchiveStock> GetAsync(String id) => await _archiveStocksRepository.GetAsync(id);

        public async Task UpdateAsync(String id, ArchiveStock item) => await _archiveStocksRepository.UpdateAsync(id, item);

        public async Task PrepareForDataUpdateAsync()
        {
            var stocks = await _archiveStocksRepository.GetAllAsync();
            foreach(var stock in stocks)
            {
                stock.IsUpdated = false;
                await UpdateAsync(stock.Id, stock);
            }
        }

        public async Task UpdateDataAsync()
        {
            var secIds = await GetAllSecIdAsync();
            var yaers = GetYears();
            var archiveDataList = new List<ArchiveStock>();

            foreach (var secId in secIds)
            {
                var dataByYears = new List<Dictionary<string, ArchiveData>>();
                foreach (var yaer in yaers)
                {
                    var data = await GetArchiveDataByYearAsync(secId, yaer);
                    dataByYears.Add(data);
                }

                var archiveStock = new ArchiveStock()
                {
                    Id = secId,
                    Data = dataByYears.SelectMany(dict => dict)
                         .ToLookup(pair => pair.Key, pair => pair.Value)
                         .ToDictionary(group => group.Key, group => group.First()),
                    IsUpdated = true
                };

                try
                {
                    var stockCheck = await GetAsync(secId);

                    if (stockCheck is null)
                    {
                        await CreateAsync(archiveStock);
                        _logger.LogInformation($"Stock - {archiveStock.Id} is create and add to database!");
                    }
                    else if (!stockCheck.IsUpdated)
                    {
                        await UpdateAsync(secId, archiveStock);
                        _logger.LogInformation($"Stock - {archiveStock.Id} is updated!");
                    }
                } catch(Exception ex)
                {
                    _logger.LogError($"Update stock - {archiveStock.Id} is fail!\n{ex.StackTrace}");
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

        private async Task<Dictionary<string, ArchiveData>> GetArchiveDataByYearAsync(string secid, string year)
        {
            try
            {
                var requesFormFirstHalfYear = GetRequest(secid, $"{year}-01-01", $"{year}-06-01");
                var requesFormSecondHalfYear = GetRequest(secid, $"{year}-06-01", $"{year}-12-31");

                await requesFormFirstHalfYear.Fetch();
                var responesFormFirstHalfYear = requesFormFirstHalfYear.ToResponse();

                await requesFormSecondHalfYear.Fetch();
                var responesFormSecondHalfYear = requesFormSecondHalfYear.ToResponse();

                var dataFormFormFirstHalfYear = GetArchiveStocksByHalfYear(responesFormFirstHalfYear);
                var dataFormSecondHalfYear = GetArchiveStocksByHalfYear(responesFormSecondHalfYear);

                return dataFormFormFirstHalfYear.Union(dataFormSecondHalfYear)
                        .GroupBy(g => g.Key)
                        .ToDictionary(pair => pair.Key, pair => pair.First().Value);
            } catch(Exception ex)
            {
                _logger.LogError($"Fail request data!\n{ex.StackTrace}");
                return new Dictionary<string, ArchiveData>();
            }

        }

        private Dictionary<string, ArchiveData> GetArchiveStocksByHalfYear(IDictionary<String, Fiss.Response.Table> respones)
        {
            var archiveStock = new Dictionary<string, ArchiveData>();
            foreach (var row in respones["Candles"].Rows.ToList())
            {
                var data = row.Values;
                var date = data["Begin"].ToString() ?? "";

                archiveStock.Add(date, new()
                {
                    Open = Convert.ToDouble(data["Open"]),
                    Close = Convert.ToDouble(data["Close"]),
                    Hight = Convert.ToDouble(data["High"]),
                    Low = Convert.ToDouble(data["Low"]),
                    Volume = Convert.ToDouble(data["Volume"])
                });
            }

            return archiveStock;
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

            for (var year = 2010; year <= DateTime.Now.Year; year++)
            {
                years.Add(year.ToString());
            }

            return years;
        }

    }

}
