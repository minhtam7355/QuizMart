using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Repositories
{
    public interface IUserRepository
    {
        Task<string> AddUser(User user);
        Task<string> DeleteUser(Guid userId);
        Task<ICollection<User>> GetAllUsers();
        Task<string> UpdateUser(User user);
        Task<User> GetUserById(Guid userId);
        Task<string> ChangePassword(Guid userId,string oldPassword, string newPassword);
    }
}
