using DomainLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.IServices
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string username, string email, string password);
        Task<User> AuthenticateUserAsync(string username, string password);
        Task<UserProfile> UpdateUserProfileAsync(Guid userId, UserProfile updatedProfile);
        Task SendPasswordResetEmailAsync(string email);
    }
}
