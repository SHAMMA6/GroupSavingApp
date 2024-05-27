using DomainLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.IServices
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Guid userId, string message);
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(Guid userId);
        Task MarkNotificationAsReadAsync(Guid notificationId);
    }
}
