using QuizMart.Models.ViewModels;

namespace QuizMart.Services.IServices
{
    public interface IUserService
    {
        public Task<string> AddUserAsync(UserInfo user);
        public Task<string> DeleteUserAsync(Guid userId);
        public Task<ICollection<UserInfo>> GetAllUsersAsync();
        public Task<string> UpdateUserAsync(UserInfo user);
        public Task<UserInfo> GetUserByIdAsync(Guid userId);
        Task<string> ChangePassword(Guid userId, string oldPassword, string newPassword);
    }
}
