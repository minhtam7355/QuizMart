using AutoMapper;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;
using QuizMart.Services.IServices;

namespace QuizMart.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<string> AddUserAsync(UserInfo user)
        {
            try
            {
                var userEntity = _mapper.Map<User>(user);
                var result = await _userRepository.AddUser(userEntity);
                return result;
            }
            catch (Exception ex)
            {
                return $"Error adding user: {ex.Message}";
            }
        }

        public async Task<string> ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            try
            {
                return await _userRepository.ChangePassword(userId, oldPassword, newPassword);                
            }
            catch (Exception ex)
            {
                return $"Error changing password: {ex.Message}";
            }
        }

        public async Task<string> DeleteUserAsync(Guid userId)
        {
            return await _userRepository.DeleteUser(userId);
        }

        public async Task<ICollection<UserInfo>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsers();
            var userModels = _mapper.Map<ICollection<UserInfo>>(users);
            return userModels;
        }

        public async Task<UserInfo> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            var userModel = _mapper.Map<UserInfo>(user);
            return userModel;
        }

        public async Task<string> UpdateUserAsync(UserInfo user)
        {
            try
            {
                var userEntity = _mapper.Map<User>(user);
                var result = await _userRepository.UpdateUser(userEntity);
                return result;
            }
            catch(Exception ex)
            {
                return $"Error updating user: {ex.Message}";
            }
        }
    }
}
