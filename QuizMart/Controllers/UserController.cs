using Microsoft.AspNetCore.Mvc;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;
        public UserController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        #region Get All Users
        [HttpGet("Get-all-Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }
        #endregion

        #region Add User
        [HttpPost("Add-User")]
        public async Task<IActionResult> AddUser([FromBody] UserInfo user)
        {
            var result = await _userRepository.AddUser(user);
            return Ok(result);
        }
        #endregion

        #region Update User
        [HttpPut("Update-User")]
        public async Task<IActionResult> UpdateUser([FromBody] UserInfo user)
        {
            var result = await _userRepository.UpdateUser(user);
            return Ok(result);
        }
        #endregion

        #region Delete User
        [HttpDelete("Delete-User")]
        public async Task<IActionResult> DeleteUser([FromBody] Guid userId)
        {
            var result = await _userRepository.DeleteUser(userId);
            return Ok(result);
        }
        #endregion

        #region Change Password
        [HttpPut("Change-Password")]
        public async Task<IActionResult> ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            var result = await _userRepository.ChangePassword(userId, oldPassword, newPassword);
            return Ok(result);
        }
        #endregion
    }
}
