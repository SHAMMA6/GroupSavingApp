using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.GroupSavingPlanDTOs
{
    public class AddContributionDTO
    {
       public string userId { get; set; }
       public string groupSavingPlanId { get; set; }
       public decimal amount { get; set; }
    }
}
