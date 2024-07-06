using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;

namespace QuizMart.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public async Task AddQuizAsync(QuizModel quiz)
        {
             await _quizRepository.AddQuizAsync(quiz);
        }
        public async Task<List<QuizModel>> GetAllQuizzes()
        {
            return await _quizRepository.GetAllQuizzes();
        }
        public async Task DeleteQuizAsync(Guid quizId)
        {
            await _quizRepository.DeleteQuizAsync(quizId);
        }
        public async Task UpdateQuizAsync(QuizModel quizModel)
        {
             await _quizRepository.UpdateQuizAsync(quizModel);
        }
        public async Task<List<QuizModel>> GetAllFavoriteQuizzesAsync()
        {
            return await _quizRepository.GetAllFavoriteQuizzesAsync();
        }
        public async Task SetQuizFavoriteStatusAsync(Guid quizId)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(quizId);
            if (quiz == null)
            {
                throw new KeyNotFoundException("Quiz not found.");
            }

            quiz.isFavorite = true;
            await _quizRepository.UpdateQuizAsync(quiz);
        }
        public async Task<QuizModel> GetQuizByIdAsync(Guid quizId)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(quizId);
            return quiz;
        }


    }
    
}
