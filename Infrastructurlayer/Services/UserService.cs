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
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(string username, string email, string password)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                throw new Exception("Email is already registered.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password),
                IsActive = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Send confirmation email logic here...

            return user;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials.");
            }

            return user;
        }

        public async Task<UserProfile> UpdateUserProfileAsync(Guid userId, UserProfile updatedProfile)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userId);
            if (userProfile == null)
            {
                throw new Exception("User profile not found.");
            }

            userProfile.FirstName = updatedProfile.FirstName;
            userProfile.LastName = updatedProfile.LastName;
            userProfile.Address = updatedProfile.Address;
            userProfile.PhoneNumber = updatedProfile.PhoneNumber;
            userProfile.DateOfBirth = updatedProfile.DateOfBirth;

            await _context.SaveChangesAsync();

            return userProfile;
        }

        public async Task SendPasswordResetEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("Email not found.");
            }

            // Send password reset email logic here...
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }

}
