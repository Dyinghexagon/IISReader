using Backend.Services;
using Quartz;

namespace Backend.Jobs
{
    public class NotificationJob : IJob
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<NotificationJob> _logger;

        public NotificationJob(IAccountService accountService, ILogger<NotificationJob> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var accounts = await _accountService.GetAllAsync();
            _logger.LogInformation($"{accounts.Count}");
        }
    }
}
