using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizMart.Models.ViewModels;
using QuizMart.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;

namespace QuizMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                return Ok(await GetCurrentUserProfileAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateProfileVM user)
        {
            try
            {
                var currentUser = await GetCurrentUserProfileAsync();
                if (currentUser == null) { return BadRequest("Failed to establish a link with the User"); }
                if (currentUser.Username != user.Username)
                {
                    bool isUsernameAvailable = await _profileService.IsUsernameAvailableAsync(user.Username!);

                    if (!isUsernameAvailable)
                    {
                        return BadRequest("Username is already in use");
                    }
                }
                if (currentUser.Email != user.Email)
                {
                    bool isEmailAvailable = await _profileService.IsEmailAvailableAsync(user.Email!);

                    if (!isEmailAvailable)
                    {
                        return BadRequest("Email is already in use");
                    }
                }

                bool result = await _profileService.UpdateUserProfileAsync(user, currentUser.UserId);

                if (result)
                {
                    return Ok("Updated Successfully");
                }
                else
                {
                    return BadRequest("Failed to update the User");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                var user = await GetCurrentUserProfileAsync();
                if (user == null) { return BadRequest("Failed to establish a link with the User"); }
                if (model.NewPassword != model.ConfirmPassword) return BadRequest("New password and confirm password must be the same");
                if (BC.EnhancedVerify(model.NewPassword, user.PasswordHash)) return BadRequest("New password cannot be the same as the current password");
                if (model.NewPassword!.Contains(user.Username!)) return BadRequest("New password cannot contain the username");
                if (!BC.EnhancedVerify(model.CurrentPassword, user.PasswordHash)) return BadRequest("Incorrect current password");

                bool result = await _profileService.ChangePasswordAsync(model, user.UserId);

                if (result)
                {
                    return Ok("Changed Password Successfully");
                }
                else
                {
                    return BadRequest("Failed to change the password");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("profile-picture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile picture)
        {
            if (picture != null && picture.Length > 0)
            {
                if (picture.Length > 1024 * 1024) // Check if the file size exceeds 1MB
                {
                    // File size is too large, return a warning
                    return BadRequest("The profile picture exceeds the maximum allowed size");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await picture.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();

                    // Convert the image bytes to a base64 string
                    var base64String = Convert.ToBase64String(imageBytes);

                    // Save the base64 string to the database
                    var user = await GetCurrentUserProfileAsync();
                    if (user == null) { return BadRequest("Failed to establish a link with the User"); }
                    bool result = await _profileService.SaveProfilePictureAsync(user.UserId, base64String);

                    if (result)
                    {
                        return Ok("Successfully uploaded the profile picture");
                    }
                    else
                    {
                        return BadRequest("Failed to save the profile picture");
                    }
                }
            }

            // No file was provided, return an error
            return BadRequest("No profile picture file was uploaded");
        }

        // Helper method to get the current user's profile
        [NonAction]
        private async Task<UserInfo?> GetCurrentUserProfileAsync()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.Sid); // Ensure this matches your claim type

            if (Guid.TryParse(userIdString, out Guid userId))
            {
                return await _profileService.GetUserProfileAsync(userId);
            }

            throw new Exception("Authentication token is invalid or missing");
        }
    }
}
