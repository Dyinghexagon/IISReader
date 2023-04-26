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
        private readonly IStocksService _stockService;
        private readonly StockMapper _mapper;

        public StocksController(
            IStocksService securityService,
            StockMapper mapper
        )
        {
            _stockService = securityService;
            _mapper = mapper;
        }

        [HttpGet("GetStocksList")]
        public async Task<List<StockModel?>> GetSecuritysList()
        {
            var securitys = await _stockService.GetAllAsync();
            var result = new List<StockModel?>();

            foreach (var security in securitys)
            {
                result.Add(_mapper.Map(security));
            }

            return result;
        }

        [HttpGet("GetStockChartData/{secid}")]
        public async Task<List<StockChartDataModel?>> GetSecurityChartData(String secid)
        {
            var securityChartDataModel = await _stockService.GetSecurityChartData(secid);

            var result = new List<StockChartDataModel?>();
            foreach(var security in securityChartDataModel)
            {
                result.Add(_mapper.MapChat(security));
            }

            return result;
        }

    }
}
