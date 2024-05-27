using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entitys
{
    public class Contribution
    {
        public Guid Id { get; set; }
        public Guid GroupSavingPlanId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ContributionDate { get; set; }

        // Navigation properties
        public GroupSavingPlan GroupSavingPlan { get; set; }
        public User User { get; set; }
    }
}
