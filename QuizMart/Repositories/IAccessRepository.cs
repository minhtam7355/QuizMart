using QuizMart.Models.ViewModels;
using QuizMart.Models.DomainModels;

namespace QuizMart.Repositories
{
    public interface IAccessRepository
    {
        public Task<UserInfo?> Login(LoginModel model);

        public Task<Boolean> Signup(SignupModel model);
    }
}
