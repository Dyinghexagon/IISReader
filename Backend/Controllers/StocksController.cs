using Microsoft.AspNetCore.Mvc;
using Backend.Mappers;
using Backend.Services.StockService;
using Backend.Services.ArchiveStockService;
using Backend.Models.Client.StockModel;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StocksController : ControllerBase
    {
        private readonly IActualStocksService _actualStockService;
        private readonly IArchiveStockService _archiveStockService;
        private readonly ActualStockMapper _actualStockMapper;
        private readonly StockChartDataMapper _archiveStockMapper;

        public StocksController(
            IActualStocksService actualStockService,
            IArchiveStockService archiveStockService,
            ActualStockMapper actualStockMapper,
            StockChartDataMapper archiveStockMapper
        )
        {
            _actualStockService = actualStockService;
            _archiveStockService = archiveStockService;
            _actualStockMapper = actualStockMapper;
            _archiveStockMapper = archiveStockMapper;
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

        [HttpGet("GetStockChartData/{secid}")]
        public async Task<List<StockChartDataModel?>> GetSecurityChartData(String secid)
        {
            var securityChartDataModel = await _archiveStockService.GetSecurityChartData(secid);

            var result = new List<StockChartDataModel?>();
            foreach(var security in securityChartDataModel)
            {
                result.Add(_archiveStockMapper.Map(security));
            }

            return result;
        }

        [HttpGet("InitArchiveStock")]
        public async Task InitArchiveStock()
        {
            await _archiveStockService.UpdateDataAsync();
        }

    }
}
