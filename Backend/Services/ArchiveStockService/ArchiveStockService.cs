using Backend.Models.Backend.StockModel;
using Backend.Repository.StockRepository;
using Fiss.Extensions;
using Fiss.Request;

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
            foreach(var secId in secIds)
            {
                var rawArchiveData = await GetRawArchiveDataAsync(secId);
                var volumes = new Dictionary<string, double>();
                foreach (var row in rawArchiveData["Candles"].Rows.ToList())
                {
                    var data = row.Values;
                    var date = data["Begin"].ToString() ?? "";
                    var volume = Convert.ToDouble(data["Volume"]);
                    volumes.Add(date, volume);
                }

                archiveData.Add(new ArchiveStock() { 
                    Id = secId, 
                    Volumes = new Dictionary<string, double>(volumes)
                });
            }

            foreach (var item in archiveData)
            {
                await UpdateAsync(item.Id, item);
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

        public async Task<List<StockChartData>> GetSecurityChartData(String secid)
        {
            var charData = new List<StockChartData>();
            var respones = await GetRawArchiveDataAsync(secid);

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

        private async Task<IDictionary<String, Fiss.Response.Table>> GetRawArchiveDataAsync(String secid)
        {

            var request = new IssRequest();
            var path = $"engines/stock/markets/shares/securities/{secid}/candles";

            request.FullPath(path);
            request.AddQuery(new KeyValuePair<String, String>("interval", "24"));

            await request.Fetch();
            var respones = request.ToResponse();

            return respones;
        }
    }
}
