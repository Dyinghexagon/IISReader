using Backend.Models.Client;
using Microsoft.AspNetCore.Mvc;
using Backend.Mappers;
using Backend.Services.StockService;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StocksController : ControllerBase
    {
        private readonly IStocksService _securityService;
        private readonly StockMapper _mapper;

        public StocksController(
            IStocksService securityService,
            StockMapper mapper
        )
        {
            _securityService = securityService;
            _mapper = mapper;
        }

        [HttpGet("GetSecuritysList")]
        public async Task<List<StockModel?>> GetSecuritysList()
        {
            var securitys = await _securityService.GetAllAsync();
            var result = new List<StockModel?>();

            foreach (var security in securitys)
            {
                result.Add(_mapper.MapSecurity(security));
            }

            return result;
        }

        [HttpGet("GetSecurityChartData/{secid}")]
        public async Task<List<StockChartDataModel?>> GetSecurityChartData(String secid)
        {
            var securityChartDataModel = await _securityService.GetSecurityChartData(secid);

            var result = new List<StockChartDataModel?>();
            foreach(var security in securityChartDataModel)
            {
                result.Add(_mapper.MapSecurityChartData(security));
            }

            return result;
        }

        [HttpGet("FillingStocks")]
        public async Task FillingStocks()
        {
            await _securityService.FillingStocksAsync();
        }
    }
}
