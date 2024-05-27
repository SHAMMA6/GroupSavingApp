using ApplicationLayer.IServices;
using DomainLayer.Entitys;
using Infrastructurlayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructurlayer.Services
{
    public class GroupSavingPlanService : IGroupSavingPlanService
    {
        private readonly AppDbContext _context;

        public GroupSavingPlanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GroupSavingPlan> CreateGroupSavingPlanAsync(Guid userId, string name, decimal totalAmount, int numberOfParticipants, int durationInMonths)
        {
            var groupSavingPlan = new GroupSavingPlan
            {
                Id = Guid.NewGuid(),
                Name = name,
                TotalAmount = totalAmount,
                NumberOfParticipants = numberOfParticipants,
                DurationInMonths = durationInMonths,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(durationInMonths),
                CreatedByUserId = userId
            };

            _context.GroupSavingPlans.Add(groupSavingPlan);
            await _context.SaveChangesAsync();

            return groupSavingPlan;
        }

        public async Task JoinGroupSavingPlanAsync(Guid userId, Guid groupSavingPlanId)
        {
            var groupSavingPlan = await _context.GroupSavingPlans.Include(gsp => gsp.Participants).FirstOrDefaultAsync(gsp => gsp.Id == groupSavingPlanId);
            if (groupSavingPlan == null)
            {
                throw new Exception("Group saving plan not found.");
            }

            if (groupSavingPlan.Participants.Count >= groupSavingPlan.NumberOfParticipants)
            {
                throw new Exception("Group saving plan is full.");
            }

            if (groupSavingPlan.Participants.Any(p => p.Id == userId))
            {
                throw new Exception("User is already a participant.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            groupSavingPlan.Participants.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupSavingPlan>> GetUserGroupSavingPlansAsync(Guid userId)
        {
            return await _context.GroupSavingPlans.Include(gsp => gsp.Participants).Where(gsp => gsp.Participants.Any(p => p.Id == userId)).ToListAsync();
        }

        public async Task AddContributionAsync(Guid userId, Guid groupSavingPlanId, decimal amount)
        {
            var contribution = new Contribution
            {
                Id = Guid.NewGuid(),
                GroupSavingPlanId = groupSavingPlanId,
                UserId = userId,
                Amount = amount,
                ContributionDate = DateTime.UtcNow
            };

            _context.Contributions.Add(contribution);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contribution>> GetGroupSavingPlanContributionsAsync(Guid groupSavingPlanId)
        {
            return await _context.Contributions.Where(c => c.GroupSavingPlanId == groupSavingPlanId).ToListAsync();
        }

        public async Task SendContributionReminderAsync(Guid userId, Guid groupSavingPlanId)
        {
            var groupSavingPlan = await _context.GroupSavingPlans.Include(gsp => gsp.Participants).FirstOrDefaultAsync(gsp => gsp.Id == groupSavingPlanId);
            if (groupSavingPlan == null)
            {
                throw new Exception("Group saving plan not found.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var message = $"Reminder: It's time to contribute to the group saving plan {groupSavingPlan.Name}.";
            // Send notification logic here...
        }
    }
}
