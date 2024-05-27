using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.NotificationDTOs
{
    public class GetUserNotificationDTO
    {
       public string userId {  get; set; }
    }

    public class MarkAsReadDto
    {
        public Guid NotificationId { get; set; }
    }

}
