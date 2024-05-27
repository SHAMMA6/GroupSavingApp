using ApplicationLayer.DTOs.UserDTOs;
using ApplicationLayer.IServices;
using DomainLayer.Entitys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presintationlayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(registerUserDto.Username, registerUserDto.Email, registerUserDto.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            try
            {
                var user = await _userService.AuthenticateUserAsync(loginUserDto.Username, loginUserDto.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UserProfile updateUserProfileDto)
        {
            try
            {
                var userId = GetUserIdFromToken(); // Implement this method to get user ID from JWT token
                var userProfile = await _userService.UpdateUserProfileAsync(userId, updateUserProfileDto);
                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset(PasswordResetDto passwordResetDto)
        {
            try
            {
                await _userService.SendPasswordResetEmailAsync(passwordResetDto.Email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Guid GetUserIdFromToken()
        {
            // Implement logic to extract user ID from JWT token
            return Guid.NewGuid();
        }
    }
}
