using Backend.Models.Backend;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs.NotificationHub
{
    public class NotificationHub : Hub<Notification>
    {
    }
}
