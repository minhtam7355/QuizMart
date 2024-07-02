using QuizMart.Models.ViewModels;
using System.Threading.Tasks;

namespace QuizMart.Services
{
    public interface IAccessService
    {
        public Task<UserInfo?> Login(LoginModel model);

        public Task<Boolean> Signup(SignupModel model);
    }
}
