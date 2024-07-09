using QuizMart.Models.ViewModels;
using QuizMart.Repositories;
using QuizMart.Services.IServices;
using System.Threading.Tasks;

namespace QuizMart.Services
{
    public class AccessService : IAccessService
    {
        private readonly IAccessRepository _accessRepository;

        public AccessService(IAccessRepository accessRepository)
        {
            _accessRepository = accessRepository;
        }

        public async Task<UserInfo?> Login(LoginModel model)
        {
            return await _accessRepository.Login(model);
        }

        public async Task<bool> Signup(SignupModel model)
        {
            return await _accessRepository.Signup(model);
        }
    }
}
