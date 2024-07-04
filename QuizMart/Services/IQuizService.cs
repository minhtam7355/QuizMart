using QuizMart.Models.ViewModels;

namespace QuizMart.Services
{
    public interface IQuizService
    {
        public Task AddQuizAsync(QuizModel quiz);
        Task<List<QuizModel>> GetAllQuizzes();
    }
}
