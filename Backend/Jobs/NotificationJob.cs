using Backend.Models.Backend;
using Quartz;

namespace Backend.Jobs
{
    public class NotificationJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
