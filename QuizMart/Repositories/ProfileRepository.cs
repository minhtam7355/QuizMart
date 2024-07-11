using Microsoft.EntityFrameworkCore;
using QuizMart.Context;
using QuizMart.Models.DomainModels;
using System;
using System.Threading.Tasks;

namespace QuizMart.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly QuizMartDbContext _dbContext;

        public ProfileRepository(QuizMartDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.UserId == userId);
        }

        public async Task<Role?> GetRoleByIdAsync(Guid roleId)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(role => role.RoleId == roleId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                return await _dbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }
        public async Task<bool> UpdateUserProfileAsync(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                return false;
            }

            _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
