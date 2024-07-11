using QuizMart.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace QuizMart.Services.IServices
{
    public interface IProfileService
    {
        Task<UserInfo?> GetUserProfileAsync(Guid userId);
        Task<bool> UpdateUserProfileAsync(UpdateProfileVM model, Guid userId);
        Task<bool> ChangePasswordAsync(ChangePasswordModel model, Guid userId);
        Task<bool> SaveProfilePictureAsync(Guid userId, string base64Image);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<bool> IsEmailAvailableAsync(string email);
    }
}
