using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entitys
{
    public class GroupSavingPlan
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal TotalAmount { get; set; }
    public int NumberOfParticipants { get; set; }
    public int DurationInMonths { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid CreatedByUserId { get; set; }
    
    // Navigation properties
    public User CreatedByUser { get; set; }
    public ICollection<User> Participants { get; set; }
    public ICollection<Contribution> Contributions { get; set; }
}
}
