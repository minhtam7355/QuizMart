using QuizMart.Models.ViewModels;

namespace QuizMart.Services.IServices
{
    public interface IQuizService
    {
        public Task AddQuizAsync(QuizModel quiz);
        Task<List<QuizModel>> GetAllQuizzesAsync();
        Task<QuizModel> GetQuizByIdAsync(Guid quizId);
        Task DeleteQuizAsync(Guid quizId);
        Task UpdateQuizAsync(QuizModel quizModel);

        Task<List<QuizModel>> GetAllFavoriteQuizzesAsync();
        Task SetQuizFavoriteStatusAsync(Guid quizId);

        Task<bool> CheckChoicesAsync(Guid quizId, List<Guid> choiceIds);
    }
}
