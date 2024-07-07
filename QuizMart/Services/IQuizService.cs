using QuizMart.Models.ViewModels;

namespace QuizMart.Services
{
    public interface IQuizService
    {
        public Task AddQuizAsync(QuizModel quiz);
        Task<List<QuizModel>> GetAllQuizzes();
        Task<QuizModel> GetQuizByIdAsync(Guid quizId);
        Task DeleteQuizAsync(Guid quizId);
         Task UpdateQuizAsync(QuizModel quizModel);

        Task<List<QuizModel>> GetAllFavoriteQuizzesAsync();
        Task SetQuizFavoriteStatusAsync(Guid quizId);
    }
}
