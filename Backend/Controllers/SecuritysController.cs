using Backend.Mappers;
using Backend.Models.Client;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpGet("GetSecuritysList/{date}")]
        public async Task<List<SecurityModel>> GetSecuritysList(String date)
        {
            var securitys = await _securityService.GetAllAsync(date);
            var result = new List<SecurityModel>();

            foreach (var security in securitys)
            {
                result.Add(_mapper.Map(security));
            }

            return result;
        }
    }
}
