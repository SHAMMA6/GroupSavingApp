using ApplicationLayer.DTOs.GroupSavingPlanDTOs;
using ApplicationLayer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presintationlayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupSavingPlanController : ControllerBase
    {
        private readonly IGroupSavingPlanService _groupSavingPlanService;

        public GroupSavingPlanController(IGroupSavingPlanService groupSavingPlanService)
        {
            _groupSavingPlanService = groupSavingPlanService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateGroupSavingPlanDto createGroupSavingPlanDto)
        {
            try
            {
                var userId = GetUserIdFromToken(); // Implement this method to get user ID from JWT token
                var groupSavingPlan = await _groupSavingPlanService.CreateGroupSavingPlanAsync(userId, createGroupSavingPlanDto.Name, createGroupSavingPlanDto.TotalAmount, createGroupSavingPlanDto.NumberOfParticipants, createGroupSavingPlanDto.DurationInMonths);
                return Ok(groupSavingPlan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("join")]
        public async Task<IActionResult> Join(JoinGroupSavingPlanDto joinGroupSavingPlanDto)
        {
            try
            {
                var userId = GetUserIdFromToken(); // Implement this method to get user ID from JWT token
                await _groupSavingPlanService.JoinGroupSavingPlanAsync(userId, joinGroupSavingPlanDto.GroupSavingPlanId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("my-plans")]
        public async Task<IActionResult> GetMyPlans()
        {
            try
            {
                var userId = GetUserIdFromToken(); // Implement this method to get user ID from JWT token
                var groupSavingPlans = await _groupSavingPlanService.GetUserGroupSavingPlansAsync(userId);
                return Ok(groupSavingPlans);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("contribute")]
        public async Task<IActionResult> Contribute(ContributeDto contributeDto)
        {
            try
            {
                var userId = GetUserIdFromToken(); // Implement this method to get user ID from JWT token
                await _groupSavingPlanService.AddContributionAsync(userId, contributeDto.GroupSavingPlanId, contributeDto.Amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/contributions")]
        public async Task<IActionResult> GetContributions(Guid id)
        {
            try
            {
                var contributions = await _groupSavingPlanService.GetGroupSavingPlanContributionsAsync(id);
                return Ok(contributions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("send-reminder")]
        public async Task<IActionResult> SendReminder(SendReminderDto sendReminderDto)
        {
            try
            {
                var userId = GetUserIdFromToken(); // Implement this method to get user ID from JWT token
                await _groupSavingPlanService.SendContributionReminderAsync(userId, sendReminderDto.GroupSavingPlanId);
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
