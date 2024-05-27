using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.GroupSavingPlanDTOs
{
    public class CreatGroupDTO
    {
        public string userId {  get; set; }
        public string name { get; set; }
        public decimal totalAmount { get; set; }
        public int numberOfParticipants { get; set; }
        public int durationInMonths { get; set; }
    }
}
