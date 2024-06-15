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
        private readonly IMapper _mapper;
        public UserRepository(QuizMartDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<string> AddUser(UserInfo user)
        {
            User newUser = _mapper.Map<User>(user);
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return Task.FromResult("User added successfully");
        }

        public Task<string> DeleteUser(Guid userId)
        {
            UserInfo user = GetUserById(userId).Result;
            if (user != null)
            {
                _dbContext.Users.Remove(_mapper.Map<User>(user));
                _dbContext.SaveChanges();
                return Task.FromResult("User delete successfully");
            }
            else
            {
                return Task.FromResult("User not found");
            }
        }

        public async Task<ICollection<UserInfo>> GetAllUsers()
        {
            return await _dbContext.Users.Select(u => _mapper.Map<UserInfo>(u)).ToListAsync();
        }

        public async Task<string> UpdateUser(UserInfo user)
        {
            UserInfo newUser = await GetUserById(user.UserId);
            if (newUser != null)
            {
                newUser.Username = user.Username;
                newUser.Email = user.Email;
                newUser.FullName = user.FullName;
                newUser.PhoneNumber = user.PhoneNumber;
                newUser.HomeAddress = user.HomeAddress;
                newUser.DateOfBirth = user.DateOfBirth;

                _dbContext.SaveChanges();
                return "User updated successfully";
            }
            else
            {
                return "User not found";
            }
        }

        public async Task<string> ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            UserInfo userInfo = await GetUserById(userId);
            if (userInfo != null)
            {
                // This should be a hashed comparison in a real application
                if (BCrypt.Net.BCrypt.Verify(oldPassword, userInfo.PasswordHash))
                {
                    UserInfo user = GetUserById(userId).Result;
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
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

        public async Task<UserInfo> GetUserById(Guid userId)
        {
            return _mapper.Map<UserInfo>(_dbContext.Users.FirstOrDefault(u => u.UserId == userId));
        }
    }
}
