using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.NotificationDTOs
{
    public class SendNotificationDTO
    {
       public string userId {  get; set; }
       public string message { get; set; }
    }
}
