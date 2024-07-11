using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizMart.Context;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;
using BC = global::BCrypt.Net.BCrypt;


namespace QuizMart.Repositories
{
    public class AccessRepository : IAccessRepository
    {
        private readonly QuizMartDbContext _dbContext;
        private readonly IMapper _mapper;

        public AccessRepository(QuizMartDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserInfo?> Login(LoginModel model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user =>
                (user.Username == model.UsernameOrEmail || user.Email == model.UsernameOrEmail));

            if (user != null)
            {
                if (!BC.EnhancedVerify(model.Password, user.PasswordHash)) { return null; }
                var roleId = user.RoleId;
                if (roleId == null) return null;

                var role = await _dbContext.Roles.FirstOrDefaultAsync(role => role.RoleId == roleId);
                var userInfo = _mapper.Map<UserInfo>(user);
                userInfo.Role = role?.RoleName;

                return userInfo;
            }

            return null;
        }

        public async Task<Boolean> Signup(SignupModel model)
        {
            try
            {
                var newUser = _mapper.Map<User>(model);
                var role = await _dbContext.Roles.FirstOrDefaultAsync(role => role.RoleName == "FreeUser");
                if (role == null) return false;
                newUser.RoleId = role?.RoleId;

                await _dbContext.Users.AddAsync(newUser);
                await _dbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
