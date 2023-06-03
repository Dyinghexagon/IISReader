using Backend.Services.ArchiveStockService;
using Quartz;

namespace Backend.Jobs
{
    public class ArchiveStockJob : IJob
    {
        private readonly IArchiveStockService _archiveStockService;
        private readonly ILogger<ArchiveStockJob> _logger;
        
        public ArchiveStockJob(
            IArchiveStockService archiveStockService, 
            ILogger<ArchiveStockJob> logger
        )
        {
            _archiveStockService = archiveStockService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _archiveStockService.UpdateDataAsync();
            _logger.LogInformation("ArchiveStockJob is job!");
        }
    }
}
