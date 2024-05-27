using DomainLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.IServices
{
    public interface IGroupSavingPlanService
    {
        Task<GroupSavingPlan> CreateGroupSavingPlanAsync(Guid userId, string name, decimal totalAmount, int numberOfParticipants, int durationInMonths);
        Task JoinGroupSavingPlanAsync(Guid userId, Guid groupSavingPlanId);
        Task<IEnumerable<GroupSavingPlan>> GetUserGroupSavingPlansAsync(Guid userId);
        Task AddContributionAsync(Guid userId, Guid groupSavingPlanId, decimal amount);
        Task<IEnumerable<Contribution>> GetGroupSavingPlanContributionsAsync(Guid groupSavingPlanId);
        Task SendContributionReminderAsync(Guid userId, Guid groupSavingPlanId);
    }
}
