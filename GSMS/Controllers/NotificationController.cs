using ApplicationLayer.DTOs.NotificationDTOs;
using ApplicationLayer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presintationlayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            try
            {
                var userId = GetUserIdFromToken(); // Implement this method to get user ID from JWT token
                var notifications = await _notificationService.GetUserNotificationsAsync(userId);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("mark-as-read")]
        public async Task<IActionResult> MarkAsRead(MarkAsReadDto markAsReadDto)
        {
            try
            {
                await _notificationService.MarkNotificationAsReadAsync(markAsReadDto.NotificationId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Guid GetUserIdFromToken()
        {
            // Implement logic to extract user ID from JWT token
            return Guid.NewGuid();
        }
    }
}
