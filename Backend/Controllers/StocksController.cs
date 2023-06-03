using Microsoft.AspNetCore.Mvc;
using Backend.Mappers;
using Backend.Services.StockService;
using Backend.Services.ArchiveStockService;
using Backend.Models.Client.StockModel;
using Backend.Models.Client;

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

        public StocksController(
            IActualStocksService actualStockService,
            IArchiveStockService archiveStockService,
            ActualStockMapper actualStockMapper,
            ArchiveDataMapper archiveDataMapper
        )
        {
            _actualStockService = actualStockService;
            _archiveStockService = archiveStockService;
            _actualStockMapper = actualStockMapper;
            _archiveDataMapper = archiveDataMapper;
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

        [HttpGet("GetArchiveData/{secid:string}")]
        public async Task<ArhiveDataModel> GetArchiveData(string secid)
        {
            return new ArhiveDataModel();
        }

        [HttpGet("InitArchiveStock")]
        public async Task InitArchiveStock()
        {
            await _archiveStockService.UpdateDataAsync();
        }

    }
}
