using QuizMart.Models.ViewModels;

namespace QuizMart.Repositories
{
    public interface IUserRepository
    {
        Task<string> AddUser(UserInfo user);
        Task<string> DeleteUser(Guid userId);
        Task<ICollection<UserInfo>> GetAllUsers();
        Task<string> UpdateUser(UserInfo user);
        Task<string> ChangePassword(Guid userId,string oldPassword, string newPassword);
    }
}
