using QuizMart.Models.DomainModels;
using System;
using System.Threading.Tasks;

namespace QuizMart.Repositories
{
    public interface IProfileRepository
    {
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<Role?> GetRoleByIdAsync(Guid roleId);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> UpdateUserProfileAsync(User user);    }
}
