using AutoMapper;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;
using QuizMart.Services.IServices;
using System;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace QuizMart.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfileService(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async Task<UserInfo?> GetUserProfileAsync(Guid userId)
        {
            var user = await _profileRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            var roleId = user.RoleId;
            if (roleId == null) return null;

            var role = await _profileRepository.GetRoleByIdAsync(roleId.Value);
            var userInfo = _mapper.Map<UserInfo>(user);
            userInfo.Role = role?.RoleName;

            return userInfo;
        }

        public async Task<bool> UpdateUserProfileAsync(UpdateProfileVM model, Guid userId)
        {
            var existingUser = await _profileRepository.GetUserByIdAsync(userId);
            if (existingUser == null) return false;

            _mapper.Map(model, existingUser);

            return await _profileRepository.UpdateUserProfileAsync(existingUser);
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordModel model, Guid userId)
        {
            var user = await _profileRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.PasswordHash = BC.EnhancedHashPassword(model.NewPassword, 13);

            return await _profileRepository.SaveChangesAsync();
        }

        public async Task<bool> SaveProfilePictureAsync(Guid userId, string base64Image)
        {
            var user = await _profileRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.ProfilePicture = base64Image;

            return await _profileRepository.SaveChangesAsync();
        }

        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            var users = await _profileRepository.GetUsersAsync();
            return users.All(user => user.Username != username);
        }

        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            var users = await _profileRepository.GetUsersAsync();
            return users.All(user => user.Email != email);
        }
    }
}
