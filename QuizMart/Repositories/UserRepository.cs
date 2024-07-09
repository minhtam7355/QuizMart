using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizMart.Context;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using BC = global::BCrypt.Net.BCrypt;

namespace QuizMart.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QuizMartDbContext _dbContext;

        public UserRepository(QuizMartDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return "User added successfully";
            }
            catch (Exception ex)
            {
                return $"Error adding user: {ex.Message}";
            }
        }

        public async Task<string> DeleteUser(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return "User not found";
            }
            try
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return "User deleted successfully";
            }
            catch(Exception ex)
            {
                return $"Error deleting user: {ex.Message}";
            }
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<string> UpdateUser(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                return "User not found";
            }
            try
            {
                _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
                await _dbContext.SaveChangesAsync();
                return "User updated successfully";
            }
            catch (Exception ex)
            {
                return $"Error updating user: {ex.Message}";
            }       
        }

        public async Task<string> ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            User user = await _dbContext.Users.FindAsync(userId);

            if (user != null)
            {
                // This should be a hashed comparison in a real application
                if (BC.EnhancedVerify(oldPassword, user.PasswordHash))
                {
                    user.PasswordHash = BC.HashPassword(newPassword);
                    _dbContext.SaveChanges();

                    return "Password changed successfully";
                }
                else
                {
                    return "Old password does not match the current password";
                }
            }
            else
            {
                return "User not found";
            }
        }

        public async Task<User> GetUserById(Guid userId)
        {
            User user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
