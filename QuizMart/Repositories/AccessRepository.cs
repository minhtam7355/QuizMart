using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizMart.Context;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
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
                return _mapper.Map<UserInfo>(user);
            }

            return null;
        }

        public async Task<Boolean> Signup(SignupModel model)
        {
            try
            {
                var newStudent = _mapper.Map<User>(model);

                await _dbContext.Users.AddAsync(newStudent);
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
