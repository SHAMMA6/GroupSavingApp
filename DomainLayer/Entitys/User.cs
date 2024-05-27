using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entitys
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public UserProfile UserProfile { get; set; }
        public ICollection<GroupSavingPlan> GroupSavingPlans { get; set; }
        public ICollection<Contribution> Contributions { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
