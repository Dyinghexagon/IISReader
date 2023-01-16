using Backend.Mappers;
using Backend.Services;
using Backend.Models.Client;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/securitys")]
    public class SecuritysController : ControllerBase
    {
        private readonly ILogger<SecuritysController> _logger;
        private readonly SecurityService _securityService;
        private readonly SecurityMapper _mapper;

        public SecuritysController(
            SecurityService securityService,
            SecurityMapper mapper,
            ILogger<SecuritysController> logger)
        {
            _securityService = securityService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetSecuritysList")]
        public async Task<List<SecurityModel?>> GetSecuritysList()
        {
            var securitys = await _securityService.GetAllAsync();
            var result = new List<SecurityModel?>();

            foreach (var security in securitys)
            {
                result.Add(_mapper.MapSecurity(security));
            }

            return result;
        }

        [HttpGet("GetSecurityChartData/{secid}")]
        public async Task<List<SecurityChartDataModel?>> GetSecurityChartData(String secid)
        {
            var securityChartDataModel = await _securityService.GetSecurityChartData(secid);

            var result = new List<SecurityChartDataModel?>();
            foreach(var security in securityChartDataModel)
            {
                result.Add(_mapper.MapSecurityChartData(security));
            }

            return result;
        }
    }
}
