using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;
using QuizMart.Services.IServices;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        #region Get All Users
        [HttpGet("Get-all-Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        #endregion

        #region Add User
        [HttpPost("Add-User")]
        public async Task<IActionResult> AddUser([FromBody] UserInfo user)
        {
            var result = await _userService.AddUserAsync(user);
            return Ok(result);
        }
        #endregion

        #region Update User
        [HttpPut("Update-User")]
        public async Task<IActionResult> UpdateUser([FromBody] UserInfo user)
        {
            var result = await _userService.UpdateUserAsync(user);
            return Ok(result);
        }
        #endregion

        #region Delete User
        [HttpDelete("Delete-User")]
        public async Task<IActionResult> DeleteUser([FromBody] Guid userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            return Ok(result);
        }
        #endregion

        #region Change Password
        [HttpPut("Change-Password")]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            var result = await _userService.ChangePassword(userId, oldPassword, newPassword);
            return Ok(result);
        }
        #endregion

        #region Get User By Id
        [HttpGet("Get-User-By-Id")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }
        #endregion
    }
}
