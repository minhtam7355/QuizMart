using QuizMart.Models.ViewModels;
using System.Threading.Tasks;

namespace QuizMart.Services.IServices
{
    public interface IAccessService
    {
        public Task<UserInfo?> Login(LoginModel model);

        public Task<bool> Signup(SignupModel model);
    }
}
