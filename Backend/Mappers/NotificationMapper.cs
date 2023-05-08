using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class NotificationMapper : IModelMapper
    {
        public Notification Map(NotificationModel notificationModel)
        {
            return new()
            {
                Id = notificationModel.Id,
                Title = notificationModel.Title,
                Description = notificationModel.Description,
                Date = notificationModel.Date,
                SecId = notificationModel.SecId,
                Volume = notificationModel.Volume,
                isReaded = notificationModel.isReaded
            };
        }

        public NotificationModel Map(Notification notification)
        {
            return new() 
            {
                Id = notification.Id,
                Title = notification.Title,
                Description = notification.Description,
                Date = notification.Date,
                SecId = notification.SecId,
                Volume = notification.Volume,
                isReaded = notification.isReaded
            };
        }

        public List<Notification> MapNotificationList(List<NotificationModel> notificationList)
        {
            if (notificationList is null)
            {
                return new();
            }
            var result = new List<Notification>();
            foreach (var notification in notificationList)
            {
                result.Add(Map(notification));
            }
            return result;
        }

        public List<NotificationModel> MapNotificationList(List<Notification> notificationList)
        {
            if (notificationList is null)
            {
                return new();
            }
            var result = new List<NotificationModel>();
            foreach (var notification in notificationList)
            {
                result.Add(Map(notification));
            }
            return result;
        }
    }
}
