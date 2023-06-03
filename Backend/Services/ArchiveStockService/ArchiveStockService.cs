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

        public ArchiveStockService(IArchiveStocksRepository archiveStocksRepository)
        {
            _archiveStocksRepository = archiveStocksRepository;
        }

        public async Task CreateAsync(ArchiveData item) => await _archiveStocksRepository.CreateAsync(item);

        public async Task DeleteAsync(String id) => await _archiveStocksRepository.DeleteAsync(id);

        public async Task<IList<ArchiveData>> GetAllAsync() => await _archiveStocksRepository.GetAllAsync();

        public async Task<ArchiveData> GetAsync(String id) => await _archiveStocksRepository.GetAsync(id);

        public async Task UpdateAsync(String id, ArchiveData item) => await _archiveStocksRepository.UpdateAsync(id, item);

        public async Task UpdateDataAsync()
        {
            var secIds = await GetAllSecIdAsync();
            var yaers = GetYears();

            foreach (var secId in secIds)
            {
                foreach(var yaer in yaers)
                {
                    var archiveData = await GetArchiveDataByYearAsync(secId, yaer);
                    await CreateAsync(archiveData);
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

        private async Task<ArchiveData> GetArchiveDataByYearAsync(string secid, string year)
        {
            var archiveData = new ArchiveData();

            var requesFormFirstHalfYear = GetRequest(secid, $"{year}-01-01", $"{year}-06-01");
            var requesFormSecondHalfYear = GetRequest(secid, $"{year}-06-01", $"{year}-12-31");
                        
            await requesFormFirstHalfYear.Fetch();
            var responesFormFirstHalfYear = requesFormFirstHalfYear.ToResponse();

            await requesFormSecondHalfYear.Fetch();
            var responesFormSecondHalfYear = requesFormSecondHalfYear.ToResponse();


            return archiveData;
        }

        private List<ArchiveStock> GetArchiveStocksByHalfYear(IDictionary<String, Fiss.Response.Table> respones)
        {
            var archiveStock = new List<ArchiveStock>();
            foreach (var row in respones["Marketdata"].Rows.ToList())
            {
                var data = row.Values;
                var date = data["Begin"].ToString() ?? "";

                archiveStock.Add(new()
                {
                    Open = Convert.ToDouble(data["Open"]),
                    Close = Convert.ToDouble(data["Close"]),
                    Hight = Convert.ToDouble(data["High"]),
                    Low = Convert.ToDouble(data["Low"]),
                    Time = date,
                    Volumn = 
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

            for (var year = 2010; year < DateTime.Now.Year; year++)
            {
                years.Add(year.ToString());
            }

            return years;
        }

    }

}
