using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.GroupSavingPlanDTOs
{
    public class CreateGroupSavingPlanDto
    {
        public string Name { get; set; }
        public decimal TotalAmount { get; set; }
        public int NumberOfParticipants { get; set; }
        public int DurationInMonths { get; set; }
    }

    public class JoinGroupSavingPlanDto
    {
        public Guid GroupSavingPlanId { get; set; }
    }

    public class ContributeDto
    {
        public Guid GroupSavingPlanId { get; set; }
        public decimal Amount { get; set; }
    }

    public class SendReminderDto
    {
        public Guid GroupSavingPlanId { get; set; }
    }

}
