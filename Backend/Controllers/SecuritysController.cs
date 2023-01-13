using Backend.Mappers;
using Backend.Models.Client;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("securitysApi")]
    public class SecuritysController : Controller
    {
        private readonly ILogger<SecuritysController> _logger;
        private readonly SecurityMapper _mapperl;

        public SecuritysController(ILogger<SecuritysController> logger, SecurityMapper mapper)
        {
            _logger = logger;
            _mapperl = mapper;
        }

        [HttpGet]
        public Task<List<SecurityModel>> GetSecuritysList()
        {
            var securitys = new List<SecurityModel>();

            securitys.Add(new SecurityModel("GAZP", "Газпром", 100, 1));
            securitys.Add(new SecurityModel("LKON", "Лукойл", 4077.5, -0.1));
            securitys.Add(new SecurityModel("YNDX", "Яндекс", 1902.4, 0.24));

            return Task.FromResult(securitys);
        }
    }
}
