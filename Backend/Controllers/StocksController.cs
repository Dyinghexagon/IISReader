using Microsoft.AspNetCore.Mvc;
using Backend.Mappers;
using Backend.Services.StockService;
using Backend.Services.ArchiveStockService;
using Backend.Models.Client.StockModel;
using Backend.Models.Client;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StocksController : ControllerBase
    {
        private readonly IActualStocksService _actualStockService;
        private readonly IArchiveStockService _archiveStockService;
        private readonly ActualStockMapper _actualStockMapper;
        private readonly ArchiveDataMapper _archiveDataMapper;
        private readonly ILogger<StocksController> _logger;

        public StocksController(
            IActualStocksService actualStockService,
            IArchiveStockService archiveStockService,
            ActualStockMapper actualStockMapper,
            ArchiveDataMapper archiveDataMapper,
            ILogger<StocksController> logger
        )
        {
            _actualStockService = actualStockService;
            _archiveStockService = archiveStockService;
            _actualStockMapper = actualStockMapper;
            _archiveDataMapper = archiveDataMapper;
            _logger = logger;
        }

        [HttpGet("GetStocksList")]
        public async Task<List<ActualStockModel?>> GetSecuritysList()
        {
            var securitys = await _actualStockService.GetAllAsync();
            var result = new List<ActualStockModel?>();

            foreach (var security in securitys)
            {
                result.Add(_actualStockMapper.Map(security));
            }

            return result;
        }

        [HttpGet("GetArchiveData/{secid}")]
        public async Task<ArhiveDataModel> GetArchiveData(string secid)
        {
            var stock = await _archiveStockService.GetAsync(secid);
            return _archiveDataMapper.Map(stock);
        }

        [HttpGet("GetArchiveData/{secid}/{year}")]
        public async Task<Dictionary<string, ArchiveStockModel>> GetArchiveData(string secid, int year)
        {
            var stocks = await _archiveStockService.GetAsync(secid);
            var stocksModel = _archiveDataMapper.Map(stocks);
            
            var result = new Dictionary<string, ArchiveStockModel>();

            foreach (var stock in stocksModel.Data)
            {
                var date = Convert.ToDateTime(stock.Key);
                if (date.Year == year)
                {
                    result.Add(stock.Key, stock.Value);
                }
            }

            return result;
        }
    }
}
