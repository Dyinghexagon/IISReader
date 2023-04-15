using Backend.Services.AccountService;
using Quartz;

namespace Backend.Jobs
{
    public class NotificationJob : IJob
    {
        private readonly IAccountsService _accountService;
        private readonly ILogger<NotificationJob> _logger;

        public NotificationJob(
            IAccountsService accountService, 
            ILogger<NotificationJob> logger
        )
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var accounts = await _accountService.GetAllAsync();
            _logger.LogInformation($"NotificationJob");
        }
    }
}
